using System;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace OneC
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
        }

        // remove redirect url if empty
        private const string ReturnUrlRegexPattern = @"(\?ReturnUrl=%2f)$";

        public MvcApplication()
        {
            PreSendRequestHeaders += MvcApplicationOnPreSendRequestHeaders;
        }

        private void MvcApplicationOnPreSendRequestHeaders(object sender, EventArgs e)
        {
            string redirectUrl = Response.RedirectLocation;

            if (string.IsNullOrEmpty(redirectUrl) || !Regex.IsMatch(redirectUrl, ReturnUrlRegexPattern))
            {
                return;
            }

            Response.RedirectLocation = Regex.Replace(redirectUrl,
                ReturnUrlRegexPattern,
                string.Empty);

        }
    }
}
