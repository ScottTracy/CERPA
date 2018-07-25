using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CERPA.Models.PartProfiles
{
    public class PartProcess
    {
        public string PartID { get; set; }
        public string WorkstationID { get; set; }
        public DateTime Started { get; set; }
        public DateTime Confirmed { get; set; }
        public string UserID { get; set; }


    }
}