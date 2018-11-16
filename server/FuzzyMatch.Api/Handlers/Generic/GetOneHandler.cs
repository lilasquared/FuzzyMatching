using System.Threading;
using System.Threading.Tasks;
using FuzzyMatch.Core.UoW;
using MediatR;
using MediatR.CQRS;
using MediatR.CQRS.Requests;

namespace FuzzyMatch.Api.Handlers.Generic
{
    public class GetOneHandler<TModel> : IRequestHandler<GetOne<TModel>, IResult<TModel>>
    {
        private readonly DataUnitOfWork _uow;

        public GetOneHandler(DataUnitOfWork uow)
        {
            _uow = uow;
        }

        public Task<IResult<TModel>> Handle(GetOne<TModel> request, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                return _uow.Execute(db => Result.Success(db.GetCollection<TModel>().FindById(request.Id)));
            }, cancellationToken);
        }
    }
}
