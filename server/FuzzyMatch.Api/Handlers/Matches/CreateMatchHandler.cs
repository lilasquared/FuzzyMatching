using System.Threading;
using System.Threading.Tasks;
using FuzzyMatch.Core.Appends;
using FuzzyMatch.Core.UoW;
using MediatR;
using MediatR.CQRS;

namespace FuzzyMatch.Api.Handlers.Matches
{
    public class CreateMatchHandler : IRequestHandler<CreateMatch, IResult<Append>>
    {
        private readonly DataUnitOfWork _uow;

        public CreateMatchHandler(DataUnitOfWork uow)
        {
            _uow = uow;
        }

        public Task<IResult<Append>> Handle(CreateMatch request, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                return _uow.Execute(db =>
                {
                    var match = new Append
                    {
                        SourceId = request.SourceId,
                        LookupId = request.LookupId,
                        Threshold = request.Threshold
                    };
                    db.GetCollection<Append>().Insert(match);
                    return Result.Success(match);
                });
            }, cancellationToken);
        }
    }
}
