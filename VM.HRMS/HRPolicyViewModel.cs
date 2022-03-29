
using Data.HRMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.HRMS
{
   public class HRPolicyViewModel
    {

        public List<HRPolicy> HRPolicyList { get; set; }
        public List<LookDesignation> LookDesignationList { get; set; }
        public List<LookPolicy> LookPolicyList { get; set; }
        //public long HRPolicyId { get; set; }
        //public long LookDesignationId { get; set; }
        //public string DesignationName { get; set; }
        //public long LookPolicyId { get; set; }
        //public string PolicyName { get; set; }
        //public long PolicyValue { get; set; }
    }

    public class HRPolicyViewModelNew
    {
        public long HRPolicyId { get; set; }
        public long LookDesignationId { get; set; }
        public long LookPolicyId { get; set; }
        public long LookPolicyGroupId { get; set; }
        public string PolicyValue { get; set; }
        public string PolicyName { get; set; }
        public string  PolicyUnit { get; set; }
    }
}
