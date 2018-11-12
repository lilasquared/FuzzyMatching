using System.Threading;
using System.Threading.Tasks;
using FuzzyMatch.Core;
using FuzzyMatch.Core.Appends;
using FuzzyMatch.Core.Configuration;
using MediatR;
using MediatR.CQRS;

namespace FuzzyMatch.Api.Handlers.Matches
{
    public class CreateMatchHandler : IRequestHandler<CreateMatch, IResult<Append>>
    {
        private readonly LiteDatabaseProvider _provider;

        public CreateMatchHandler(LiteDatabaseProvider provider)
        {
            _provider = provider;
        }

        public Task<IResult<Append>> Handle(CreateMatch request, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                using (var db = _provider(DataContext.Data))
                {
                    var match = new Append
                    {
                        SourceId = request.SourceId,
                        LookupId = request.LookupId,
                        Threshold = request.Threshold
                    };
                    db.GetCollection<Append>().Insert(match);
                    return Result.Success(match);
                }
            }, cancellationToken);
        }
    }
}
