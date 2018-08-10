using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CERPA.Models
{
    public class VariableValue
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ConfigurableAssemblyVariableId { get; set; }
        public double Value { get; set; }
    }
}