//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InventoryManagement
{
    using System;
    using System.Collections.Generic;
    
    public partial class AssetModel
    {
        public AssetModel()
        {
            this.Assets = new HashSet<Asset>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public Nullable<int> TypeId { get; set; }
    
        public virtual ICollection<Asset> Assets { get; set; }
        public virtual AssetType AssetType { get; set; }
    }
}
