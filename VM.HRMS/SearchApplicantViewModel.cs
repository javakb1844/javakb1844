using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.HRMS
{
  public  class SearchApplicantViewModel
    {
        public string Email { get; set; }
        public long? LookDesignationId { get; set; }
        public string Contact { get; set; }
        public DateTime? ApplyFromDate { get; set; }
        public DateTime? ApplyToDate { get; set; }
        public long? LookApplicantStatusId { get; set; }
    }
}
