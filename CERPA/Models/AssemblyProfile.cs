using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CERPA.Models
{
    public class AssemblyProfile
    {
        [Key]
        public int Id { get; set; }
        public PartStructure Structures { get; set; }
        public List<PartProperty> Properties { get; set; }
        public InventoryItem Items { get; set; }
        public PartProcess Processes { get; set; }
        public List<ConfigurableAssemblyVariable> Variables{get;set;}
        
        

    }
} 