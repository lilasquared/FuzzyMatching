using FuzzyMatch.Api.Handlers.Generic;
using FuzzyMatch.Core;
using FuzzyMatch.Core.UoW;

namespace FuzzyMatch.Api.Handlers.Datasets
{
    public class GetDatasetsHandler : GetAllHandler<Dataset>
    {
        public GetDatasetsHandler(DataUnitOfWork uow) : base(uow) { }
    }
}
