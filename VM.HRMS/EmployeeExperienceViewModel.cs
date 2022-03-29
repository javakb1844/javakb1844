using Data.HRMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.HRMS
{
   public class EmployeeExperienceViewModel
    {
        public long EmployeeId { get; set; }
        public List<EmployeeExperienceHistory> EmployeeExperienceHistory { get; set; }
    }
}
