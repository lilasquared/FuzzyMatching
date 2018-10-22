using System.Collections.Generic;

namespace MediatR.CQRS.Requests
{
    public class GetAll<TModel> : IAction<IEnumerable<TModel>> { }
}
