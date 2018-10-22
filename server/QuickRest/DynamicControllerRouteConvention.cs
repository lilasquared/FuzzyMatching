using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace QuickRest
{
    public class DynamicControllerRouteConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            if (!controller.ControllerType.IsGenericType) return;

            var genericArg = controller.ControllerType.GenericTypeArguments[0];
            var attribute = genericArg.GetCustomAttributes<QuickRouteAttribute>().FirstOrDefault();
            if (attribute == null) return;

            controller.Selectors[0].AttributeRouteModel =
                new AttributeRouteModel(new RouteAttribute(attribute.BaseRoute));
        }
    }
}
