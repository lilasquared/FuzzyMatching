using System.Threading;
using System.Threading.Tasks;
using FuzzyMatch.Core.Configuration;
using MediatR;
using MediatR.CQRS;
using MediatR.CQRS.Requests;

namespace FuzzyMatch.Api.Handlers.Generic
{
    public class GetOneHandler<TModel> : IRequestHandler<GetOne<TModel>, IResult<TModel>>
    {
        private readonly LiteDatabaseProvider _provider;

        public GetOneHandler(LiteDatabaseProvider provider)
        {
            _provider = provider;
        }

        public Task<IResult<TModel>> Handle(GetOne<TModel> request, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                using (var db = _provider(DataContext.Data))
                {
                    return Result.Success(db.GetCollection<TModel>().FindById(request.Id));
                }
            }, cancellationToken);
        }
    }
}
