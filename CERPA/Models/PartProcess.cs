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
        public TimeSpan ProcessTime { get; set; }
        public string UserID { get; set; }


    }
}