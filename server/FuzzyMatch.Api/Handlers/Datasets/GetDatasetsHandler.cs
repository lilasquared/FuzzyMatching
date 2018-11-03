using FuzzyMatch.Api.Configuration;
using FuzzyMatch.Api.Handlers.Generic;
using FuzzyMatch.Api.Models;

namespace FuzzyMatch.Api.Handlers.Datasets
{
    public class GetDatasetsHandler : GetAllHandler<Dataset>
    {
        public GetDatasetsHandler(DatabaseOptions dbOptions) : base(dbOptions) { }
    }
}
