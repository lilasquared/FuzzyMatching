using System.Threading;
using System.Threading.Tasks;
using FuzzyMatch.Api.Configuration;
using MediatR;
using MediatR.CQRS;
using MediatR.CQRS.Requests;

namespace FuzzyMatch.Api.Handlers.Generic
{
    public class GetOneHandler<TModel> : IRequestHandler<GetOne<TModel>, IResult<TModel>>
    {
        private readonly DatabaseOptions _dbOptions;

        public GetOneHandler(DatabaseOptions dbOptions)
        {
            _dbOptions = dbOptions;
        }

        public Task<IResult<TModel>> Handle(GetOne<TModel> request, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                using (var db = new LiteDB.LiteDatabase(_dbOptions.Path))
                {
                    return Result.Success(db.GetCollection<TModel>().FindById(request.Id));
                }
            }, cancellationToken);
        }
    }
}
