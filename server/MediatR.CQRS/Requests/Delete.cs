using System;

namespace MediatR.CQRS.Requests
{
    public class Delete<TModel> : ICommand
    {
        public Int32 Id { get; }

        public Delete(Int32 id)
        {
            Id = id;
        }
    }
}
