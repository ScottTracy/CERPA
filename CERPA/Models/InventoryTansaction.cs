using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CERPA.Models
{
    public class InventoryTansaction
    {
        public int Id { get; set; }
        public string InventoryItem { get; set; }
        public string UserId { get; set; }
        public int? JobId { get; set; }
        public DateTime TimeStamp { get; set; }
        public string TransactionType { get; set; }
        public int Quantity { get; set; }
        public string Comment { get; set; }

    }
}