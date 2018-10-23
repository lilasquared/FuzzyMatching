using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace QuickApi
{
    public class QuickApiControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        private readonly Type _controllerType;
        private readonly IEnumerable<Type> _modelTypes;

        public QuickApiControllerFeatureProvider(Type controllerType,
                                                params Type[] modelTypes)
            : this(controllerType, modelTypes.AsEnumerable()) { }

        public QuickApiControllerFeatureProvider(Type controllerType, IEnumerable<Type> modelTypes)
        {
            if (controllerType == null)
            {
                throw new ArgumentNullException(nameof(controllerType));
            }

            if (controllerType.BaseType != typeof(Controller))
            {
                throw new ArgumentException("Controller type must be extend 'Controller'", nameof(controllerType));
            }

            if (!controllerType.IsGenericType)
            {
                throw new ArgumentException("Controller type must be generic", nameof(controllerType));
            }

            _controllerType = controllerType;
            _modelTypes = modelTypes ?? throw new ArgumentNullException(nameof(modelTypes));
        }

        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            foreach (var type in _modelTypes)
            {
                var controllerType = _controllerType.MakeGenericType(type);

                TypeDescriptor.AddAttributes(controllerType, new ApiExplorerSettingsAttribute
                {
                    GroupName = type.Name
                });

                feature.Controllers.Add(controllerType.GetTypeInfo());
            }
        }
    }
}
