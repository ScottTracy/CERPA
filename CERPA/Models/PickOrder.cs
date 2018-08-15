using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CERPA.Models
{
    public class PickOrder
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string PartId { get; set; }
        public string Location { get; set; }
        public string Destination { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? Confirmed { get; set; }
        public bool IsConfirmed { get; set; }
        public string UserId { get; set; }
        public int PartQuantity { get; set; }
    }
}