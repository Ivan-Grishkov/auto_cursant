using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace AutoCadet.Attributes
{
    public class RequireRequestValueAttribute : ActionMethodSelectorAttribute
    {
        private readonly IList<string> _listActions = new List<string> { "Instructors", "Training", "VideoLessons" };
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
            if (string.IsNullOrWhiteSpace(ValueName)
                && prettyFromUrl == null
                && _listActions.Select(x => x.ToLower()).Contains(actionFromUrl?.ToString().ToLower()))
            {
                return true;
            }

            return false;
        }
        public string ValueName { get; private set; }
    }
}