using System.Threading;

namespace FuzzyMatch.Core
{
    public interface IQueueWriter<in TMessage> where TMessage : IQueueable, new()
    {
        void Enqueue(TMessage message, CancellationToken cancellationToken);
    }
}