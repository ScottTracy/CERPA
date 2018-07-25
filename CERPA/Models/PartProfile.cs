using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CERPA.Models
{
    public class PartProfile
    {
        public string PartID { get; set; }
        public bool IsConfigurable { get; set; }
        public bool IsPurchased { get; set; }
        public List<string> Weight { get; set; }
        public List<string> Length { get; set; }
        public List<string> Height { get; set; }
        public List<string> Width { get; set; }
        
       
        
        
        
        public string VariableAName { get; set; }
        public string VariableBName { get; set; }
        public string VariableCName { get; set; }
        public string VariableDName { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string PropertyAName { get; set; }
        public string PropertyBName { get; set; }
        public string PropertyCName { get; set; }
        public string PropertyDName { get; set; }
        public List<string> PropertyAValue { get; set; }
        public List<string> PropertyBValue { get; set; }
        public List<string> PropertyCValue { get; set; }
        public List<string> PropertyDValue { get; set; }


    }
}