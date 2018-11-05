using FuzzyMatch.Api.Configuration;
using FuzzyMatch.Api.Handlers.Generic;
using FuzzyMatch.Api.Models;

namespace FuzzyMatch.Api.Handlers.Matches
{
    public class DeleteMatchHandler : DeleteHandler<Match>
    {
        public DeleteMatchHandler(DatabaseOptions dbOptions) : base(dbOptions) { }
    }
}
