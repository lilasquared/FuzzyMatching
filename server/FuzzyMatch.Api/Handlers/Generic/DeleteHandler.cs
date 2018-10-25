using System.Threading;
using System.Threading.Tasks;
using FuzzyMatch.Api.Configuration;
using MediatR;
using MediatR.CQRS;
using MediatR.CQRS.Requests;

namespace FuzzyMatch.Api.Handlers.Generic
{
    public class DeleteHandler<TModel> : IRequestHandler<Delete<TModel>, IResult<Unit>>
    {
        private readonly DatabaseOptions _dbOptions;

        public DeleteHandler(DatabaseOptions dbOptions)
        {
            _dbOptions = dbOptions;
        }

        public Task<IResult<Unit>> Handle(Delete<TModel> request, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                using (var db = new LiteDB.LiteDatabase(_dbOptions.Path))
                {
                    db.GetCollection<TModel>().Delete(request.Id);
                    return Result.Success(Unit.Value);
                }
            }, cancellationToken);
        }
    }
}
