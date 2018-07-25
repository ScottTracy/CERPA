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
        
       
        
        
        
   
      
      

    }
}