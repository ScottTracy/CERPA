using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CERPA.Models.PartProfiles
{
    public class ConfigurableAssemblyVariable
    {
        public string PartID { get; set; }
        public string VariableName { get; set; }
        public bool ISRequired { get; set; }
    }
}