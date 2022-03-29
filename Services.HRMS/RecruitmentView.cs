using Data.HRMS;
using Services.Look;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VM.HRMS;

namespace Services.HRMS
{
  public  class RecruitmentView
    {
        HRMSWorker hrmsworker;


        public RecruitmentView()
        {
            hrmsworker = new HRMSWorker();
        }
        
            
        public long RecruitmentId { get; set; }

        public long? LookDesignationId { get; set; }
        public long? LookBranchId { get; set; }
        public long? LookEmployeeTypeId { get; set; }
        public long? LookDepartmentId { get; set; }
        public long? LookJobTypeId { get; set; }
        public long? ReportingEmployeeId { get; set; }
        public long? Length_Of_Term { get; set; }
        public long? NoOfPositions { get; set; }
        public long? LookGenderId { get; set; }
        public List<long> LookAppointmentProcessList { get; set; }
        public List<long> LookSelectionProcessList { get; set; }
        public string PositionDescription { get; set; }

        public string ShortSummary { get; set; }
        public HttpPostedFile PositionDescriptionFile { get; set; }

       public long? ForApprovalEmployeeId { get; set; }

        public long LookRecruitmentStatusId { get; set; }
        public Nullable<System.DateTime> Preferred_Start_Date { get; set; }
        public Nullable<System.DateTime> AdClosingDate { get; set; }
    
        public List<LookSelectionProcessViewModel> SelectionProcessList { get; set; } 


        public List<LookSelectionProcessViewModel> LookAppointmentProcess { get; set; }
        public List<LookSelectionProcessViewModel> LookSelectionProcess { get; set; }
        public SelectList getDepartment()
        {
            IEnumerable<SelectListItem> departmentList = (from m in hrmsworker.Repository.Read<LookDepartment>() select m).AsEnumerable().Select(m => new SelectListItem() { Text = m.DepartmentName, Value = m.LookDepartmentId.ToString() });
            return new SelectList(departmentList, "Value", "Text", LookDepartmentId);
        }

          

        public SelectList getEmployeeType()
        {
            IEnumerable<SelectListItem> employeeList = (from m in hrmsworker.Repository.Read<LookEmployeeType>() select m).AsEnumerable().Select(m => new SelectListItem() { Text = m.EmployeeTypeName, Value = m.LookEmployeeTypeId.ToString() });
            return new SelectList(employeeList, "Value", "Text", LookEmployeeTypeId);
        }
        
        public SelectList getDesignation()
        {
            IEnumerable<SelectListItem> designatioList = (from m in hrmsworker.Repository.Read<LookDesignation>() select m).AsEnumerable().Select(m => new SelectListItem() { Text = m.DesignationName, Value = m.LookDesignationId.ToString() });
            return new SelectList(designatioList, "Value", "Text", LookDesignationId);
        }
        public SelectList getJobTypes()
        {
            IEnumerable<SelectListItem> JobTypesList = (from m in hrmsworker.Repository.Read<LookJobType>() select m).AsEnumerable().Select(m => new SelectListItem() { Text = m.JobTypeName, Value = m.LookJobTypeId.ToString() });
            return new SelectList(JobTypesList, "Value", "Text", LookJobTypeId);
        }
        public SelectList getReportingEmploye()
        {
            IEnumerable<SelectListItem> JobTypesList = (from m in hrmsworker.Repository.Read<Employee>() select m).AsEnumerable().Select(m => new SelectListItem() { Text = m.FullName+", "+ LookDesignationService.GetDesignationName(m.LookDesignationId).Data, Value = m.EmployeeId.ToString() });
            return new SelectList(JobTypesList, "Value", "Text", ReportingEmployeeId);
        }
        public SelectList getGenders()
        {
            IEnumerable<SelectListItem> JobTypesList = (from m in hrmsworker.Repository.Read<LookGender>() select m).AsEnumerable().Select(m => new SelectListItem() { Text = m.LookGenderName, Value = m.LookGenderId.ToString() });
            return new SelectList(JobTypesList, "Value", "Text", LookGenderId);
        }

       /* public SelectList getLookAppointmentProcess()
        {
            IEnumerable<SelectListItem> JobTypesList = (from m in hrmsworker.Repository.Read<LookSelectionProcess>() select m).AsEnumerable().Where(x=>x.Type==1 ).Select(m => new SelectListItem() { Text = m.SelectionProcess, Value = m.LookSelectionProcessId.ToString() });
            return new SelectList(JobTypesList, "Value", "Text", LookAppointmentProcessList);
        }
        public SelectList getLookSelectionProcess()
        {
            IEnumerable<SelectListItem> JobTypesList = (from m in hrmsworker.Repository.Read<LookSelectionProcess>() select m).AsEnumerable().Where(x => x.Type == 0).Select(m => new SelectListItem() { Text = m.SelectionProcess, Value = m.LookSelectionProcessId.ToString() });
            return new SelectList(JobTypesList, "Value", "Text", LookSelectionProcessList);
        }*/




    }
}
