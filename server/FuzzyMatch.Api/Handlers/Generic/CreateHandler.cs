using System.Threading;
using System.Threading.Tasks;
using FuzzyMatch.Core.Configuration;
using MediatR;
using MediatR.CQRS;
using MediatR.CQRS.Requests;

namespace FuzzyMatch.Api.Handlers.Generic
{
    public class CreateHandler<TModel> : IRequestHandler<Create<TModel>, IResult<TModel>>
    {
        private readonly LiteDatabaseProvider _provider;

        public CreateHandler(LiteDatabaseProvider provider)
        {
            _provider = provider;
        }

        public Task<IResult<TModel>> Handle(Create<TModel> request, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                using (var db = _provider(DataContext.Data))
                {
                    db.GetCollection<TModel>().Insert(request.Model);
                    return Result.Success(request.Model);
                }
            }, cancellationToken);
        }
    }
}
