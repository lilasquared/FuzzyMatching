using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace QuickRest
{
    public static class AssemblyExtensions
    {
        public static IEnumerable<Type> GetQuickApiTypes(this Assembly assembly)
        {
            return assembly.GetExportedTypes()
                .Where(x => x.GetCustomAttributes<QuickRouteAttribute>().Any());
        }
    }
}
