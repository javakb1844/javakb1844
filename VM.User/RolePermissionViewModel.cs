using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.User
{
    public class RolePermissionViewModel
    {
        public long Role { get; set; }
        public List<PermissionViewModel> Permissions { get; set; }
        public List<long> SelectedPermissions { get; set; }
        public List<MenuViewModel> Menus { get; set; }
    }
}
