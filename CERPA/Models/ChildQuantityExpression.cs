using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CERPA.Models
{
    public class ChildQuantityExpression
    {
        public int Id { get; set; }
        public string ChildID { get; set; }
        public string Variable { get; set; }
        public decimal Devisor { get; set; }
        public decimal Constant { get; set; }
    }
}