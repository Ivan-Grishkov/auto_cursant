using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Canonicalize;
using log4net;

namespace AutoCadet
{
    public class MvcApplication : HttpApplication
    {
        private ILog _log;
        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(Server.MapPath("~/Web.config")));
            _log = LogManager.GetLogger("AutoCadet");
            RouteTable.Routes.Canonicalize().NoWww().Lowercase().NoTrailingSlash();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AntiForgeryConfig.SuppressXFrameOptionsHeader = true;
        }

        /// <summary>
        /// http://stackoverflow.com/questions/878578/how-can-i-have-lowercase-routes-in-asp-net-mvc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //You don't want to redirect on posts, or images/css/js
            bool isGet = HttpContext.Current.Request.RequestType.ToLowerInvariant().Contains("get");
            if (isGet && HttpContext.Current.Request.Url.AbsolutePath.Contains(".") == false)
            {
                string lowercaseUrl = Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.Url.AbsolutePath;
                if (Regex.IsMatch(lowercaseUrl, @"[A-Z]"))
                {
                    //You don't want to change casing on query strings
                    lowercaseUrl = lowercaseUrl.ToLower() + HttpContext.Current.Request.Url.Query;

                    Response.Clear();
                    Response.Status = "301 Moved Permanently";
                    Response.AddHeader("Location", lowercaseUrl);
                    Response.End();
                }

                if (HttpContext.Current.Request.Url.AbsolutePath.ToLower() == "/index")
                {
                    Response.Clear();
                    Response.Status = "301 Moved Permanently";
                    var homeUrl = Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority;
                    Response.AddHeader("Location", homeUrl);
                    Response.End();
                }
            }
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            if (_log == null)
            {
                _log = LogManager.GetLogger("AutoCadet");
            }

            _log.Error(exception);
        }
    }
}
