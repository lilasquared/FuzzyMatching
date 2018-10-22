using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using FuzzyMatch.Api.Models;
using MediatR;
using MediatR.CQRS;

namespace FuzzyMatch.Api.Requests
{
    public class UploadData : IAction<Unit>
    {
        public Int32 DatasetId { get; set; }
        public String FileName { get; set; }
        public Stream FileStream { get; set; }
    }

    public class UploadDataHandler : IRequestHandler<UploadData, IResult<Unit>>
    {
        public Task<IResult<Unit>> Handle(UploadData request, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                using (var db = new LiteDB.LiteDatabase("data.db"))
                {
                    var datasets = db.GetCollection<Dataset>();

                    var dataset = datasets.FindById(request.DatasetId);

                    if (dataset.FileId != null)
                    {
                        db.FileStorage.Delete(dataset.FileId);
                    }

                    var fileInfo = db.FileStorage.Upload($"$/datasets/{request.DatasetId}", request.FileName, request.FileStream);

                    dataset.FileId = fileInfo.Id;
                    datasets.Update(dataset);

                    return Result.Success(Unit.Value);
                }
            }, cancellationToken);
        }
    }
}
