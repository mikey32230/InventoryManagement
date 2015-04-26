using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;

namespace InventoryManagement.Controllers
{
    [Authorize(Roles="Admin")]
    public class AssetModelsController : Controller
    {
        private InventoryEntities context = new InventoryEntities();

        // GET: AssetModels
        public ActionResult Index()
        {

            IEnumerable<AssetModel> assetmodels = context.AssetModels.Include(x => x.AssetType);
            return View(assetmodels);
        }

        // GET: AssetModels/Create
        [HttpGet]
        public ActionResult Create()          
        {
            ViewBag.AssetTypesList = new SelectList(context.AssetTypes, "Id","Type");

            return View();
        }

        // POST: AssetModels/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AssetModel assetModel)
        {
            context.AssetModels.Add(assetModel);
            context.SaveChanges();
            return RedirectToAction("Index", "AssetModels");
        }

      
        // GET: AssetModels/EditPartial/5
        public ActionResult EditPartial(int id)
        {
            var assetModel = context.AssetModels.Where(x => x.Id == id).FirstOrDefault();
            ViewBag.AssetTypesList = new SelectList(context.AssetTypes.OrderByDescending(x => x.Id == id), "Id", "Type");

            return View(assetModel);
        }


        // Post: AssetModels/EditPartial/5
        [HttpPost]
        public PartialViewResult EditPartial(AssetModel assetModel)
        {
            AssetModel dbAssetModel = context.AssetModels.Find(assetModel.Id);
            dbAssetModel.Assets = assetModel.Assets;
            dbAssetModel.AssetType = assetModel.AssetType;
            dbAssetModel.Manufacturer = assetModel.Manufacturer;
            dbAssetModel.Name = assetModel.Name;
            dbAssetModel.TypeId = assetModel.TypeId;
            context.SaveChanges();


            return PartialView("~/Views/AssetModels/_tableRow.cshtml", dbAssetModel);
        }

        // GET: AssetModels/Delete/5
        public ActionResult Delete(int id)
        {
            var assetModel = context.AssetModels.Where(x => x.Id == id).FirstOrDefault();
            return View(assetModel);
        }

        // POST: AssetModels/Delete/5
        [HttpPost]
        public ActionResult Delete(AssetModel assetModel)
        {
            context.AssetModels.Attach(assetModel);
            context.AssetModels.Remove(assetModel);
            context.SaveChanges();

            return RedirectToAction("Index", "AssetModels");
        }
    }
}
