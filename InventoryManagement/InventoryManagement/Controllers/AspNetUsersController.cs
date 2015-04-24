using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using InventoryManagement;
using System.Diagnostics; 

namespace InventoryManagement.Controllers
{
    public class AspNetUsersController : Controller
    {
        private InventoryEntities db = new InventoryEntities();

        // GET: AspNetUsers
        public ActionResult Index()
        {
            var aspNetUsers = db.AspNetUsers.Include(a => a.AssetUser).Include(a => a.AspNetRoles); 
            return View(aspNetUsers.ToList());
        }

        public ActionResult Admin(string id) 
        {
            AspNetRole role = db.AspNetRoles.Find(id);
            TempData["RoleData"] = role;
            return RedirectToAction("Edit", "AspNetRoles"); 
        }
        // GET: AspNetUsers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }


        // GET: AspNetUsers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.AssetUsers, "AspNetUserId", "FirstName", aspNetUser.Id);
            ViewBag.AspNetRoles = new SelectList(db.AspNetRoles, "Id", "Name");
            ViewBag.message = aspNetUser.UserName;
            return View(aspNetUser);
        }

      
        // POST: AspNetUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AspNetUser aspNetUser)
        {
           
                // Your code...
                // Could also be before try if you know the exception occurs in SaveChanges

            try
                    {
                
                        AspNetUser dbuser = db.AspNetUsers.Find(aspNetUser.Id);
                        dbuser.Id = aspNetUser.Id;
                        dbuser.Email = aspNetUser.Email;
                        dbuser.EmailConfirmed = aspNetUser.EmailConfirmed;
                        dbuser.PasswordHash = aspNetUser.PasswordHash;
                        dbuser.SecurityStamp = aspNetUser.SecurityStamp;
                        dbuser.PhoneNumberConfirmed = aspNetUser.PhoneNumberConfirmed;
                        dbuser.TwoFactorEnabled = aspNetUser.TwoFactorEnabled;
                        dbuser.LockoutEndDateUtc = aspNetUser.LockoutEndDateUtc;
                        dbuser.LockoutEnabled = aspNetUser.LockoutEnabled;
                        dbuser.AccessFailedCount = aspNetUser.AccessFailedCount;
                        dbuser.UserName = aspNetUser.UserName;
                        
                        db.SaveChanges();
                        return RedirectToAction("Index");
                }
             
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
                ViewBag.Id = new SelectList(db.AssetUsers, "AspNetUserId", "FirstName", aspNetUser.Id); 
                ViewBag.AspNetRoles = new SelectList(db.AspNetRoles, "Id", "Name");
                return View(aspNetUser);
            
            
           
        }

        // GET: AspNetUsers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }

        // POST: AspNetUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            db.AspNetUsers.Remove(aspNetUser);
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
