﻿using System;
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
            if(query.AssetTypeId != null)
            {
                assets = assets.Where(a => a.AssetModel.AssetType.Id == query.AssetTypeId);
            }
            if (!String.IsNullOrEmpty(query.owner))
            {
                
                //Use .split to separate first and last name 
                //Trim whitespace 
                var stringPartition = query.owner.Split(' ');
                String lName = stringPartition[0].Trim();
                if(!(String.IsNullOrEmpty(lName)))
                {
                    assets = assets.Where(a => a.AssetUser.LastName.Equals(lName));
                }
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

        //For Autcomplete of Owner's names 
        public IEnumerable<String> getOwners(String term)
        {
            var owners = db.AssetUsers.Where(u => u.Assets.Count >= 1);
            List<String> ownerNames = new List<String>(); 
            foreach (AssetUser user in owners)
            {     
                ownerNames.Add(user.getFullName());
            }
            var filteredNames = ownerNames.Where(o => o.StartsWith(term, true, null)); 
            if(filteredNames.Count()==0)
            {
                filteredNames = ownerNames.Where(o => o.EndsWith(term, true, null)); 
            }
            return filteredNames; 
        }
    
    }
  
}