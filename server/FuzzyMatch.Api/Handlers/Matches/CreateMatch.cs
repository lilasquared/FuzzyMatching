using System;
using FuzzyMatch.Core.Appends;
using MediatR.CQRS;

namespace FuzzyMatch.Api.Handlers.Matches
{
    public class CreateMatch : IAction<Append>
    {
        public Int32 SourceId { get; set; }
        public Int32 LookupId { get; set; }
        public Double Threshold { get; set; }
    }
}
