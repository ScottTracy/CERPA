using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CERPA.Models
{
    public class InventoryItem
    {
        [Key]
        public string PartID { get; set; }
        public string Location { get; set; }
        public DateTime LastConfirmed { get; set; }
        public int Quantity { get; set; }
        public int ReorderPoint { get; set; }
        public int ReorderQuantity { get; set; }
    }
}