using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CERPA.Models
{
    public class QuantityExpressionValue
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int? ChildQuantityExpressionId { get; set; }
        public int ChildQuantityValue { get; set; }
        
    }
}