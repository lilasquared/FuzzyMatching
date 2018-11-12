using System;

namespace FuzzyMatch.Core.Appends
{
    public class AppendResult
    {
        public Int32 SourceRecordId { get; set; }
        public String SourceRecord { get; set; }
        public Int32 LookupRecordId { get; set; }
        public String LookupRecord { get; set; }
        public Double Ratio { get; set; }
    }
}
