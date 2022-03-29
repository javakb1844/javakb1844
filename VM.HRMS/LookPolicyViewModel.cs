using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.HRMS
{
   public class LookPolicyViewModel
    {
        public long? LookPolicyId { get; set; }
        public string PolicyName { get; set; }
        public string PolicyUnit { get; set; }
        public long? LookPolicyGroupId { get; set; }
        public string PolicyValue { get; set; }
        public long EmployeePolicyId { get; set; }
        public string EmployeePolicyValue { get; set; }

    }
}
