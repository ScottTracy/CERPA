using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CERPA.Models
{
    public class Job
    {
        [Key]
        public int ID { get; set; }
        public string OrderID { get; set; }
        public string Workstation { get; set; }

        public string PartID { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? ConfirmedOn { get; set; }
        public string UserID { get; set; }
        public bool IsConfirmed { get; set; }
    }
}