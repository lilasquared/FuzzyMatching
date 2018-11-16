using System.Threading;
using System.Threading.Tasks;
using FuzzyMatch.Core;
using FuzzyMatch.Core.UoW;
using MediatR;
using MediatR.CQRS;
using MediatR.CQRS.Requests;

namespace FuzzyMatch.Api.Handlers.Datasets
{
    public class DeleteDatasetHandler : IRequestHandler<Delete<Dataset>, IResult<Unit>>
    {
        private readonly DataUnitOfWork _uow;

        public DeleteDatasetHandler(DataUnitOfWork uow)
        {
            _uow = uow;
        }

        public Task<IResult<Unit>> Handle(Delete<Dataset> request, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                return _uow.Execute(db =>
                {
                    var dataset = db.GetCollection<Dataset>().FindById(request.Id);

                    if (dataset.FileId != null)
                    {
                        db.FileStorage.Delete(dataset.FileId);
                    }

                    db.GetCollection<Dataset>().Delete(request.Id);

                    return Result.Success();
                });
            }, cancellationToken);
        }
    }
}
