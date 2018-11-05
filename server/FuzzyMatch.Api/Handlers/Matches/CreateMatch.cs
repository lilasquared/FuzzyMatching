using System;
using FuzzyMatch.Api.Models;
using MediatR.CQRS;

namespace FuzzyMatch.Api.Handlers.Matches
{
    public class CreateMatch : IAction<Match>
    {
        public Int32 SourceId { get; set; }
        public Int32 LookupId { get; set; }
        public Double Threshold { get; set; }
    }
}
