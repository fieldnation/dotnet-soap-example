﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FieldNationApp.Models;

namespace FieldNationApp.Controllers
{
    public class LocationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Locations
        public async Task<ActionResult> Index()
        {
            return View(await db.Locations.ToListAsync());
        }

        // GET: Locations/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkOrderLocation workOrderLocation = await db.Locations.FindAsync(id);
            if (workOrderLocation == null)
            {
                return HttpNotFound();
            }
            return View(workOrderLocation);
        }

        // GET: Locations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Locations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "WorkOrderLocationId,Address,City,State,ZipCode")] WorkOrderLocation workOrderLocation)
        {
            if (ModelState.IsValid)
            {
                db.Locations.Add(workOrderLocation);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(workOrderLocation);
        }

        // GET: Locations/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkOrderLocation workOrderLocation = await db.Locations.FindAsync(id);
            if (workOrderLocation == null)
            {
                return HttpNotFound();
            }
            return View(workOrderLocation);
        }

        // POST: Locations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "WorkOrderLocationId,Address,City,State,ZipCode")] WorkOrderLocation workOrderLocation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workOrderLocation).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(workOrderLocation);
        }

        // GET: Locations/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkOrderLocation workOrderLocation = await db.Locations.FindAsync(id);
            if (workOrderLocation == null)
            {
                return HttpNotFound();
            }
            return View(workOrderLocation);
        }

        // POST: Locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            WorkOrderLocation workOrderLocation = await db.Locations.FindAsync(id);
            db.Locations.Remove(workOrderLocation);
            await db.SaveChangesAsync();
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
