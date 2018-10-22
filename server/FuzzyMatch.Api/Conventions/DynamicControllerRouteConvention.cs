using System;
using FuzzyMatch.Api.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace FuzzyMatch.Api.Conventions
{
    public class DynamicControllerRouteConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            if (!controller.ControllerType.IsGenericType) return;

            var genericType = controller.ControllerType.GenericTypeArguments[0];

            if (Activator.CreateInstance(genericType) is IControllable controllable)
            {
                controller.Selectors[0].AttributeRouteModel =
                    new AttributeRouteModel(new RouteAttribute(controllable.GetRoute()));
            }
        }
    }
}