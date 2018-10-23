using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace QuickApi
{
    public static class MvcBuilderExtensions
    {
        public static IMvcBuilder AddQuickApi(this IMvcBuilder builder,
                                              Type controllerType,
                                              params Assembly[] assemblies)
        {
            return builder.AddQuickApi(controllerType, assemblies.AsEnumerable());
        }

        public static IMvcBuilder AddQuickApi(this IMvcBuilder builder,
                                              Type controllerType,
                                              IEnumerable<Assembly> assemblies)
        {
            var modelTypes = assemblies.SelectMany(x => x.GetQuickApiTypes());

            return builder.AddQuickApi(controllerType, modelTypes);
        }

        public static IMvcBuilder AddQuickApi(this IMvcBuilder builder,
                                              Type controllerType,
                                              params Type[] modelTypes)
        {
            return builder.AddQuickApi(controllerType, modelTypes.AsEnumerable());
        }

        public static IMvcBuilder AddQuickApi(this IMvcBuilder builder,
                                              Type controllerType,
                                              IEnumerable<Type> modelTypes)
        {
            builder.AddMvcOptions(options => options.Conventions.Add(new QuickApiControllerRouteConvention()));

            var featureProvider = new QuickApiControllerFeatureProvider(controllerType, modelTypes);
            builder.ConfigureApplicationPartManager(m => m.FeatureProviders.Add(featureProvider));

            return builder;
        }
    }
}
