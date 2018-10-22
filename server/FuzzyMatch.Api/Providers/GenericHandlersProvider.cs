using System;
using System.Collections.Generic;
using StructureMap;

namespace FuzzyMatch.Api.Providers
{
    public static class GenericHandlersProvider
    {
        public static void AddGenericHandlers(this ConfigurationExpression config,
                                              IEnumerable<Type> typesToInject,
                                              IEnumerable<Type> genericHandlers)
        {
            foreach (var type in typesToInject)
            {
                foreach (var handlerType in genericHandlers)
                {
                    var genericType = handlerType.MakeGenericType(type);
                    config.For(genericType).Use(genericType);
                }
            }
        }
    }
}
