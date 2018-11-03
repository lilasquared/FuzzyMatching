using System.Threading;
using System.Threading.Tasks;
using FuzzyMatch.Api.Configuration;
using FuzzyMatch.Api.Models;
using MediatR;
using MediatR.CQRS;
using MediatR.CQRS.Requests;

namespace FuzzyMatch.Api.Handlers.Datasets
{
    public class DeleteDatasetHandler : IRequestHandler<Delete<Dataset>, IResult<Unit>>
    {
        private readonly DatabaseOptions _dbOptions;

        public DeleteDatasetHandler(DatabaseOptions dbOptions)
        {
            _dbOptions = dbOptions;
        }

        public Task<IResult<Unit>> Handle(Delete<Dataset> request, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                using (var db = new LiteDB.LiteDatabase(_dbOptions.Path))
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
