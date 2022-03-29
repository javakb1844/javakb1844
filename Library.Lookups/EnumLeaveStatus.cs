using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Lookups
{
    public enum EnumLeaveStatus
    {
        [StringValue("Draft")]
        Draft = 1,
        [StringValue("Submitted")]
        Submitted = 2,
        [StringValue("Approved")]
        Approved = 3,
        [StringValue("Rejected")]
        Rejected = 4,
        [StringValue("Confirmed")]
        Confirmed = 5,
        [StringValue("Cancelled")]
        Cancelled = 6,
        [StringValue("Availed")]
        Availed = 7
    }
}
