using FuzzyMatch.Api.Handlers.Generic;
using FuzzyMatch.Core;
using FuzzyMatch.Core.Appends;
using FuzzyMatch.Core.Configuration;

namespace FuzzyMatch.Api.Handlers.Matches
{
    public class GetOneMatchHandler : GetOneHandler<Append>
    {
        public GetOneMatchHandler(LiteDatabaseProvider provider) : base(provider) { }
    }
}
