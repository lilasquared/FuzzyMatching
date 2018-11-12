using System.Threading;
using System.Threading.Tasks;
using FuzzyMatch.Core.Configuration;
using MediatR;
using MediatR.CQRS;
using MediatR.CQRS.Requests;

namespace FuzzyMatch.Api.Handlers.Generic
{
    public class DeleteHandler<TModel> : IRequestHandler<Delete<TModel>, IResult<Unit>>
    {
        private readonly LiteDatabaseProvider _provider;

        public DeleteHandler(LiteDatabaseProvider provider)
        {
            _provider = provider;
        }

        public Task<IResult<Unit>> Handle(Delete<TModel> request, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                using (var db = _provider(DataContext.Data))
                {
                    db.GetCollection<TModel>().Delete(request.Id);
                    return Result.Success(Unit.Value);
                }
            }, cancellationToken);
        }
    }
}
