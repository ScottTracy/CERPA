﻿using CERPA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CERPA
{
    public class InventoryEmail:Email
    {
        private string emailToAddress = "";
        

        public void CreateInventoryEmail(Job job,string item, string quantity )
        {
            string subject = (job.ID).ToString();
           
            string body ="Incorrect Part Quanity."+item +": "+ quantity;
            SendEmail(emailToAddress, subject, body);
            
        }
    }
}