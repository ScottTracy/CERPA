using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CERPA.Models
{
    public class PartStructure
    {
        [Key]
        public int ID { get; set; }
        public string PartID { get; set; }
        public string ChildID { get; set; }
        [DisplayName("Is the child quantity configurable?")]
        public bool ISChildQuantityConfigurable { get; set; }
        public int ChildQuantity { get; set; }
        public ChildQuantityExpression ChildQuantityExpression { get; set; }
    }
}