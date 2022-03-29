using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.HRMS
{
   public class ApproveRecruitmentStatusViewModel
    {
        public long RecruitmentId { get; set; }
        public long ForwardToEmployeeId { get; set; }
        public long LookRecruitmentStatusId { get; set; }
        public string Comments { get; set; }

       
    }
}
