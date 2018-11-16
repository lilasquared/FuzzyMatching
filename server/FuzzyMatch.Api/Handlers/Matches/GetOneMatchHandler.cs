using FuzzyMatch.Api.Handlers.Generic;
using FuzzyMatch.Core.Appends;
using FuzzyMatch.Core.UoW;

namespace FuzzyMatch.Api.Handlers.Matches
{
    public class GetOneMatchHandler : GetOneHandler<Append>
    {
        public GetOneMatchHandler(DataUnitOfWork uow) : base(uow) { }
    }
}
