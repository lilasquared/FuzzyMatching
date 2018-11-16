using System.Threading;
using System.Threading.Tasks;
using FuzzyMatch.Core.UoW;
using MediatR;
using MediatR.CQRS;
using MediatR.CQRS.Requests;

namespace FuzzyMatch.Api.Handlers.Generic
{
    public class CreateHandler<TModel> : IRequestHandler<Create<TModel>, IResult<TModel>>
    {
        private readonly DataUnitOfWork _uow;

        public CreateHandler(DataUnitOfWork uow)
        {
            _uow = uow;
        }

        public Task<IResult<TModel>> Handle(Create<TModel> request, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                return _uow.Execute(db =>
                {
                    db.GetCollection<TModel>().Insert(request.Model);
                    return Result.Success(request.Model);
                });
            }, cancellationToken);
        }
    }
}
