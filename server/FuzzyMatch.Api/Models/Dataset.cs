using System;
using QuickRest;

namespace FuzzyMatch.Api.Models
{
    [QuickRoute(BaseRoute = "/api/datasets")]
    public class Dataset
    {
        public Int32 Id { get; set; }
        public String Name { get; set; }
        public String FileId { get; set; }
    }
}
