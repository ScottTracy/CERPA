using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CERPA.Models;
using System.Collections;
using System.Web.UI;

namespace CERPA.Controllers
{
    public class PickOrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PickOrders
        public async Task<ActionResult> Index()
        {
            return View(await db.PickOrders.ToListAsync());
        }
        public ActionResult _jobInventory()
        {
            var JobId = (int)Session["JobId"];
            List<PickOrder> pickOrders= db.PickOrders.Where(o => o.Id == JobId).Select(c => c).ToList();
            
            return PartialView(pickOrders.AsEnumerable());
        }
        public ActionResult EmailInventory(string id)
        {
            InventoryEmail mail = new InventoryEmail();
            mail.CreateInventoryEmail(id);
            return RedirectToAction("Index", "Home");
        }
        // GET: PickOrders
        public async Task<ActionResult> _Operations()
        {
            var pickorders= await db.PickOrders.ToListAsync();
            IEnumerable pickOrders = pickorders;
            return View(pickOrders);
        }
        // GET: PickOrders/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PickOrder pickOrder = await db.PickOrders.FindAsync(id);
            if (pickOrder == null)
            {
                return HttpNotFound();
            }
            return View(pickOrder);
        }

        // GET: PickOrders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PickOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,OrderId,PartId,Location,Destination,Start,Confirmed,IsConfirmed,UserId,PartQuantity")] PickOrder pickOrder)
        {
            if (ModelState.IsValid)
            {
                db.PickOrders.Add(pickOrder);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(pickOrder);
        }

        // GET: PickOrders/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PickOrder pickOrder = await db.PickOrders.FindAsync(id);
            if (pickOrder == null)
            {
                return HttpNotFound();
            }
            return View(pickOrder);
        }

        // POST: PickOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,OrderId,PartId,Location,Destination,Start,Confirmed,IsConfirmed,UserId,PartQuantity")] PickOrder pickOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pickOrder).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(pickOrder);
        }

        // GET: PickOrders/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PickOrder pickOrder = await db.PickOrders.FindAsync(id);
            if (pickOrder == null)
            {
                return HttpNotFound();
            }
            return View(pickOrder);
        }

        // POST: PickOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PickOrder pickOrder = await db.PickOrders.FindAsync(id);
            db.PickOrders.Remove(pickOrder);
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
        protected void btnOpenPopupWindow_Click(object sender, EventArgs e)
        {
            int intId = 100;

            string strPopup = "<script language='javascript' ID='script1'>"

            // Passing intId to popup window.
            + "window.open('popup.aspx?data=" + HttpUtility.UrlEncode(intId.ToString())

            + "','new window', 'top=90, left=200, width=300, height=100, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')"

            + "</script>";

            System.Web.UI.ScriptManager.RegisterStartupScript((Page)System.Web.HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);
        }
    }
}
