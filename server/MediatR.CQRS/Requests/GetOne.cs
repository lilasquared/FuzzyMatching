using System;

namespace MediatR.CQRS.Requests
{
    public class GetOne<TModel> : IAction<TModel>
    {
        public Int32 Id { get; }

        public GetOne(Int32 id)
        {
            Id = id;
        }
    }
}