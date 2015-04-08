using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace InventoryManagement.Models
{
    public class VendorContactWrap
    {
        public Vendor vendor { get; set; }   //Should I be exposing these directly to the view!!??? 
        public Contact contact { get; set; } //Should I be exposing these directly to the view!!??? 


    }
}