using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using FuzzyMatch.Api.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace FuzzyMatch.Api.Providers
{
    public class DynamicControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        private readonly IEnumerable<Type> _types;

        public DynamicControllerFeatureProvider(IEnumerable<IControllable> controllables)
        {
            _types = controllables.Select(x => x.GetType());
        }

        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            foreach (var type in _types)
            {
                var controllerType = typeof(BaseController<>)
                    .MakeGenericType(type);

                TypeDescriptor.AddAttributes(controllerType, new ApiExplorerSettingsAttribute
                {
                    GroupName = type.Name
                });

                feature.Controllers.Add(controllerType.GetTypeInfo());
            }
        }
    }
}