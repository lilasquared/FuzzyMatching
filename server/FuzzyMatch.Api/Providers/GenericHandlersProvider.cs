using System;
using System.Collections.Generic;
using System.Linq;
using FuzzyMatch.Api.Abstracts;
using FuzzyMatch.Api.GenericHandlers;
using StructureMap;

namespace FuzzyMatch.Api.Providers
{
    public static class GenericHandlersProvider
    {
        private static readonly Type[] HandlerTypes = new[]
        {
            typeof(GetAllHandler<>),
            typeof(GetOneHandler<>),
            typeof(CreateHandler<>),
            typeof(DeleteHandler<>)
        };

        public static void AddGenericHandlers(this ConfigurationExpression config,
                                              IEnumerable<IControllable> controllables)
        {
            var controllableTypes = controllables.Select(x => x.GetType());

            foreach (var type in controllableTypes)
            {
                foreach (var handlerType in HandlerTypes)
                {
                    var genericType = handlerType.MakeGenericType(type);
                    config.For(genericType).Use(genericType);
                }
            }
        }
    }
}
