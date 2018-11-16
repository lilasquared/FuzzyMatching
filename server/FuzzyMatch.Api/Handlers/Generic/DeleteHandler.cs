using System.Threading;
using System.Threading.Tasks;
using FuzzyMatch.Core.UoW;
using MediatR;
using MediatR.CQRS;
using MediatR.CQRS.Requests;

namespace FuzzyMatch.Api.Handlers.Generic
{
    public class DeleteHandler<TModel> : IRequestHandler<Delete<TModel>, IResult<Unit>>
    {
        private readonly DataUnitOfWork _uow;

        public DeleteHandler(DataUnitOfWork uow)
        {
            _uow = uow;
        }

        public Task<IResult<Unit>> Handle(Delete<TModel> request, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                return _uow.Execute(db =>
                {
                    db.GetCollection<TModel>().Delete(request.Id);
                    return Result.Success(Unit.Value);
                });
            }, cancellationToken);
        }
    }
}
