using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MediatR.CQRS;
using MediatR.CQRS.Requests;

namespace FuzzyMatch.Api.Handlers.Generic
{
    public class GetAllHandler<TModel> : IRequestHandler<GetAll<TModel>, IResult<IEnumerable<TModel>>>
    {
        public Task<IResult<IEnumerable<TModel>>> Handle(GetAll<TModel> request, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                using (var db = new LiteDB.LiteDatabase("data.db"))
                {
                    return Result.Success(db.GetCollection<TModel>().FindAll());
                }
            }, cancellationToken);
        }
    }
}
