using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CERPA.Models
{
    public class Job
    {
        public string Workstation { get; set; }
        public string PartID { get; set; }
        public DateTime Start { get; set; }
        public DateTime Confirmed { get; set; }
        public string UserID { get; set; }
    }
}