using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Lookups
{
    public enum  EnumLeavePolicy
    {
        [StringValue("Short Leave")]
        ShortLeave = 1,
        [StringValue("Half Leave")]
        HalfLeave = 2
        
    }
}
