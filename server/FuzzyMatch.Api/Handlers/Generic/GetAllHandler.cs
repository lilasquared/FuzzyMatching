using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FuzzyMatch.Core.UoW;
using MediatR;
using MediatR.CQRS;
using MediatR.CQRS.Requests;

namespace FuzzyMatch.Api.Handlers.Generic
{
    public class GetAllHandler<TModel> : IRequestHandler<GetAll<TModel>, IResult<IEnumerable<TModel>>>
    {
        private readonly DataUnitOfWork _uow;

        public GetAllHandler(DataUnitOfWork uow)
        {
            _uow = uow;
        }

        public Task<IResult<IEnumerable<TModel>>> Handle(GetAll<TModel> request, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                return _uow.Execute(db => Result.Success(
                                             db.GetCollection<TModel>().FindAll()));
            }, cancellationToken);
        }
    }
}
