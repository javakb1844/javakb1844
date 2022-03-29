using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Lookups
{
   public enum EnumLookRecruitmentStatus
    { 
        [StringValue("Requested")]
        Request = 1,
        [StringValue("Approved")]
        Approved = 2,
        [StringValue("Revised")]
        Revised = 3,
        [StringValue("Published")]
        Published = 4,
        [StringValue("Forward")]
        Forward = 5
    }
}
