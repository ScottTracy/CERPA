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
        public int VariableId { get; set; }
        public double Devisor { get; set; }
        public double Constant { get; set; }
    }
}