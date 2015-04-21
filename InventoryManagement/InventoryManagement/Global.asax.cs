﻿using InventoryManagement.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace InventoryManagement
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

        protected void Application_Error(object sender, EventArgs e)
        {
            Server.ClearError();
            var routeData = new RouteData();
            routeData.Values["controller"] = "Error";

            if ((Context.Server.GetLastError() is HttpException) && ((Context.Server.GetLastError() as HttpException).GetHttpCode() != 404))
            {
                Response.ContentType = "text/html";
                routeData.Values["action"] = "Index";
            }
            else
            {
                // Handle 404 error and response code
                Response.ContentType = "text/html";
                Response.StatusCode = 404;
                routeData.Values["action"] = "Error";
            }
            Response.TrySkipIisCustomErrors = true; // If you are using IIS7, have this line
            IController errorsController = new ErrorController();
            HttpContextWrapper wrapper = new HttpContextWrapper(Context);
            var rc = new System.Web.Routing.RequestContext(wrapper, routeData);
            errorsController.Execute(rc);
        }

    }
}
