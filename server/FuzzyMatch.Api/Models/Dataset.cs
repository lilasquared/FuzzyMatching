using System;
using FuzzyMatch.Api.Abstracts;

namespace FuzzyMatch.Api.Models
{
    public class Dataset : IControllable
    {
        public Int32 Id { get; set; }
        public String Name { get; set; }
        public String FileId { get; set; }

        public String GetRoute() => "/api/datasets";
    }
}
