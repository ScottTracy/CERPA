using CERPA.Models.PartProfiles;
using System;
using System.Collections.Generic;

namespace CERPA.Models
{
    public class Order
    {
        public string PartID { get; set; }
        public string OrderID { get; set; }
        public string UserID { get; set; }
        public DateTime Timestamp { get; set; } 
        public DateTime DueDate { get; set; }
    }
}