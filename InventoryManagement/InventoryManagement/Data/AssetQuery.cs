using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Data
{
    public class AssetQuery
    {
        public String serialNumber {get;set;}
        public String modelName {get;set;}
        public String owner {get;set;}
        public String roomNumber {get;set;}
        public DateTime? purchaseDate {get;set;}
    
    }
}