using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using InventoryManagement;
using InventoryManagement.Models;
using InventoryManagement.Data;

namespace InventoryManagement.Controllers
{
   // [Authorize(Roles = "Admin")]
    public class ManageAssetsController : Controller
    {
        private InventoryEntities db = new InventoryEntities();
        private AssetService service = new AssetService(); 

        // GET: ManageAssets
        public ActionResult Index()
        {
            var assets = db.Assets.Include(a => a.AssetModel).Include(a => a.AssetUser).Include(a=>a.AssetModel.AssetType);

            ViewBag.AssetModelId = new SelectList(db.AssetModels, "Id", "Name");
            ViewBag.AssetTypeId = new SelectList(db.AssetTypes, "Id", "Type"); 
           
            return View(assets);

        }

        [HttpPost]
        // Post: ManageAssets
        public ActionResult Index(AssetQuery query)
        {
            
            var assets = service.searchAssets(query);
            ViewBag.AssetModelId = new SelectList(db.AssetModels, "Id", "Name");
            ViewBag.AssetTypeId = new SelectList(db.AssetTypes, "Id", "Type");
          
            return View(assets);
        }

        // GET: ManageAssets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asset asset = db.Assets.Find(id);
            if (asset == null)
            {
                return HttpNotFound();
            }
            return View(asset);
        }

        // GET: ManageAssets/Create
        public ActionResult Create()
        {
            ViewBag.AssetModelId = new SelectList(db.AssetModels, "Id", "Name");
            ViewBag.AssetOwner = new SelectList(db.AssetUsers, "AspNetUserId", "FirstName");

            ViewBag.AssetTypesList = new SelectList(db.AssetTypes, "Id", "Type"); 
            return View();
        }

        // POST: ManageAssets/Create  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AssetModelId,PurchaseDate,SerialNumber,RoomNum,AssetOwner")] Asset asset)
        {
            if (ModelState.IsValid)
            {
                db.Assets.Add(asset);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AssetModelId = new SelectList(db.AssetModels, "Id", "Name", asset.AssetModelId);
            ViewBag.AssetOwner = new SelectList(db.AssetUsers, "AspNetUserId", "FirstName", asset.AssetOwner);

            ViewBag.AssetTypes = new SelectList(db.AssetTypes, "Id", "Type"); 
            return View(asset);
        }


        // GET: ManageAssets/EditPARTIAL/5
        public ActionResult _EditPartial(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asset asset = db.Assets.Find(id);
            if (asset == null)
            {
                return HttpNotFound();
            }
            ViewBag.AssetModelId = new SelectList(db.AssetModels, "Id", "Name", asset.AssetModelId);
            return View(asset);
        }

        // POST: ManageAssets/EditPARTIAL/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult  _EditPartial(Asset asset)
        {
            if (ModelState.IsValid)
            {
                db.Entry(asset).State = EntityState.Modified;
                db.SaveChanges();
                db.ChangeTracker.DetectChanges();

                //Elusive fix for navigation property woes 
                if(db.Entry(asset).Reference(x=>x.AssetModel).IsLoaded == false)
                {
                    db.Entry(asset).Reference(x => x.AssetModel).Load(); 
                }
                if (db.Entry(asset).Reference(x => x.AssetUser).IsLoaded == false)
                {
                    db.Entry(asset).Reference(x => x.AssetUser).Load();
                }
               return PartialView("~/Views/ManageAssets/_AssetRow.cshtml", asset); 
           } 
            
            //ViewBag.AssetModelId = new SelectList(db.AssetModels, "Id", "Name", asset.AssetModelId);
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        // GET: ManageAssets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asset asset = db.Assets.Find(id);
            if (asset == null)
            {
                return HttpNotFound();
            }
            ViewBag.AssetModelId = new SelectList(db.AssetModels, "Id", "Name", asset.AssetModelId);
            return View(asset);
        }

        // POST: ManageAssets/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AssetModelId,PurchaseDate,SerialNumber,RoomNum,AssetOwner")] Asset asset)
        {
            if (ModelState.IsValid)
            {
                db.Entry(asset).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AssetModelId = new SelectList(db.AssetModels, "Id", "Name", asset.AssetModelId);
            return View(asset);
        }

        // GET: ManageAssets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asset asset = db.Assets.Find(id);
            if (asset == null)
            {
                return HttpNotFound();
            }
            return View(asset);
        }

        // POST: ManageAssets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Asset asset = db.Assets.Find(id);
            db.Assets.Remove(asset);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: ManageAssets/Reassign/5
        public ActionResult Reassign(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asset asset = db.Assets.Find(id);
            if (asset == null)
            {
                return HttpNotFound();
            }
            ViewBag.AssetOwner = new SelectList(db.AssetUsers, "AspNetUserId", "FirstName", asset.AssetOwner);
            return View(asset);
        }

        // POST: ManageAssets/Reassign/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reassign ([Bind(Include = "Id, AssetModelId, PurchaseDate, SerialNumber, RoomNum, AssetOwner")]Asset asset)
        {
            if (ModelState.IsValid)
            {
                db.Entry(asset).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);  
        }

        public ActionResult getOwners(String term)
        {

            var filteredNames = service.getOwners(term);
            return Json(filteredNames, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
