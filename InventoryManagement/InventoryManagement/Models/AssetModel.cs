using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagement
{
    public partial class AssetModel
    {
        IEnumerable<AssetType> _types;

        public AssetModel(IEnumerable<AssetType> types)
        {
            _types = types;
        }
        public IEnumerable<SelectListItem> AssetTypes
        {
            get
            {
                return new SelectList(_types,"Id","Types");
            }
        }
        [Display(Name = "Asset Type")]
        public int SelectedAssetTypeID { get; set; }

        public void SetTypes(IEnumerable<AssetType> types)
        {
        //    _types = types.Select(t => t.Type = t.Type);
        }
    }
}