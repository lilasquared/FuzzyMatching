using FuzzyMatch.Api.Configuration;
using FuzzyMatch.Api.Handlers.Generic;
using FuzzyMatch.Api.Models;

namespace FuzzyMatch.Api.Handlers.Matches
{
    public class GetAllMatchesHandler : GetAllHandler<Match>
    {
        public GetAllMatchesHandler(DatabaseOptions dbOptions) : base(dbOptions) { }
    }
}
