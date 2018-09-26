using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CERPA.Models;

namespace CERPA.Models
{
    public class UserJobsViewmodel
    {
        public ApplicationUser User { get; set; }
        public Job Job { get; set; }

    }
}