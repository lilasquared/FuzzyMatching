using System;

namespace QuickApi
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class QuickApiRouteAttribute : Attribute
    {
        public String BaseRoute { get; set; }
    }
}