using Data.HRMS;
using Library.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using VM.HRMS;
using VM.Look;

namespace Services.HRMS
{
   public class DropDownListClass
    {
        HRMSWorker hrmsworker;


        public DropDownListClass()
        {
            hrmsworker = new HRMSWorker();
        }

        public long ApplicantId { get; set; }

        public long? EmployeeId { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string HusbandName { get; set; }
      
        public Guid? EmployeeGuid { get; set; }
        public string Email { get; set; }
        public double? Salary { get; set; }
        public string Gender { get; set; }
        public Nullable<long> MaritalStatus { get; set; }
        public string CNIC { get; set; }
        public string PresentAddress { get; set; }
        public string PermanentAddress { get; set; }
        public string MobileNo { get; set; }
        public string ConatctInfo { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public string DOB { get; set; }
        public string ShortSummary { get; set; }
        public Nullable<long> NoYearsOfExperience { get; set; }
        public Nullable<long> LookQualificationTypeId { get; set; }
        public string HRRemarks { get; set; }
        public string ApplicantRemarks { get; set; }
        public Nullable<long> LookOrganizationId { get; set; }
        public string ReferenceName { get; set; }
        public string ReferenceEmail { get; set; }
        public Nullable<System.DateTime> ExpectedDateOfJoining { get; set; }
        public Nullable<long> LookEmployeeTypeId { get; set; }
        public Nullable<long> RoleId { get; set; }
       
        public Nullable<int>  WorkingHourPolicyId { get; set; }
        public Nullable<long> LookQualificationId { get; set; }
        public HttpPostedFile CoveringLetterFile { get; set; }
        public HttpPostedFile ResumeFile { get; set; }
        public string CoveringLetter { get; set; }
        public string Resume { get; set; }
        public Nullable<long> LookDepartmentId { get; set; }
        public Nullable<long> LookDesignationId { get; set; }
        public Nullable<long> LookInstitutionId { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public Nullable<long> UpdatedBy { get; set; }

        public string DesignationName { get; set; }
        public Nullable<double> StartSalary { get; set; }

        public Nullable<long> ManagerId { get; set; }

        public Nullable<System.DateTime> DateOfJoining { get; set; }

        public string ICEContactInfo { get; set; }        

        public Nullable<System.DateTime> ExitDate { get; set; }
        public bool IsDisable { get; set; }
        public string DisableDetail { get; set; }

        public List<LookPolicyViewModel> LookPolicyList { get; set; }
        public List<EmployeeEducation> EmployeeEducationList { get; set; }

        public List<EmployeeExperienceHistory> EmployeeExperienceHistoryList { get; set; }

        public List<EmployeesDependent> EmployeeDependentList { get; set; }

        public List<EmployeeAttachment> EmployeeAttachmentList { get; set; }

        public EmployeeEducation EmployeeEducation { get; set; }

        public EmployeeExperienceHistory EmployeeExperienceHistory { get; set; }

        public EmployeesDependent EmployeesDependent { get; set; }

        public EmployeeAttachment EmployeeAttachments { get; set; }


        public SelectList getDepartment(int ProductSaleProfileId)
        {
            IEnumerable<SelectListItem> departmentList = (from m in hrmsworker.Repository.Read<LookDepartment>().Where(x => x.ProductSaleProfileId == ProductSaleProfileId) select m).AsEnumerable().Select(m => new SelectListItem() { Text = m.DepartmentName, Value = m.LookDepartmentId.ToString() });
            return new SelectList(departmentList, "Value", "Text", LookDepartmentId);
        }

        public SelectList getQualificationType()
        {
            IEnumerable<SelectListItem> qualificationList = (from m in hrmsworker.Repository.Read<LookQualificationType>() select m).AsEnumerable().Select(m => new SelectListItem() { Text = m.QualificationType, Value = m.LookQualificationTypeId.ToString() });
            return new SelectList(qualificationList, "Value", "Text", LookQualificationTypeId);
        }
        public SelectList getQualification()
        {
            IEnumerable<SelectListItem> qualificationList = (from m in hrmsworker.Repository.Read<LookQualification>() select m).AsEnumerable().Select(m => new SelectListItem() { Text = m.QualificationName, Value = m.LookQualificationId.ToString() });
            return new SelectList(qualificationList, "Value", "Text", LookQualificationId);
        }

        public SelectList WorkingHourPolicy(int ProductSaleProfileId)
        {
            IEnumerable<SelectListItem> employeeList = (from m in hrmsworker.Repository.Read<HRPolicyCaption>().Where(x=>x.ProductSaleProfileId== ProductSaleProfileId) select m).AsEnumerable().Select(m => new SelectListItem() { Text = m.PolicyCaption, Value = m.HRPolicyCaptionId.ToString() });
            return new SelectList(employeeList, "Value", "Text", WorkingHourPolicyId);
        }
        public SelectList getEmployeeType(int ProductSaleProfileId)
        {
            IEnumerable<SelectListItem> employeeList = (from m in hrmsworker.Repository.Read<LookEmployeeType>() select m).AsEnumerable().Select(m => new SelectListItem() { Text = m.EmployeeTypeName, Value = m.LookEmployeeTypeId.ToString() });
            return new SelectList(employeeList, "Value", "Text", LookEmployeeTypeId);
        }
        public SelectList geteRole(int ProductSaleProfileId)
        {
            IEnumerable<SelectListItem> employeeList = (from m in hrmsworker.Repository.Read<Role>().Where(x => x.ProductSaleProfileId == ProductSaleProfileId) select m).Where(x=>x.RoleId>1 && x.IsActive==true).AsEnumerable().Select(m => new SelectListItem() { Text = m.Name, Value = m.RoleId.ToString() });
            return new SelectList(employeeList, "Value", "Text", RoleId);
        }

        
        public SelectList getOrganization(int ProductSaleProfileId)
        {
            IEnumerable<SelectListItem> organizationList = (from m in hrmsworker.Repository.Read<LookOrganization>().Where(x => x.ProductSaleProfileId == ProductSaleProfileId) select m).AsEnumerable().Select(m => new SelectListItem() { Text = m.OrganizationName, Value = m.LookOrganizationId.ToString() });
            return new SelectList(organizationList, "Value", "Text", LookOrganizationId);
        }

        public SelectList getDesignation(int ProductSaleProfileId)
        {
            IEnumerable<SelectListItem> designatioList = (from m in hrmsworker.Repository.Read<LookDesignation>().Where(x => x.ProductSaleProfileId == ProductSaleProfileId) select m).AsEnumerable().Select(m => new SelectListItem() { Text = m.DesignationName, Value = m.LookDesignationId.ToString() });
            return new SelectList(designatioList, "Value", "Text", LookDesignationId);
        }

        public SelectList getInstitution(int ProductSaleProfileId)
        {
            IEnumerable<SelectListItem> designatioList = (from m in hrmsworker.Repository.Read<LookInstitution>() select m).AsEnumerable().Select(m => new SelectListItem() { Text = m.InstitutionName, Value = m.LookInstitutionId.ToString() });
            return new SelectList(designatioList, "Value", "Text",LookInstitutionId);
        }

        public Result<LookDesignation> GetDesignationById(int ProductSaleProfileId,long? id)
        {
            var result = new Result<LookDesignation>();
            try
            {
               
                var dbRecruitment = hrmsworker.Repository.Read<LookDesignation>()
                    .Where(x =>x.ProductSaleProfileId== ProductSaleProfileId && x.LookDesignationId == id).FirstOrDefault();
                if (dbRecruitment.IsNotNull())
                {
                    result.Data = dbRecruitment;
                }
                else
                {
                    result.Data = null;
                }
                result.ResultType = ResultType.Success;
            }
            catch (Exception e)
            {
                result.Data = null;
                result.ResultType = ResultType.Exception;
                result.Exception = e;
                result.Message = e.GetOriginalException().Message;
            }
            return result;

        }


    }
}
