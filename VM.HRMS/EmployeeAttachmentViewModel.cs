using Data.HRMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.HRMS
{
    public class EmployeeAttachmentViewModel
    {
        public long EmployeeId { get; set; }
        public List<EmployeeAttachment> EmployeesAttachments { get; set; }
    }
}
