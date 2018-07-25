using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CERPA.Models.PartProfiles
{
    public class PartProperty
    {
        public string PartID { get; set; }
        public string PropertyName { get; set; }
        public bool IsPropertyConfigurable { get; set; }
        public string PropertyValue { get; set; }
        public List<string>PropertyExpression { get; set; }

    }
}