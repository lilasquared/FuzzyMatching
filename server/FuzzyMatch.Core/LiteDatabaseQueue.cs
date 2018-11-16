using System;
using System.Threading;
using System.Threading.Tasks;
using FuzzyMatch.Core.UoW;
using LiteDB;

namespace FuzzyMatch.Core
{
    public interface IQueueable
    {
        Int32 Id { get; set; }
    }

    public class LiteDatabaseQueueOptions
    {
        public TimeSpan PollingDelay { get; set; }

        public LiteDatabaseQueueOptions()
        {
            PollingDelay = TimeSpan.FromSeconds(5);
        }
    }

    public class LiteDatabaseQueue<TMessage> : IQueueReader<TMessage>, IQueueWriter<TMessage>
        where TMessage : IQueueable, new()
    {
        private readonly QueueUnitOfWork _uow;
        private readonly LiteDatabaseQueueOptions _options;

        public LiteDatabaseQueue(QueueUnitOfWork uow, LiteDatabaseQueueOptions options)
        {
            _uow = uow;
            _options = options;
        }

        public async Task<TMessage> Dequeue(CancellationToken cancellationToken)
        {
            do
            {
                cancellationToken.ThrowIfCancellationRequested();
                var message = _uow.Execute(db =>
                {
                    var collection = db.GetCollection<TMessage>();
                    var request = collection.FindOne(Query.All());
                    if (request != null)
                    {
                        collection.Delete(request.Id);
                    }

                    return request;
                });

                if (message != null)
                {
                    return message;
                }

                await Task.Delay(_options.PollingDelay, cancellationToken);
            } while (true);
        }

        public void Enqueue(TMessage message, CancellationToken cancellationToken)
        {
            _uow.ExecuteCommand(db => db.GetCollection<TMessage>().Insert(message));
        }
    }
}
