using System;
using System.Threading;
using System.Threading.Tasks;
using FuzzyMatch.Core;
using FuzzyMatch.Core.Appends;
using FuzzyMatch.Core.UoW;
using MediatR;
using MediatR.CQRS;

namespace FuzzyMatch.Api.Handlers.Matches
{
    public class StartMatchHandler : IRequestHandler<StartMatch, IResult<Unit>>
    {
        private readonly IQueueWriter<PerformAppend> _queueWriter;
        private readonly DataUnitOfWork _provider;

        public StartMatchHandler(IQueueWriter<PerformAppend> queueWriter, DataUnitOfWork provider)
        {
            _queueWriter = queueWriter;
            _provider = provider;
        }

        public Task<IResult<Unit>> Handle(StartMatch request, CancellationToken cancellationToken)
        {
            _provider.ExecuteCommand(db =>
            {
                var match = db.GetCollection<Append>().FindById(request.MatchId);
                if (match == null)
                {
                    throw new InvalidOperationException($"Append id {request.MatchId} does not exist.");
                }

                _queueWriter.Enqueue(new PerformAppend {MatchId = request.MatchId}, cancellationToken);
                match.Status = AppendStatus.Queued;
                db.GetCollection<Append>().Update(match);
            });

            return Task.FromResult<IResult<Unit>>(new SuccessResult());
        }
    }
}
