namespace MediatR.CQRS.Requests
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
