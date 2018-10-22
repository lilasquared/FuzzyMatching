using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace FuzzyMatch.Api.Providers
{
    public class DynamicControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        private readonly Type _baseController;
        private readonly IEnumerable<Type> _types;

        public DynamicControllerFeatureProvider(Type baseController, IEnumerable<Type> types)
        {
            _baseController = baseController;
            _types = types;
        }

        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            foreach (var type in _types)
            {
                var controllerType = _baseController.MakeGenericType(type);

                TypeDescriptor.AddAttributes(controllerType, new ApiExplorerSettingsAttribute
                {
                    GroupName = type.Name
                });

                feature.Controllers.Add(controllerType.GetTypeInfo());
            }
        }
    }
}
