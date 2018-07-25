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
        public string OrderID { get; set; }
        public string Workstation { get; set; }

        public string PartID { get; set; }
        public DateTime Start { get; set; }
        public DateTime Confirmed { get; set; }
        public string UserID { get; set; }
    }
}