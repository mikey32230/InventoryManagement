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


namespace InventoryManagement.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ManageAssetsController : Controller
    {
        private InventoryEntities db = new InventoryEntities();


        // GET: ManageAssets
        public ActionResult Index()
        {
            var assets = db.Assets.Include(a => a.AssetModel).Include(a => a.AssetUser);
            //Option to use ViewModel - Coded before learning about DisplayTemplates

            //List<AssetViewModel> viewModel = new List<AssetViewModel>();  
            //foreach(Asset a in assets)
            //{
            //    var toAddViewModel = new AssetViewModel();
            //    toAddViewModel.asset = a; 
            //    viewModel.Add(toAddViewModel); 
            //}
            return View(assets);

        }

        [HttpPost]
        // Post: ManageAssets
        public ActionResult Index(String serialNumber, String modelName, String owner, String roomNumber, DateTime? purchaseDate)
        {
            var assets = db.Assets.Include(a => a.AssetModel).Include(a => a.AssetUser);

            if (!String.IsNullOrEmpty(serialNumber))
            {
                assets = assets.Where(a => a.SerialNumber == serialNumber);
            }
            if (!String.IsNullOrEmpty(modelName))
            {
                assets = assets.Where(a => a.AssetModel.Name == modelName);
            }
            if (!String.IsNullOrEmpty(owner))
            {
                
                //Use .split to separate first and last name 
                //Trim whitespace 
                var stringPartition = owner.Split(',');
                String lName = stringPartition[0].Trim();
                assets = assets.Where(a=>a.AssetUser.LastName.StartsWith(lName)); 
                if(stringPartition.Length >=2)
                {
                    String fNAme = stringPartition[1].Trim();
                    assets = assets.Where(a=>a.AssetUser.FirstName.StartsWith(fNAme));
                }
                
            }
            if (!String.IsNullOrEmpty(roomNumber))
            {
                assets = assets.Where(a => a.RoomNum == roomNumber);
            }
            if (purchaseDate != null)
            {
                //Remove the Time portion of the DateTime 
                assets = assets.Where(a => DbFunctions.TruncateTime(a.PurchaseDate) == DbFunctions.TruncateTime(purchaseDate));
            }

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
            return View();
        }

        // POST: ManageAssets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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
            return View(asset);
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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
            //ViewBag.AssetOwner = new SelectList(db.AssetUsers, "AspNetUserId", "FirstName", asset.AssetOwner);
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

        // POST: ManageAssets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reassign([Bind(Include = "Id,AssetModelId,PurchaseDate,SerialNumber,RoomNum,AssetOwner")] Asset asset)
        {
            if (ModelState.IsValid)
            {
                db.Entry(asset).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.AssetOwner = new SelectList(db.AssetUsers, "AspNetUserId", "FirstName", asset.AssetOwner);
            return View(asset);
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
