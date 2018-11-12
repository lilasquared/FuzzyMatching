using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FuzzyMatch.Core.Configuration;
using MediatR;
using MediatR.CQRS;
using MediatR.CQRS.Requests;

namespace FuzzyMatch.Api.Handlers.Generic
{
    public class GetAllHandler<TModel> : IRequestHandler<GetAll<TModel>, IResult<IEnumerable<TModel>>>
    {
        private readonly LiteDatabaseProvider _provider;

        public GetAllHandler(LiteDatabaseProvider provider)
        {
            _provider = provider;
        }

        public Task<IResult<IEnumerable<TModel>>> Handle(GetAll<TModel> request, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                using (var db = _provider(DataContext.Data))
                {
                    return Result.Success(db.GetCollection<TModel>().FindAll());
                }
            }, cancellationToken);
        }
    }
}
