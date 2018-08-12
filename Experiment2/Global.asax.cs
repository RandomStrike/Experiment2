using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Experiment2
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }

        void Session_Start(object sender, EventArgs e)
        {
            DateTime dtStart = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime dtEnd = dtStart.AddDays(-1);
            dtStart = dtStart.AddMonths(-1);

            HttpContext.Current.Session.Add("StartDate", dtStart);
            HttpContext.Current.Session.Add("EndDate", dtEnd);

        }
    }
}
