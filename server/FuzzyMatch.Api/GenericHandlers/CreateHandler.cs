using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MediatR.CQRS;
using MediatR.CQRS.Requests;

namespace FuzzyMatch.Api.GenericHandlers
{
    public class CreateHandler<TModel> : IRequestHandler<Create<TModel>, IResult<TModel>>
    {
        public Task<IResult<TModel>> Handle(Create<TModel> request, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                using (var db = new LiteDB.LiteDatabase("data.db"))
                {
                    db.GetCollection<TModel>().Insert(request.Model);
                    return Result.Success(request.Model);
                }
            }, cancellationToken);
        }
    }
}
