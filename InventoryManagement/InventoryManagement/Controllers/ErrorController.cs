using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagement.Controllers
{
    /// <summary>
    /// ErrorController for handling 404 errors
    /// </summary>
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            //Regular error
            return View();
        }

        public ActionResult Error()
        {
            //Error404
            return View("Error404");
        }
    }
}