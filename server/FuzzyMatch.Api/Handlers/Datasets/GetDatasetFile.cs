using System;
using FuzzyMatch.Api.Models;
using MediatR.CQRS;

namespace FuzzyMatch.Api.Handlers.Datasets
{
    public class GetDatasetFile : IAction<DatasetFile>
    {
        public Int32 Id { get; }

        public GetDatasetFile(Int32 id)
        {
            Id = id;
        }
    }
}
