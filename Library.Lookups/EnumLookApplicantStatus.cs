using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Lookups
{
    public enum EnumLookApplicantStatus
    {
        [StringValue("Just Applied")]
        JustApplied = 1,
        [StringValue("Interview Called")]
        InterviewCalled = 2,
        [StringValue("Shortlisted")]
        Shortlisted = 3,
        [StringValue("Deffered")]
        Deffered = 4,
        [StringValue("Selected")]
        Selected = 5,
        [StringValue("Offered")]
        Offered = 6,
        [StringValue("Hired")]
        Hired = 7,
        [StringValue("Rejected")]
        Rejected = 8,
        [StringValue("Joined")]
        Joined = 9,

    }
}
