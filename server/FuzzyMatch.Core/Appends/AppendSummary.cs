using System;

namespace FuzzyMatch.Core.Appends
{
    public class AppendSummary
    {
        public Int32 Id { get; set; }
        public Int32 SourceId { get; set; }
        public Int32 LookupId { get; set; }
        public Double Threshold { get; set; }
        public DateTime CreatedAt { get; set; }
        public Double Progress { get; set; }
        public String Status { get; set; }

        public static AppendSummary FromMatch(Append append)
        {
            return new AppendSummary
            {
                Id = append.Id,
                SourceId = append.SourceId,
                LookupId = append.LookupId,
                Threshold = append.Threshold,
                CreatedAt = append.CreatedAt,
                Progress = append.Progress,
                Status = append.Status.ToString()
            };
        }
    }
}
