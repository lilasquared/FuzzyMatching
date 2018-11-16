using System;
using System.IO;
using System.Threading.Tasks;
using FuzzyMatch.Core.Configuration;
using LiteDB;
using MediatR;

namespace FuzzyMatch.Core.UoW
{
    public abstract class UnitOfWork
    {
        private readonly ContextProvider _provider;

        protected UnitOfWork(ContextProvider provider)
        {
            _provider = provider;
        }

        public Task<Unit> ExecuteCommand(Action<LiteDatabase> callback)
        {
            return Execute(db =>
            {
                callback(db);
                return Unit.Task;
            });
        }

        public T Execute<T>(Func<LiteDatabase, T> callback)
        {
            return Execute(callback, TimeSpan.FromMinutes(1));
        }

        public T Execute<T>(Func<LiteDatabase, T> callback, TimeSpan timeout)
        {
            var expiration = DateTime.UtcNow.Add(timeout);
            while (DateTime.UtcNow < expiration)
            {
                try
                {
                    using (var db = _provider())
                    {
                        return callback(db);
                    }
                }
                catch (IOException)
                {
                    Console.WriteLine("Unable to get lock on db");
                }
            }

            throw new TimeoutException("Timeout period expired before lock could be obtained.");
        }
    }
}
