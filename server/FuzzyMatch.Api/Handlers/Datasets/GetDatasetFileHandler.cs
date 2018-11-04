using System.IO;
using System.Threading;
using System.Threading.Tasks;
using FuzzyMatch.Api.Configuration;
using FuzzyMatch.Api.Models;
using LiteDB;
using MediatR;
using MediatR.CQRS;

namespace FuzzyMatch.Api.Handlers.Datasets
{
    public class GetDatasetFileHandler : IRequestHandler<GetDatasetFile, IResult<DatasetFile>>
    {
        private readonly DatabaseOptions _dbOptions;

        public GetDatasetFileHandler(DatabaseOptions dbOptions)
        {
            _dbOptions = dbOptions;
        }

        public Task<IResult<DatasetFile>> Handle(GetDatasetFile request, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                using (var db = new LiteDatabase(_dbOptions.Path))
                {
                    var dataset = db.GetCollection<Dataset>().FindById(request.Id);
                    var stream = new MemoryStream();
                    db.FileStorage.Download(dataset.FileId, stream);
                    stream.Seek(0, SeekOrigin.Begin);
                    return Result.Success(new DatasetFile
                    {
                        Id = dataset.FileId,
                        Name = dataset.FileName,
                        Contents = stream
                    });
                }
            }, cancellationToken);
        }
    }
}
