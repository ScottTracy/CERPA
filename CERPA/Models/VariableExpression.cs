using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CERPA.Models
{
    public class VariableExpression
    {
        public int Id { get; set; }
        public int VariableId { get; set; }
        public int Devisor { get; set; }
        public double Constant { get; set; }
        public int PropertyId { get; set; }


    }
}