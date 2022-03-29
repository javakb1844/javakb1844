using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Lookups
{
   public enum EnumLookGender
    {
        [StringValue("Male")]
        [Description("M")]
        Male = 1,

        [StringValue("Female")]
        [Description("F")]
        Female = 2,

        [StringValue("Not Preferred")]
        [Description("NP")]
        NotPreferred = 3
    }
}
