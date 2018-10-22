using System;

namespace FuzzyMatch.Core.Datasets
{
    public class Dataset : ControllableModel
    {
        public Int32 Id { get; set; }
        public String Name { get; set; }
        public String FileId { get; set; }

        public override String GetRoute() => "/api/datasets";
    }
}
