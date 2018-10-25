using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FuzzyMatch.Api.Configuration;
using MediatR;
using MediatR.CQRS;
using MediatR.CQRS.Requests;

namespace FuzzyMatch.Api.Handlers.Generic
{
    public class GetAllHandler<TModel> : IRequestHandler<GetAll<TModel>, IResult<IEnumerable<TModel>>>
    {
        private readonly DatabaseOptions _dbOptions;

        public GetAllHandler(DatabaseOptions dbOptions)
        {
            _dbOptions = dbOptions;
        }

        public Task<IResult<IEnumerable<TModel>>> Handle(GetAll<TModel> request, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                using (var db = new LiteDB.LiteDatabase(_dbOptions.Path))
                {
                    return Result.Success(db.GetCollection<TModel>().FindAll());
                }
            }, cancellationToken);
        }
    }
}
