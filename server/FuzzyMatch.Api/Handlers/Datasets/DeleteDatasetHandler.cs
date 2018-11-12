using System.Threading;
using System.Threading.Tasks;
using FuzzyMatch.Core;
using FuzzyMatch.Core.Configuration;
using MediatR;
using MediatR.CQRS;
using MediatR.CQRS.Requests;

namespace FuzzyMatch.Api.Handlers.Datasets
{
    public class DeleteDatasetHandler : IRequestHandler<Delete<Dataset>, IResult<Unit>>
    {
        private readonly LiteDatabaseProvider _provider;

        public DeleteDatasetHandler(LiteDatabaseProvider provider)
        {
            _provider = provider;
        }

        public Task<IResult<Unit>> Handle(Delete<Dataset> request, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                using (var db = _provider(DataContext.Data))
                {
                    var dataset = db.GetCollection<Dataset>().FindById(request.Id);

                    if (dataset.FileId != null)
                    {
                        db.FileStorage.Delete(dataset.FileId);
                    }
                    db.GetCollection<Dataset>().Delete(request.Id);

                    return Result.Success();
                }
            }, cancellationToken);
        }
    }
}
