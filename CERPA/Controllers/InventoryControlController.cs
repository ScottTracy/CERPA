using System;
using System.Collections.Generic;
using System.Linq;
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
       
    }
}