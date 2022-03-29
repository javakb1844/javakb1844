using Data.HRMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.HRMS
{
   public class EmployeeAppliedLeaveViewModel
    {

      //  public EmployeeAppliedLeave EmployeeAppliedLeave { get; set; }
        public string Email { get; set; }
        public long AvailedLeave { get; set; }
        public long EmployeePolicyId { get; set; }
        public long EmployeeId { get; set; }
        public string EmployeeNum { get; set; }
        public long LookPolicyId { get; set; }
        public string PolicyName { get; set; }
        public string PolicyUnit { get; set; }

   

        public DateTime LeaveFromDate { get; set; }
        public DateTime LeaveToDate { get; set; }
        public long CurrentshortLeaves { get; set; }
        public long CurrentHalfLeaves { get; set; }
        public long CurrentLeaves { get; set; }



        //  public long EmployeeAppliedLeaveId { get; set; }
        /*   public long EmployeeId { get; set; }
           public Nullable<System.DateTime> LeaveTimeStart { get; set; }
           public Nullable<System.DateTime> LaveTimeEnd { get; set; }
           public Nullable<System.DateTime> ActualTimeIn { get; set; }
           public Nullable<System.DateTime> ActualTimOut { get; set; }
           public long Status { get; set; }
           public Nullable<long> LookPolicyId { get; set; }
           public Nullable<System.DateTime> ForDate { get; set; }
           public string Reason { get; set; }
           public string HRRemarks { get; set; }
           public Nullable<long> UpdatedBy { get; set; }
           public Nullable<System.DateTime> UpdateDate { get; set; }
           public Nullable<long> CreatedBy { get; set; }
           public Nullable<System.DateTime> CreateDate { get; set; }*/
    }
}
