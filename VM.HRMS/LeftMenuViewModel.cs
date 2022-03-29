
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.HRMS
{
   public class LeftMenuViewModel
    {

        public long LeftMenuId { get; set; }
        public string MenuName { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string IconClass { get; set; }
        public bool IsActive { get; set; }
        public long ParentId { get; set; }
        public bool HaveChild { get; set; }
        public long DisplayOrder { get; set; }
        public Nullable<long> NewParentId { get; set; }

    }
}
