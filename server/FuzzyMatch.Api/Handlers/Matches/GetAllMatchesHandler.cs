using FuzzyMatch.Api.Handlers.Generic;
using FuzzyMatch.Core.Appends;
using FuzzyMatch.Core.UoW;

namespace FuzzyMatch.Api.Handlers.Matches
{
    public class GetAllMatchesHandler : GetAllHandler<Append>
    {
        public GetAllMatchesHandler(DataUnitOfWork uow) : base(uow) { }
    }
}
