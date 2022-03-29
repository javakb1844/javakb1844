using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.User
{
    public class MenuViewModel
    {
        public long LeftMenuId { get; set; }
        public string MenuName { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string IconClass { get; set; }
        public bool IsActive { get; set; }
        public long ParentId { get; set; }
        public bool HaveChild { get; set; }
        public Nullable<long> DisplayOrder { get; set; }
    }
}
