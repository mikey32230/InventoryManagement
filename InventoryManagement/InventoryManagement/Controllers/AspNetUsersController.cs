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
using InventoryManagement.Models;

namespace InventoryManagement.Controllers
{

    [Authorize(Roles = "Admin")]
    public class AspNetUsersController : Controller
    {
        private InventoryEntities db = new InventoryEntities();

        // GET: AspNetUsers
        public ActionResult Index()
        {
            var aspNetUsers = db.AspNetUsers.Include(a => a.AssetUser).Include(a => a.AspNetRoles);
            return View(aspNetUsers.ToList());
        }

        // GET: AspNetUsers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IEnumerable<SelectListItem> roles = new SelectList(db.AspNetRoles, "Id", "Name");
            var viewModel = new EditRolesViewModel();
            viewModel.roles = roles;
            viewModel.userId = id;

            return View(viewModel);

        }


        // POST: AspNetUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(EditRolesViewModel viewModel)
        {
            var user = db.AspNetUsers.Find(viewModel.userId);
            var roleToAdd = db.AspNetRoles.Find(viewModel.roleSelection);
            user.AspNetRoles.Add(roleToAdd);
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
