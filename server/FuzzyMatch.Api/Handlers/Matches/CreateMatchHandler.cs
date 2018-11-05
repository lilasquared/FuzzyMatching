using System.Threading;
using System.Threading.Tasks;
using FuzzyMatch.Api.Configuration;
using FuzzyMatch.Api.Models;
using LiteDB;
using MediatR;
using MediatR.CQRS;

namespace FuzzyMatch.Api.Handlers.Matches
{
    public class CreateMatchHandler : IRequestHandler<CreateMatch, IResult<Match>>
    {
        private readonly DatabaseOptions _dbOptions;

        public CreateMatchHandler(DatabaseOptions dbOptions)
        {
            _dbOptions = dbOptions;
        }

        public Task<IResult<Match>> Handle(CreateMatch request, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                using (var db = new LiteDatabase(_dbOptions.Path))
                {
                    var match = new Match
                    {
                        SourceId = request.SourceId,
                        LookupId = request.LookupId,
                        Threshold = request.Threshold
                    };
                    db.GetCollection<Match>().Insert(match);
                    return Result.Success(match);
                }
            }, cancellationToken);
        }
    }
}
