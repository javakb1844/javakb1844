using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Lookups
{
  public  enum EnumPolicyType
    {
        [StringValue("Leave")]
        Leave = 1,
        [StringValue("Shift Timing")]
        ShiftTiming = 2,
        [StringValue("Week Working Days")]
        WeekWorkingDays = 3
    }
}
