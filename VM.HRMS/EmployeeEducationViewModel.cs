using Data.HRMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.HRMS
{
  public  class EmployeeEducationViewModel
    {
     public long EmployeeId  { get; set; } 
     public List<EmployeeEducation> EmployeeEducation { get; set; }
    
    }
}
