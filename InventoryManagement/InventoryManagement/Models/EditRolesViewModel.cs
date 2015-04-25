using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagement.Models
{
    public class EditRolesViewModel
    {
        public IEnumerable<SelectListItem> roles { get; set; }
        public String userId { get; set; }
        public String roleSelection { get; set; }
    }
}