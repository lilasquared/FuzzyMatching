using System.Threading;
using System.Threading.Tasks;
using FuzzyMatch.Core;
using FuzzyMatch.Core.Configuration;
using MediatR;
using MediatR.CQRS;

namespace FuzzyMatch.Api.Handlers.Datasets
{
    public class CreateDatasetHandler : IRequestHandler<CreateDataset, IResult<Dataset>>
    {
        private readonly LiteDatabaseProvider _provider;

        public CreateDatasetHandler(LiteDatabaseProvider provider)
        {
            _provider = provider;
        }

        public Task<IResult<Dataset>> Handle(CreateDataset request, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                using (var db = _provider(DataContext.Data))
                {
                    var dataset = new Dataset
                    {
                        Name = request.Name
                    };
                    db.GetCollection<Dataset>().Insert(dataset);

                    var fileInfo = db.FileStorage.Upload($"$/datasets/{dataset.Id}", request.FileName, request.FileStream);

                    dataset.FileId = fileInfo.Id;
                    dataset.FileName = fileInfo.Filename;

                    db.GetCollection<Dataset>().Update(dataset);

                    return Result.Success(dataset);
                }
            }, cancellationToken);
        }
    }
}
