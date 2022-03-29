using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.HRMS
{
   public class EmployeeLeaveSummaryViewModel
    {
        public long EmployeePolicyId { get; set; }
        public System.DateTime StartLeaveDate { get; set; }
        public System.DateTime EndLeaveDate { get; set; }
        public Nullable<long> LeaveDays { get; set; }
        public Nullable<long> Hours { get; set; }
       // public long EmployeeId { get; set; }
        public Nullable<long> LookLeaveStatusId { get; set; }
        public long CurrentShortLeave { get; set; }
        public long AvailedShortLeave { get; set; }
        public long TotalShortLeave { get; set; }
        public long CurrentHalfLeave { get; set; }
        public long AvailedHalfLeave { get; set; }
        public long TotalHalfLeave { get; set; }
        public long CurrentFullLeave { get; set; }
        public long AvailedFullLeave { get; set; }
        public long TotalFullLeave { get; set; }
    }
}
