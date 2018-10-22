using System;
using System.Collections.Generic;

namespace MediatR.CQRS
{
    public interface IControllable
    {
        String GetRoute();
    }

    public interface ICustomizable
    {
        IDictionary<String, Action<IRequest>> GetRoutes();
    }
}
