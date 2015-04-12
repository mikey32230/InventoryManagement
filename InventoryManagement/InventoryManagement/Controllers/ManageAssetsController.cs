﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using InventoryManagement;

namespace InventoryManagement.Controllers
{
    public class ManageAssetsController : Controller
    {
        private InventoryEntities db = new InventoryEntities();

        // GET: ManageAssets
        public ActionResult Index()
        {
            var assets = db.Assets.Include(a => a.AssetModel).Include(a => a.AssetUser);
            return View(assets.ToList());
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
            ViewBag.AssetOwner = new SelectList(db.AssetUsers, "AspNetUserId", "FirstName", asset.AssetOwner);
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
            ViewBag.AssetOwner = new SelectList(db.AssetUsers, "AspNetUserId", "FirstName", asset.AssetOwner);
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