using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MediatR.CQRS;
using MediatR.CQRS.Requests;

namespace FuzzyMatch.Core
{
    public class DeleteHandler<TModel> : IRequestHandler<Delete<TModel>, IResult<Unit>>
    {
        public Task<IResult<Unit>> Handle(Delete<TModel> request, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                using (var db = new LiteDB.LiteDatabase("data.db"))
                {
                    db.GetCollection<TModel>().Delete(request.Id);
                    return Result.Success(Unit.Value);
                }
            }, cancellationToken);
        }
    }
}
