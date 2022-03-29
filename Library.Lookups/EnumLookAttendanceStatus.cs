using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Extensions;

namespace Library.Lookups.Enums
{
    public enum EnumLookAttendanceStatus
    {
        [StringValue("Present")]
        Present = 1,

        [StringValue("Absent")]
        Absent  = 2,

        [StringValue("Leave")]
        Leave = 3

    }
}
