using System;
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
    public class WorkOrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: WorkOrders
        public async Task<ActionResult> Index()
        {
            return View(await db.WorkOrders.ToListAsync());
        }

        // GET: WorkOrders/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkOrder workOrder = await db.WorkOrders.FindAsync(id);
            if (workOrder == null)
            {
                return HttpNotFound();
            }
            return View(workOrder);
        }

        // GET: WorkOrders/Create
        public ActionResult Create()
        {
            var model = new WorkOrderViewModel
            {
                WorkOrder = new WorkOrder(),
                WorkOrderLocationItems = new SelectList(db.Locations.ToList(), "WorkOrderLocationId", "Address")
            };

            return View(model);
        }

        // POST: WorkOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(WorkOrderViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var location = db.Locations.Find(vm.WorkOrder.WorkOrderLocationId);
                vm.WorkOrder.WorkOrderLocation = location;
                db.WorkOrders.Add(vm.WorkOrder);
                await db.SaveChangesAsync();
                var fnResponse = await vm.WorkOrder.CreateOnFieldNation();
                return RedirectToAction("Index");
            }

            return View(vm);
        }

        // GET: WorkOrders/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkOrder workOrder = await db.WorkOrders.FindAsync(id);
            if (workOrder == null)
            {
                return HttpNotFound();
            }
            return View(workOrder);
        }

        // POST: WorkOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "WorkOrderId,Title,Description,Instructions,StartTime,EndTime")] WorkOrder workOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workOrder).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(workOrder);
        }

        // GET: WorkOrders/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkOrder workOrder = await db.WorkOrders.FindAsync(id);
            if (workOrder == null)
            {
                return HttpNotFound();
            }
            return View(workOrder);
        }

        // POST: WorkOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            WorkOrder workOrder = await db.WorkOrders.FindAsync(id);
            db.WorkOrders.Remove(workOrder);
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
