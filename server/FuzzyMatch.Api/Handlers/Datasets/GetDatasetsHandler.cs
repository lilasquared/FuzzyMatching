using FuzzyMatch.Api.Handlers.Generic;
using FuzzyMatch.Core;
using FuzzyMatch.Core.Configuration;

namespace FuzzyMatch.Api.Handlers.Datasets
{
    public class GetDatasetsHandler : GetAllHandler<Dataset>
    {
        public GetDatasetsHandler(LiteDatabaseProvider provider) : base(provider) { }
    }
}
