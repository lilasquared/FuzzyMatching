using FuzzyMatch.Api.Handlers.Generic;
using FuzzyMatch.Core.Appends;
using FuzzyMatch.Core.UoW;

namespace FuzzyMatch.Api.Handlers.Matches
{
    public class DeleteMatchHandler : DeleteHandler<Append>
    {
        public DeleteMatchHandler(DataUnitOfWork uow) : base(uow) { }
    }
}
