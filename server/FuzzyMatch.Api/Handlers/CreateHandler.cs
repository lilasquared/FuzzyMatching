using System.Threading;
using System.Threading.Tasks;
using FuzzyMatch.Api.Datasets;
using FuzzyMatch.Api.Requests;
using MediatR;
using MediatR.CQRS;

namespace FuzzyMatch.Api.Handlers
{
    public abstract class CreateHandler<TModel> : IRequestHandler<Create<TModel>, IResult<TModel>>
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

    public class CreateDatasetHandler : CreateHandler<Dataset> { }
}
