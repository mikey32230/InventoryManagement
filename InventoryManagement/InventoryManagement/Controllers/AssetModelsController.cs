using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagement.Controllers
{
    [Authorize]
    public class AssetModelsController : Controller
    {
        // GET: AssetModels
        public ActionResult Index()
        {
            InventoryEntities context = new InventoryEntities();
            return View(context.AssetModels);
        }

        // GET: AssetModels/Create
        public ActionResult Create()          
        {
            InventoryEntities context = new InventoryEntities();

            return View(new AssetModel(context.AssetTypes));
        //    return View();
        }

        // POST: AssetModels/Create
        [HttpPost]
        public ActionResult Create(AssetModel am)
        {
            using (var context = new InventoryEntities())
            {
                //var newAssetModel =  new AssetModel()
                //{
                //    AssetType = am.AssetType,
                //    Manufacturer = am.Manufacturer,
                //    Name = am.Name
                //};
                context.AssetModels.Add(am);
                context.SaveChanges();
                return RedirectToAction("Index", "AssetModels");
            }
        }

        // GET: AssetModels/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AssetModels/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: AssetModels/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AssetModels/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
