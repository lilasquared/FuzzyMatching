using System;
using System.Threading;
using System.Threading.Tasks;
using FuzzyMatch.Core.Configuration;
using LiteDB;

namespace FuzzyMatch.Core
{
    public interface IQueueable
    {
        Int32 Id { get; set; }
    }

    public class LiteDatabaseQueueOptions
    {
        public TimeSpan PoolingDelay { get; set; }
        public LiteDatabaseProvider LiteDatabaseProvider { get; set; }

        public LiteDatabaseQueueOptions()
        {
            PoolingDelay = TimeSpan.FromSeconds(5);
        }
    }

    public class LiteDatabaseQueue<TMessage> : IQueueReader<TMessage>, IQueueWriter<TMessage>
        where TMessage : IQueueable, new()
    {
        private readonly LiteDatabaseProvider _provider;
        private readonly LiteDatabaseQueueOptions _options;

        public LiteDatabaseQueue(LiteDatabaseProvider provider, LiteDatabaseQueueOptions options)
        {
            _provider = provider;
            _options = options;
        }

        public async Task<TMessage> Dequeue(CancellationToken cancellationToken)
        {
            using (var db = _provider(DataContext.Queue))
            {
                var collection = db.GetCollection<TMessage>();
                do
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    var request = collection.FindOne(Query.All());
                    if (request != null)
                    {
                        collection.Delete(request.Id);
                        return request;
                    }

                    await Task.Delay(_options.PoolingDelay, cancellationToken);
                } while (true);
            }
        }

        public void Enqueue(TMessage message, CancellationToken cancellationToken)
        {
            using (var db = _provider(DataContext.Queue))
            {
                db.GetCollection<TMessage>().Insert(message);
            }
        }
    }
}
