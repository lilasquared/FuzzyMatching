using System.Threading;
using System.Threading.Tasks;
using FuzzyMatch.Core;
using FuzzyMatch.Core.Appends;
using MediatR;
using StructureMap;

namespace FuzzyMatch.Worker
{
    public class MatchQueueProcessor
    {
        private readonly IQueueReader<PerformAppend> _queueReader;
        private readonly IContainer _container;
        private readonly CancellationTokenSource _cts;

        public MatchQueueProcessor(IQueueReader<PerformAppend> queueReader, IContainer container)
        {
            _queueReader = queueReader;
            _container = container;
            _cts = new CancellationTokenSource();
        }

        public async Task Start()
        {
            while (!_cts.IsCancellationRequested)
            {
                var message = await _queueReader.Dequeue(_cts.Token);
                using (var nested = _container.GetNestedContainer())
                {
                    await nested.GetInstance<IMediator>().Send(message, _cts.Token);
                }
            }
        }

        public void Stop()
        {
            _cts.Cancel();
        }
    }
}
