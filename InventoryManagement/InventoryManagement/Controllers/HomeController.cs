using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace InventoryManagement.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            InventoryEntities db = new InventoryEntities(); 
            string userId = @User.Identity.GetUserId();
            var assets = db.Assets.Where(u => u.AssetUser.AspNetUserId == userId); 
            return View(assets);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}