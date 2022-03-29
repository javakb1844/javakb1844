using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.HRMS
{
  public  class ChangeApplicantsStatusViewModel
    {
        public long ApplicantId { get; set; }
        public long LookApplicantStatusId { get; set; }
        public long Points { get; set; }
        public string Comments { get; set; }
    }
}
