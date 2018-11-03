using System;
using System.Collections.Generic;
using QuickApi;

namespace FuzzyMatch.Api.Models
{
    [QuickApiRoute("/api/updates")]
    public class Update
    {
        public Int32 Id { get; set; }
        public DateTime Date { get; set; }
        public String Subject { get; set; }
        public String Body { get; set; }
        public IEnumerable<String> Participants { get; set; }
    }
}
