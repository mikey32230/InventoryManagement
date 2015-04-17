using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InventoryManagement;
using System.Data.Entity;

namespace InventoryManagement.Data
{
    public class AssetService
    {
        //Wouldn't normally hard-code the data source
        private InventoryEntities db = new InventoryEntities(); 
        
        public IEnumerable<Asset> searchAssets(AssetQuery query)
        {
            var assets = db.Assets.Include(a => a.AssetModel).Include(a => a.AssetUser);

            if (!String.IsNullOrEmpty(query.serialNumber))
            {
                assets = assets.Where(a => a.SerialNumber == query.serialNumber);
            }
            if (query.AssetModelId != null)
            {
                assets = assets.Where(a => a.AssetModel.Id== query.AssetModelId);
            }
            if (!String.IsNullOrEmpty(query.owner))
            {
                
                //Use .split to separate first and last name 
                //Trim whitespace 
                var stringPartition = query.owner.Split(' ');
                String lName = stringPartition[0].Trim();
                assets = assets.Where(a=>a.AssetUser.LastName.Equals(lName));
                if (stringPartition.Length >= 2)
                {
                    String fNAme = stringPartition[1].Trim();
                    assets = assets.Where(a => a.AssetUser.FirstName.Equals(fNAme));
                }
            }
            if (!String.IsNullOrEmpty(query.roomNumber))
            {
                assets = assets.Where(a => a.RoomNum == query.roomNumber);
            }
            if (query.purchaseDate != null)
            {
                //Remove the Time portion of the DateTime 
                assets = assets.Where(a => DbFunctions.TruncateTime(a.PurchaseDate) == DbFunctions.TruncateTime(query.purchaseDate));
            }

            return assets; 
        }
    
    }
}