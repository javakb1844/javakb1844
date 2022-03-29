using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.HRMS
{
   public class RecruitmentApprovalViewModel
    {
        public long RecruitmentApprovalId { get; set; }
        public long RecruitmentId { get; set; }
        public long EmployeeId { get; set; }
        public System.DateTime ApprovedDate { get; set; }
        public string Remarks { get; set; }
        public Nullable<long> ForwardToEmployeeId { get; set; }
        public string ApproveByName { get; set; }
        public string FarwardApprovalName { get; set; }
        public long LookRecruitmentStatusId { get; set; }
    }
}
