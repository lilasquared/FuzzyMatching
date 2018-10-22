using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MediatR.CQRS;
using MediatR.CQRS.Requests;

namespace FuzzyMatch.Core
{
    public class GetOneHandler<TModel> : IRequestHandler<GetOne<TModel>, IResult<TModel>>
    {
        public Task<IResult<TModel>> Handle(GetOne<TModel> request, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                using (var db = new LiteDB.LiteDatabase("data.db"))
                {
                    return Result.Success(db.GetCollection<TModel>().FindById(request.Id));
                }
            }, cancellationToken);
        }
    }
}
