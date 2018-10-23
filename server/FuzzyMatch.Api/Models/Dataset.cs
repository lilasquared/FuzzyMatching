using System;
using QuickApi;

namespace FuzzyMatch.Api.Models
{
    [QuickApiRoute(BaseRoute = "/api/datasets")]
    public class Dataset
    {
        public Int32 Id { get; set; }
        public String Name { get; set; }
        public String FileId { get; set; }
    }
}
