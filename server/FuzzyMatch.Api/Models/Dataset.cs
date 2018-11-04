using System;
using System.IO;

namespace FuzzyMatch.Api.Models
{
    public class Dataset
    {
        public Int32 Id { get; set; }
        public String Name { get; set; }
        public String FileId { get; set; }
        public String FileName { get; set; }
    }

    public class DatasetFile
    {
        public String Id { get; set; }
        public String Name { get; set; }
        public Stream Contents { get; set; }
    }
}
