using System;
using System.IO;
using FuzzyMatch.Api.Models;
using MediatR.CQRS;

namespace FuzzyMatch.Api.Handlers.Datasets
{
    public class CreateDataset : IAction<Dataset>
    {
        public Stream FileStream { get; set; }
        public String Name { get; set; }
        public String FileName { get; set; }
    }
}
