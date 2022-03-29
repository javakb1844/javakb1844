using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Lookups
{
    public enum EnumLookInterviewStatus
    {
        [StringValue("Called")]
        Called=1,
        [StringValue("Postponed")]
        Postponed = 2,
        [StringValue("Success")]
        Success = 3,
        [StringValue("Failed")]
        Failed = 4
    }
}
