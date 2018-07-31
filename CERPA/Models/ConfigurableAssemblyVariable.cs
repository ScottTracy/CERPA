using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CERPA.Models
{
    public class ConfigurableAssemblyVariable
    {
        [Key]
        public int ID { get; set; }
        public string PartID { get; set; }
        public string VariableName { get; set; }
        public bool ISRequired { get; set; }
    }
}