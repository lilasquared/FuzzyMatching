using System.Threading;
using System.Threading.Tasks;
using FuzzyMatch.Core;
using FuzzyMatch.Core.UoW;
using MediatR;
using MediatR.CQRS;

namespace FuzzyMatch.Api.Handlers.Datasets
{
    public class CreateDatasetHandler : IRequestHandler<CreateDataset, IResult<Dataset>>
    {
        private readonly DataUnitOfWork _uow;

        public CreateDatasetHandler(DataUnitOfWork uow)
        {
            _uow = uow;
        }

        public Task<IResult<Dataset>> Handle(CreateDataset request, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                return _uow.Execute(db =>
                {
                    var dataset = new Dataset
                    {
                        Name = request.Name
                    };
                    db.GetCollection<Dataset>().Insert(dataset);

                    var fileInfo =
                        db.FileStorage.Upload($"$/datasets/{dataset.Id}", request.FileName, request.FileStream);

                    dataset.FileId = fileInfo.Id;
                    dataset.FileName = fileInfo.Filename;

                    db.GetCollection<Dataset>().Update(dataset);

                    return Result.Success(dataset);
                });
            }, cancellationToken);
        }
    }
}
