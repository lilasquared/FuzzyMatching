using System;
using System.Collections.Generic;

namespace FuzzyMatch.Api.Models
{
    public enum MatchStatus
    {
        Created,
        Processing,
        Completed,
        Failed
    }

    public class MatchSummary
    {
        public Int32 Id { get; set; }
        public Int32 SourceId { get; set; }
        public Int32 LookupId { get; set; }
        public Double Threshold { get; set; }
        public DateTime CreatedAt { get; set; }
        public String Status { get; set; }

        public static MatchSummary FromMatch(Match match)
        {
            return new MatchSummary
            {
                Id = match.Id,
                SourceId = match.SourceId,
                LookupId = match.LookupId,
                Threshold = match.Threshold,
                CreatedAt = match.CreatedAt,
                Status = match.Status.ToString()
            };
        }
    }

    public class Match
    {
        public Int32 Id { get; set; }
        public Int32 SourceId { get; set; }
        public Int32 LookupId { get; set; }
        public Double Threshold { get; set; }
        public DateTime CreatedAt { get; set; }
        public MatchStatus Status { get; set; }
        public IList<MatchResult> Results { get; set; }

        public Match()
        {
            Status = MatchStatus.Created;
            CreatedAt = DateTime.UtcNow;
            Results = new List<MatchResult>();
        }
    }

    public class MatchResult
    {
        public Int32 SourceRecordId { get; set; }
        public Int32 LookupRecordId { get; set; }
        public Double Ratio { get; set; }
    }
}
