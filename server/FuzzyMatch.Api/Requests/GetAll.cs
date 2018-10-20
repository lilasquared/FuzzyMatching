using System.Collections.Generic;
using MediatR.CQRS;

namespace FuzzyMatch.Api.Requests
{
    public class GetAll<TModel> : IAction<IEnumerable<TModel>> { }
}
