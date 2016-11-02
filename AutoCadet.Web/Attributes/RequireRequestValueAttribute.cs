using System.Reflection;
using System.Web.Mvc;

namespace AutoCadet.Attributes
{
    public class RequireRequestValueAttribute : ActionMethodSelectorAttribute
    {
        public RequireRequestValueAttribute(string valueName)
        {
            ValueName = valueName;
        }
        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            var prettyFromUrl = controllerContext.RequestContext.RouteData.Values[ValueName];
            if (prettyFromUrl != null)
            {
                return true;
            }

            // Instructors
            prettyFromUrl = controllerContext.RequestContext.RouteData.Values["prettyUrl"];
            var actionFromUrl = controllerContext.RequestContext.RouteData.Values["action"];
            if (string.IsNullOrWhiteSpace(ValueName) && prettyFromUrl == null && actionFromUrl?.ToString().ToLower() == "Instructors".ToLower())
            {
                return true;
            }

            return false;
        }
        public string ValueName { get; private set; }
    }
}