using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CERPA.Models
{
    public class GroupViewModel
    {
        
        
            public GroupViewModel()
            {
                this.UsersList = new List<SelectListItem>();
                this.RolesList = new List<SelectListItem>();
            }
            [Required(AllowEmptyStrings = false)]
            public string Id { get; set; }
            [Required(AllowEmptyStrings = false)]
            public string Name { get; set; }
            public string Description { get; set; }
            public ICollection<SelectListItem> UsersList { get; set; }
            public ICollection<SelectListItem> RolesList { get; set; }
        
    }
}