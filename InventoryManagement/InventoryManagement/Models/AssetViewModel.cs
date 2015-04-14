using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Models
{
    public class AssetViewModel
    {
        public Asset asset {get;set;}  

        public String getDate()
        {
            return asset.PurchaseDate.Value.ToShortDateString(); 
        }


    }
}