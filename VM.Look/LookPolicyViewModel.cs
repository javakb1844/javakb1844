using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.Look
{
   public class LookPolicyViewModel
    {
        public long? LookPolicyId { get; set; }
        public string PolicyName { get; set; }
        public string PolicyUnit { get; set; }
        public int? LookPolicyGroupId { get; set; }
        public string PolicyValue { get; set; }
        public long? EmployeePolicyId { get; set; }
        public string EmployeePolicyValue { get; set; }
       public long EmployeeId { get; set; }     

    }

    public class LookGENPolicyViewModel
    {
      public long LookPolicyId { get; set; }
        public string PolicyName { get; set; }
        public string PolicyUnit { get; set; }
        public int LookPolicyGroupId { get; set; }
        public int? ValueType { get; set; }
        public bool? IsPercentage { get; set; }
        public string PolicyValue { get; set; }
        public TimeSpan? TimeIn { get; set; }
        public TimeSpan? TimeOut { get; set; }
        public long? HRPolicyByCaptionId { get; set; }
	 
    }
}
