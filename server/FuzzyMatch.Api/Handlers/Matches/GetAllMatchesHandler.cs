using FuzzyMatch.Api.Handlers.Generic;
using FuzzyMatch.Core;
using FuzzyMatch.Core.Appends;
using FuzzyMatch.Core.Configuration;

namespace FuzzyMatch.Api.Handlers.Matches
{
    public class GetAllMatchesHandler : GetAllHandler<Append>
    {
        public GetAllMatchesHandler(LiteDatabaseProvider provider) : base(provider) { }
    }
}
