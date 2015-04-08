using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagement.Views.Query
{
    public class QueryController : Controller
    {

        private InventoryEntities db = new InventoryEntities();
        // GET: Query
        public ActionResult Index()
        {
            IEnumerable<Asset> assets = db.Assets;
            return View(assets);
        }
    }
}