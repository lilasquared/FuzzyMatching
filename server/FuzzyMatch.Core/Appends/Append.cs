using System;
using System.Collections.Generic;

namespace FuzzyMatch.Core.Appends
{
    public class Append
    {
        public Int32 Id { get; set; }
        public Int32 SourceId { get; set; }
        public Int32 LookupId { get; set; }
        public Double Threshold { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime CompletedAt { get; set; }
        public Double Progress { get; set; }
        public AppendStatus Status { get; set; }
        public IList<AppendResult> Results { get; set; }

        public Append()
        {
            Status = AppendStatus.Created;
            CreatedAt = DateTime.UtcNow;
            Results = new List<AppendResult>();
        }

        public void Start()
        {
            StartedAt = DateTime.UtcNow;
            Status = AppendStatus.Processing;
        }

        public void Finish()
        {
            CompletedAt = DateTime.UtcNow;
            Status = AppendStatus.Completed;
        }
    }
}
