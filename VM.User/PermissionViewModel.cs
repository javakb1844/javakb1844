using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.User
{
    public class PermissionViewModel
    {
        public long PermissionId { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string Attribute { get; set; }
        public string Name { get; set; }
        public Nullable<long> MenuId { get; set; }
        public Nullable<long> LookDepartmentId { get; set; }
        public bool Selected { get; set; } 
        public long? RolePermissionId { get; set; }
        public long Menu { get; set; }
        public bool IsChild { get; set; }
        public long ChildMenu { get; set; }
    }
}
