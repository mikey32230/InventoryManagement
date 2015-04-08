using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Models
{
    public class AssetQuery
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string assetType { get; set; }
        public string vendorName { get; set; }
        public string manufacturerName { get; set; }
        public string modifiedFirstName { get; set; }
        public string modifiedLastName { get; set; }
        public DateTime modifiedDate { get; set; }

    }

}