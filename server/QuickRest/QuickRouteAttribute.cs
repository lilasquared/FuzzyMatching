using System;

namespace QuickRest
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class QuickRouteAttribute : Attribute
    {
        public String BaseRoute { get; set; }
    }
}