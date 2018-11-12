using System;
using MediatR.CQRS;

namespace FuzzyMatch.Api.Handlers.Matches
{
    public class StartMatch : ICommand
    {
        public Int32 MatchId { get; }

        public StartMatch(Int32 matchId)
        {
            MatchId = matchId;
        }
    }
}
