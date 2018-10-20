using MediatR.CQRS;

namespace FuzzyMatch.Api.Requests
{
    public class Create<TModel> : IAction<TModel>
    {
        public TModel Model { get; }

        public Create(TModel model)
        {
            Model = model;
        }
    }
}
