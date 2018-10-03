using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CERPA.Models;
using Microsoft.AspNet.Identity;

namespace CERPA.Controllers
{
    public class InventoryControlController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: InventoryControl
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SearchTransactions(string searchTerm)
        {
            List<InventoryControlViewModel> inventoryControls = new List<InventoryControlViewModel>();
            var users = db.Users.Where(x => x.UserName.Contains(searchTerm) || searchTerm == null).ToList();
            foreach(var user in users)
            {
                var transactions = db.InventoryTansactions.Where(x => x.UserId == user.Id).Select(x => x).ToList();
                foreach (var transaction in transactions)
                {
                    inventoryControls.Add(new InventoryControlViewModel
                    {
                        InventoryTransaction = transaction,
                        InventoryItem = db.Inventory.Where(y => y.PartID == transaction.InventoryItem).Select(y => y).First()
                    });
                }
            }
            var transactionResults = db.InventoryTansactions.Where(z => z.InventoryItem.Contains(searchTerm) || searchTerm == null).ToList();
            foreach(var transaction in transactionResults)
            {
                inventoryControls.Add(new InventoryControlViewModel
                {
                    InventoryTransaction = transaction,
                    InventoryItem = db.Inventory.Where(y => y.PartID == transaction.InventoryItem).Select(y => y).First()
                });
            }
            return View(inventoryControls.GroupBy(t => t.InventoryTransaction.Id).Select(group => group.First()));
        }

        public string GetUserName(int id)
        {
            InventoryTransaction transaction =  db.InventoryTansactions.Where(y => y.Id == id).Select(x => x).First();
            return db.Users.Where(u => u.Id == transaction.UserId).Select(u => u.UserName).First();
        }
        
        public async Task ReduceInventory(Job job)
        {
            await AddPart(job);
            var jobInventory = await GetJobInventoryAsync(job);
            foreach (var item in jobInventory)
            {
                item.Key.Quantity = item.Key.Quantity - item.Value;
                db.Entry(item.Key).State = EntityState.Modified;
                CheckInventoryLevel(item.Key);
                await RecordTransaction(item.Key, job, item.Value,("auto on job confirmation"),"");
                await db.SaveChangesAsync();
            }

        }
        public async Task AddPart(Job job)
        {
            var partId = job.PartID;
            var part = await db.Inventory.Where(x => x.PartID == partId).Select(x => x).FirstAsync();
            part.Quantity++;
            db.Entry(part).State = EntityState.Modified;
            await db.SaveChangesAsync();
            
            await RecordTransaction(part, job, -1, ("auto on job confirmation"),"");

        }
        public async Task<Dictionary<InventoryItem, int>> GetJobInventoryAsync(Job job)
        {
            Dictionary<InventoryItem, int> jobInventory = new Dictionary<InventoryItem, int>();
            var items =await db.PickOrders.Where(x => x.JobId == job.ID).Select(y => y).ToListAsync();
            foreach(var item in items)
            {
                jobInventory.Add(db.Inventory.Where(z => z.PartID == item.PartId).Select(i => i).First(), item.PartQuantity);

            }
            return jobInventory;
        }
        public async Task RecordTransaction(InventoryItem item,Job job,int quantity, string type,string comment)
        {
            int JobID;
            if (job == null)
            {
                JobID = 0;
            }
            else
            {
                 JobID = job.ID;
            }
            InventoryTransaction transaction = new InventoryTransaction()
            {
                InventoryItem = item.PartID,
                UserId = System.Web.HttpContext.Current.User.Identity.GetUserId(),
                JobId =JobID,
                TimeStamp=DateTime.Now,
                TransactionType=type,
                Quantity= quantity,
                Comment=comment
            };
            db.InventoryTansactions.Add(transaction);
            await db.SaveChangesAsync();
        }
        public void CheckInventoryLevel(InventoryItem item)
        {
            if (item.Quantity <= item.ReorderPoint&&(item.Location!=null))
            {
                NotifyInventoryControl(item);
            }
            return;
        }
        public void NotifyInventoryControl(InventoryItem item)
        {
            string subject = "Part " + item.PartID + " is below reorder point";
            string body = subject + " of " + item.ReorderPoint + ". There are currently " + item.Quantity + " pieces left.";
            Email.SendEmail("scotttesttracy@gmail.com", subject, body);
        }
        // GET: InventoryItems/Edit/5
        public async Task<ActionResult> EditQuantity(int id)
        {
            
            InventoryItem inventoryItem = await db.Inventory.FindAsync(id);
            Session["item"] = inventoryItem;
            if (inventoryItem == null)
            {
                return HttpNotFound();
            }
            return View(inventoryItem);
        }

        // POST: InventoryItems/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditQuantity([Bind(Include = "PartID,Location,LastConfirmed,Quantity,ReorderPoint,ReorderQuantity")] InventoryItem inventoryItem)
        {
            var originalValues = (InventoryItem)Session["item"];
            inventoryItem.ID = originalValues.ID;
            inventoryItem.LastConfirmed = originalValues.LastConfirmed;
            inventoryItem.Location = originalValues.Location;
            inventoryItem.PartID = originalValues.PartID;
            

            if (ModelState.IsValid)
            {
                var change = originalValues.Quantity - inventoryItem.Quantity;
                if (change != 0)
                {
                    await RecordTransaction(inventoryItem, null, change, "Manual Change", "");
                }
                db.Entry(inventoryItem).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("ViewPurchasedInventory","InventoryControl");
            }
            return View(inventoryItem);
        }
        public ActionResult ViewTransactions(int? Id)
        {
            var partId = db.Inventory.Where(y => y.ID == Id).Select(y => y.PartID).First();
            return View(db.InventoryTansactions.Where(x => x.InventoryItem == partId).Select(x => x).ToList());
        }
        public ActionResult ViewPurchasedInventory()
        {
            return View(db.Inventory.Where(x => x.Location != null).Select(x => x).ToList());
        }

       
    }
}