using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CERPA.Models.PartProfiles
{
    public class PartStructure
    {
        public string PartID { get; set; }
        public string ChildID { get; set; }
        public bool ISChildQuantityConfigurable { get; set; }
        public int ChildQuantity { get; set; }
        public List<string> ChildQuantityExpression { get; set; }
    }
}