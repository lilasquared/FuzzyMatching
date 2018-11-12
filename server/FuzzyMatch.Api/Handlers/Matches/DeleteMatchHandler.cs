using FuzzyMatch.Api.Handlers.Generic;
using FuzzyMatch.Core;
using FuzzyMatch.Core.Appends;
using FuzzyMatch.Core.Configuration;

namespace FuzzyMatch.Api.Handlers.Matches
{
    public class DeleteMatchHandler : DeleteHandler<Append>
    {
        public DeleteMatchHandler(LiteDatabaseProvider provider) : base(provider) { }
    }
}
