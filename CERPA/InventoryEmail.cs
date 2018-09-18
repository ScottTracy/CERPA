using CERPA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CERPA
{
    public class InventoryEmail:Email
    {
        private string emailToAddress = "Scotttesttracy+1@gmail.com";
        

        public void CreateInventoryEmail(string item )
        {
            string subject = item;

            string body = "Incorrect Part Quanity." + item;
            SendEmail(emailToAddress, subject, body);
            
        }
    }
}