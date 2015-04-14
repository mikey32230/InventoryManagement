using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagement
{
    public partial class AssetModel
    {
        IEnumerable<string> _types;

        public AssetModel(IEnumerable<AssetType> types)
        {
            _types = types.Select(t => t.Type = t.Type);
        }
        public IEnumerable<SelectListItem> Types
        {
            get
            {
                return new SelectList(_types, "Types");
            }
        }

        public void SetTypes(IEnumerable<AssetType> types)
        {
            _types = types.Select(t => t.Type = t.Type);
        }
    }
}