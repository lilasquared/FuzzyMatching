using System.IO;
using System.Threading;
using System.Threading.Tasks;
using FuzzyMatch.Core;
using FuzzyMatch.Core.UoW;
using MediatR;
using MediatR.CQRS;

namespace FuzzyMatch.Api.Handlers.Datasets
{
    public class GetDatasetFileHandler : IRequestHandler<GetDatasetFile, IResult<DatasetFile>>
    {
        private readonly DataUnitOfWork _uow;

        public GetDatasetFileHandler(DataUnitOfWork uow)
        {
            _uow = uow;
        }

        public Task<IResult<DatasetFile>> Handle(GetDatasetFile request, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                return _uow.Execute(db =>
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
                });
            }, cancellationToken);
        }
    }
}
