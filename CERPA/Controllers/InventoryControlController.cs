using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CERPA.Models;

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

        public string GetUserName(InventoryTransaction transaction)
        {
            return db.Users.Where(u => u.Id == transaction.UserId).Select(n => n.UserName).First();
        }
        public async Task ReduceInventory(Job job)
        {
            var jobInventory = GetJobInventory(job);
            foreach (var item in jobInventory)
            {
                item.Key.Quantity = item.Key.Quantity - item.Value;
                db.Entry(item.Key).State = EntityState.Modified;
                CheckInventoryLevel(item.Key);
                RecordTransaction(item.Key, job, item.Value,("auto on job confirmation"));
                await db.SaveChangesAsync();
            }

        }
        public Dictionary<InventoryItem,int> GetJobInventory(Job job)
        {
            Dictionary<InventoryItem, int> jobInventory = new Dictionary<InventoryItem, int>();
            var items = db.PickOrders.Where(x => x.JobId == job.ID).Select(y => y).ToList();
            foreach(var item in items)
            {
                jobInventory.Add(db.Inventory.Where(z => z.PartID == item.PartId).Select(i => i).First(), item.PartQuantity);

            }
            return jobInventory;
        }
        public void RecordTransaction(InventoryItem item,Job job,int quantity, string type)
        {
            InventoryTransaction transaction = new InventoryTransaction
            {
                InventoryItem = item.PartID,
                UserId = job.UserID,
                JobId=job.ID,
                TimeStamp=DateTime.Now,
                TransactionType=type,
                Quantity= quantity,
                Comment=""
            };
        }
        public void CheckInventoryLevel(InventoryItem item)
        {
            if (item.Quantity <= item.ReorderPoint)
            {
                NotifyInventoryControl(item);
            }
            return;
        }
        public void NotifyInventoryControl(InventoryItem item)
        {
            string subject = "Part " + item.PartID + "is below reorder point";
            string body = subject + " of " + item.ReorderPoint + ". There are currently " + item.Quantity + " pieces left.";
            Email.SendEmail("scotttesttracy@gmail.com", subject, body);
        }

       
    }
}