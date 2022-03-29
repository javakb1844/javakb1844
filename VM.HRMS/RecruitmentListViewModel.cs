using Data.HRMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace VM.HRMS
{
   public class RecruitmentListViewModel
    {
       
        public long RecruitmentId { get; set; }
        public Nullable<long> LookDesignationId { get; set; }
        public Nullable<long> LookBranchId { get; set; }
        public Nullable<long> LookEmployeeTypeId { get; set; }
        public Nullable<System.DateTime> Preferred_Start_Date { get; set; }
        public Nullable<System.DateTime> AdClosingDate { get; set; }
        public Nullable<long> LookJobTypeId { get; set; }
        public Nullable<long> LookDepartmentId { get; set; }
        public Nullable<long> ReportingEmployeeId { get; set; }
        public Nullable<long> Length_Of_Term { get; set; }
        public long LookRecruitmentStatusId { get; set; }
        public Nullable<long> NoOfPositions { get; set; }
        public Nullable<long> LookGenderId { get; set; }
        public Nullable<long> ForApprovalEmployeeId { get; set; }
       

        
        public string PositionDescription { get; set; }
        public System.DateTime CreateDate { get; set; }
        public long CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public string DesignationName { get; set; }
        public string EmployeeTypeName { get; set; }
        public string JobTypeName { get; set; }
        public string Reporting { get; set; }
        public string LookGenderName { get; set; }
         public string ForApproval { get; set; }
        public string ShortSummary { get; set; }
        public string CreatedByName { get; set; }
    
        
        public IEnumerable<SelectListItem> ForApprovalList { get; set; }
        public List<RecruitmentApprovalViewModel> RecruitmentApprovalViewModel { get; set; }





}
}
