using System;
using MediatR.CQRS;

namespace FuzzyMatch.Core.Appends
{
    public class PerformAppend : ICommand, IQueueable
    {
        public Int32 Id { get; set; }
        public Int32 MatchId { get; set; }
    }
}
