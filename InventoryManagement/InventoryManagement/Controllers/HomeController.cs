using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace InventoryManagement.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            InventoryEntities db = new InventoryEntities(); 
            string userId = @User.Identity.GetUserId();
            var user = db.AspNetUsers.Where(u=>u.Id == userId).Single();
            
            var assets = db.Assets.Where(u => u.AssetUser.AspNetUserId == userId);
            var permission = user.AspNetRoles.FirstOrDefault();
            if (permission == null)
            {
                Session["elevated"] = false;
            }
            else
            {
                Session["elevated"] = true;
            }
            ViewBag.elevated = Session["elevated"]; 
            return View(assets);

        }

        public ActionResult About()
        {
            ViewBag.Message = "Description page";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact page";

            return View();
        }
    }
}