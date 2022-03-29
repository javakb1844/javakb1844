using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.HRMS
{
  public  class EmployeeViewModel
    {
        public long EmployeeId { get; set; }
       public string FullName    { get; set; }
       public Guid EmployeeGuid  { get; set; }
       public string EmployeeNum  { get; set; }     
       public string Email  { get; set; }    
       public string Gender  { get; set; }     
       public string CNIC  { get; set; }
       public bool? IsDisable  { get; set; }   
       public string MobileNo  { get; set; } 
       public DateTime? DateOfJoining  { get; set; }
       public long? ManagerId  { get; set; }     
       public int? BioMetricId  { get; set; }
        public string ManagerName { get; set; }
        public string DepartmentName { get; set; }
        
        public int? LookDesignationId { get; set; }

    }
}
