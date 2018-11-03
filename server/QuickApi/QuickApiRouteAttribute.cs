using System;

namespace QuickApi
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class QuickApiRouteAttribute : Attribute
    {
        public String BaseRoute { get; }

        public QuickApiRouteAttribute(String baseRoute)
        {
            BaseRoute = baseRoute;
        }
    }
}