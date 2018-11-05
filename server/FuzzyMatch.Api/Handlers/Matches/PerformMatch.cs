using System;
using MediatR.CQRS;

namespace FuzzyMatch.Api.Handlers.Matches
{
    public class PerformMatch : ICommand
    {
        public Int32 Id { get; set; }

        public PerformMatch(Int32 id)
        {
            Id = id;
        }
    }
}
