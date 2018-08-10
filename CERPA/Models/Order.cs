using CERPA.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CERPA.Models
{
    public class Order
    {

        public string PartID { get; set; }
        [Key]
        public int OrderID { get; set; }
        public string UserID { get; set; }
        public DateTime Timestamp { get; set; } 
        public DateTime DueDate { get; set; }
        public DateTime? ConfirmedOn { get; set; }
        public bool IsConfirmed { get; set; }
    }
}