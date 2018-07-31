using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CERPA.Models
{
    public class PartProperty
    {
        [Key]
        public int ID { get; set; }
        public string PartID { get; set; }
        public string PropertyName { get; set; }
        public bool IsPropertyConfigurable { get; set; }
        public string PropertyValue { get; set; }
        public List<string>PropertyExpression { get; set; }

    }
}