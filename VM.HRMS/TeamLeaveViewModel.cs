using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.HRMS
{
  public  class TeamLeaveViewModel
    {
        public long EmployeeLeaveSummaryId { get; set; }
        public long EmployeePolicyId { get; set; }
        public System.DateTime StartLeaveDate { get; set; }
        public System.DateTime EndLeaveDate { get; set; }
        public Nullable<long> ShortLeaves { get; set; }
        public Nullable<long> HalfLeaves { get; set; }
        public Nullable<long> Leaves { get; set; }
        public long EmployeeId { get; set; }
        public Nullable<long> LookLeaveStatusId { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public System.DateTime StatusUpdateDate { get; set; }

        public string Email { get; set; }
        public string PolicyName { get; set; }
        public string PolicyUnit { get; set; }


    }
}
