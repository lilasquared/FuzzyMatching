using System.Threading;
using System.Threading.Tasks;
using FuzzyMatch.Api.Configuration;
using FuzzyMatch.Api.Models;
using MediatR;
using MediatR.CQRS;

namespace FuzzyMatch.Api.Handlers.Datasets
{
    public class CreateDatasetHandler : IRequestHandler<CreateDataset, IResult<Dataset>>
    {
        private readonly DatabaseOptions _dbOptions;

        public CreateDatasetHandler(DatabaseOptions dbOptions)
        {
            _dbOptions = dbOptions;
        }

        public Task<IResult<Dataset>> Handle(CreateDataset request, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                using (var db = new LiteDB.LiteDatabase(_dbOptions.Path))
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
