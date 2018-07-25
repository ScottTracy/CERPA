using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CERPA.Models
{
    public class PartProcess
    {
        [Key]
        public string PartID { get; set; }
        public string WorkstationID { get; set; }
        public TimeSpan ProcessTime { get; set; }
        public string UserID { get; set; }


    }
}