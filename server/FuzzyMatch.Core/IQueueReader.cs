using System.Threading;
using System.Threading.Tasks;

namespace FuzzyMatch.Core
{
    public interface IQueueReader<TMessage> where TMessage : IQueueable, new()
    {
        Task<TMessage> Dequeue(CancellationToken cancellationToken);
    }
}