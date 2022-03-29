using Data.HRMS;
using Library.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.HRMS;
using Library.Lookups;
using System.Web;

namespace Services.HRMS
{
    public class ApplicantServices
    {
        HRMSWorker hrmsWorker = new HRMSWorker();
        public Result<List<Applicant>> ApplicantList()
        {
            var result = new Result<List<Applicant>>();
            try
            {

                var dbApplicant = hrmsWorker.Repository.Read<Applicant>()
                   .ToListSafely();
                if (dbApplicant.IsNotNull())
                {
                    result.Data = dbApplicant;
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
        public Result<List<Applicant>> ApplicantList(SearchApplicantViewModel model)
        {
            var result = new Result<List<Applicant>>();
            try
            {
                string where = string.Empty;
                if (model.Email.IsNotNullOrEmpty())
                {
                    where += " and Email like '%" + model.Email + "%'";
                }

                if (model.LookDesignationId.IsNotNull())
                {
                    where += " and LookDesignationId=" + model.LookDesignationId;
                }
                if (model.Email.IsNotNullOrEmpty())
                {
                    where += " and Email like '%" + model.Email + "%'";
                }
                if (model.Contact.IsNotNullOrEmpty())
                {
                    where += " and (MobileNo like '%" + model.Email + "%' or ConatctInfo like '%" + model.Email + "%'";
                }
                if (model.ApplyFromDate.IsNotNullOrEmpty())
                {
                    where += " and CreateDate>=" + model.ApplyFromDate;
                }
                if (model.ApplyToDate.IsNotNullOrEmpty())
                {
                    where += " and CreateDate<=" + model.ApplyToDate;
                }
                if (model.LookApplicantStatusId.IsNotNull())
                {
                    where += "and LookApplicantStatusId=" + model.LookApplicantStatusId;
                }
                string sql = @"Select* from Applicant
                    where 1=1 " + where;
                var dbApplicant = hrmsWorker.Repository.db.Database.SqlQuery<Applicant>(sql).ToListSafely();
                if (dbApplicant.IsNotNull())
                {
                    result.Data = dbApplicant;
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
        public Result<bool> CreateApplicant(Applicant applicant)
        {
            var result = new Result<bool>();
            try
            {
                var hrmsWorker = new HRMSWorker();
                applicant.LookApplicantStatusId = (long)EnumLookApplicantStatus.JustApplied;
                hrmsWorker.Repository.Create(applicant);
                hrmsWorker.SaveChanges();
                result.Data = true;
                result.ResultType = ResultType.Success;
            }
            catch (Exception e)
            {
                result.Data = false;
                result.ResultType = ResultType.Exception;
                result.Message = e.GetOriginalException().Message;
                result.Exception = e;
            }
            return result;
        }

        public Result<bool> AddStatusChangeComments(ChangeApplicantsStatusViewModel model,long CreatedBy)
        {
          
            var result = new Result<bool>();
            try
            {
                var hrmsWorker = new HRMSWorker();
                ApplicantStatusChange applicantStatusChange = new ApplicantStatusChange();
                applicantStatusChange.ApplicantId = model.ApplicantId;
                applicantStatusChange.LookApplicantStatusId = model.LookApplicantStatusId;
                applicantStatusChange.Comments = model.Comments;
                applicantStatusChange.CreatedDate = DateTime.Now;
                applicantStatusChange.CreatedBy = CreatedBy; // (long)Session["EmployeeId"];

                hrmsWorker.Repository.Create(applicantStatusChange);
                hrmsWorker.SaveChanges();
                result.Data = true;
                result.ResultType = ResultType.Success;
            }
            catch (Exception e)
            {
                result.Data = false;
                result.ResultType = ResultType.Exception;
                result.Message = e.GetOriginalException().Message;
                result.Exception = e;
            }
            return result;
           
        }
        public Result<bool> UpdateApplicantStatus(EnumLookApplicantStatus status, long applicantId,long SelectionGrade=0)
        {
            var result = new Result<bool>();
            try
            {

                var applicant = hrmsWorker.Repository.Read<Applicant>()
                    .Where(a => a.ApplicantId.Equals(applicantId)).FirstOrDefault();
                applicant.LookApplicantStatusId = (long)status;
                if(SelectionGrade>0)
                    applicant.SelectionGrade = SelectionGrade;

                hrmsWorker.Repository.Update(applicant);
                hrmsWorker.SaveChanges();
                result.Data = true;
                result.ResultType = ResultType.Success;
            }
            catch (Exception e)
            {
                result.Data = false;
                result.ResultType = ResultType.Exception;
                result.Message = e.GetOriginalException().Message;
                result.Exception = e;

            }
            return result;
        }
        public Result<bool> UpdateApplicant(Applicant modelApplicant)
        {
            var result = new Result<bool>();
            try
            {
                var hrmsWorker = new HRMSWorker();
                /*  Applicant dbApplicant = hrmsWorker.Repository.Read<Applicant>()
                               .Where(b => b.ApplicantId == modelApplicant.ApplicantId).FirstOrDefault();
                  if (dbApplicant.IsNotNull())
                  {*/
                // dbDepartment.DepartmentName = modelDepartment.DepartmentName;
                hrmsWorker.Repository.Update(modelApplicant);
                hrmsWorker.SaveChanges();
                //    }

                result.Data = true;
                result.ResultType = ResultType.Success;
            }
            catch (Exception e)
            {
                result.Data = false;
                result.ResultType = ResultType.Exception;
                result.Message = e.GetOriginalException().Message;
                result.Exception = e;
            }
            return result;
        }

        public Result<Applicant> GetApplicantById(long? id)
        {
            var result = new Result<Applicant>();
            try
            {
                var hrmsWorker = new HRMSWorker();
                var dbDepartment = hrmsWorker.Repository.Read<Applicant>()
                    .Where(x => x.ApplicantId == id).FirstOrDefault();
                if (dbDepartment.IsNotNull())
                {
                    result.Data = dbDepartment;
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

        public Result<DropDownListClass> ApplicantMaping(Applicant ddList)
        {
            Result<DropDownListClass> result = new Result<DropDownListClass>();
            DropDownListClass model = new DropDownListClass();
            try
            {
                model.FullName = ddList.FullName;
                model.FirstName = ddList.FirstName;
                model.LastName = ddList.LastName;
                model.Email = ddList.Email;
                model.ApplicantId = ddList.ApplicantId;
                model.MaritalStatus = ddList.MaritalStatus;
                model.CNIC = ddList.CNIC;
                model.PresentAddress = ddList.PresentAddress;

                model.CoveringLetter = ddList.CoveringLetter;
                model.Resume = ddList.Resume;
                model.PermanentAddress = ddList.PermanentAddress;
                model.MobileNo = ddList.MobileNo;
                model.ConatctInfo = ddList.ConatctInfo;
                model.Salary = ddList.Salary;
                model.DateOfBirth = ddList.DateOfBirth;
                model.ShortSummary = ddList.ShortSummary;
                model.NoYearsOfExperience = ddList.NoYearsOfExperience;
                model.LookQualificationId = ddList.LookQualificationId;
                model.HRRemarks = ddList.HRRemarks;
                model.ApplicantRemarks = ddList.ApplicantRemarks;
                model.LookOrganizationId = ddList.LookOrganizationId;
                model.ReferenceName = ddList.ReferenceName;
                model.ReferenceEmail = ddList.ReferenceEmail;
                model.ExpectedDateOfJoining = ddList.ExpectedDateOfJoining;
                model.LookDepartmentId = ddList.LookDepartmentId;
                model.LookDesignationId = ddList.LookDesignationId;
                model.LookDesignationId = ddList.LookDesignationId;
                result.Data = model;
                result.ResultType = ResultType.Success;

                return result;
            }
            catch (Exception e)
            {
                result.Data = null;
                result.ResultType = ResultType.Exception;
                result.Message = e.GetOriginalException().Message;
                result.Exception = e;
                return result;
            }
        }

        public Result<Applicant> ApplicantMaping(DropDownListClass ddList)
        {
            Result<Applicant> result = new Result<Applicant>();
            Applicant model = new Applicant();
            try
            {
                model.FullName = ddList.FullName;
                model.FirstName = ddList.FirstName;
                model.LastName = ddList.LastName;
                model.Email = ddList.Email;
                model.ApplicantId = ddList.ApplicantId;
                model.MaritalStatus = ddList.MaritalStatus;
                model.CNIC = ddList.CNIC;
                model.PresentAddress = ddList.PresentAddress;
                model.PermanentAddress = ddList.PermanentAddress;
                model.MobileNo = ddList.MobileNo;
                model.ConatctInfo = ddList.ConatctInfo;
                model.Salary = ddList.StartSalary;
                model.DateOfBirth = ddList.DateOfBirth;
                model.ShortSummary = ddList.ShortSummary;
                model.NoYearsOfExperience = ddList.NoYearsOfExperience;
                model.LookQualificationId = ddList.LookQualificationTypeId;
                model.HRRemarks = ddList.HRRemarks;
                model.ApplicantRemarks = ddList.ApplicantRemarks;
                model.LookOrganizationId = ddList.LookOrganizationId;
                model.ReferenceName = ddList.ReferenceName;
                model.ReferenceEmail = ddList.ReferenceEmail;
                model.ExpectedDateOfJoining = ddList.ExpectedDateOfJoining;
                model.LookDepartmentId = ddList.LookDepartmentId;
                model.LookDesignationId = ddList.LookDesignationId;
                model.LookDesignationId = ddList.LookDesignationId;
                result.Data = model;
                result.ResultType = ResultType.Success;

                return result;
            }
            catch (Exception e)
            {
                result.Data = null;
                result.ResultType = ResultType.Exception;
                result.Message = e.GetOriginalException().Message;
                result.Exception = e;
                return result;
            }
        }
        public Result<long> ApplicantJoining(long id,string joiningDate)
        {
            var result = new Result<long>();
            try
            {
                var applicant = GetApplicantById(id);
                if (applicant.ResultType.Equals(ResultType.Exception)
                    || applicant.Data.Equals(null))
                {
                    if (applicant.ResultType.Equals(ResultType.Exception))
                    {
                        result.ResultType = applicant.ResultType;
                        result.Message = applicant.Message;
                        result.Exception = applicant.Exception;
                    }
                    else
                    {
                        result.ResultType = ResultType.Failure;
                        result.Message = "applicant not found";
                    }
                    return result;
                }
                var dbapplicant = applicant.Data;
                Employee employee = new Employee();
                employee.FirstName = dbapplicant.FirstName;
                employee.LastName = dbapplicant.LastName;
                employee.FullName = dbapplicant.FullName;
                employee.Gender = dbapplicant.Gender;
                employee.CNIC = dbapplicant.CNIC;
                employee.DateOfBirth = dbapplicant.DateOfBirth;
                employee.Email = dbapplicant.Email;
                employee.Gender = dbapplicant.Gender;
                employee.LookDepartmentId = dbapplicant.LookDepartmentId;
                employee.LookDesignationId = dbapplicant.LookDesignationId;
                employee.LookEmployeeTypeId = dbapplicant.LookEmployeeTypeId;
                employee.MaritalStatus = dbapplicant.MaritalStatus;
                employee.ConatctInfo = dbapplicant.ConatctInfo;
                employee.PresentAddress = dbapplicant.PresentAddress;
                employee.StartSalary = dbapplicant.Salary;
                employee.CreateDate = DateTime.Now;
                employee.ApplicantId = dbapplicant.ApplicantId;
                employee.EmployeeGuid = Guid.NewGuid();
                if (joiningDate.IsNotNullOrEmpty())
                {

                    employee.DateOfJoining = DateTime.Parse(joiningDate);
                }
                if (HttpContext.Current.Session["EmployeeId"].IsNotNull())
                {
                    employee.CreatedBy = (long)HttpContext.Current.Session["EmployeeId"];
                }
                EmployeeServices employeeService = new EmployeeServices();
                var employeeCreation = employeeService.CreateEmployee(employee);
                if (employeeCreation.ResultType.Equals(ResultType.Exception))
                {
                    result.ResultType = employeeCreation.ResultType;
                    result.Message = employeeCreation.Message;
                    result.Exception = employeeCreation.Exception;
                    return result;
                }
                result.Data = employee.EmployeeId;
                result.ResultType = ResultType.Success;
            }
            catch (Exception e)
            {
                result.Data = 0;
                result.ResultType = ResultType.Exception;
                result.Message = e.GetOriginalException().Message;
                result.Exception = e;

            }
            return result;
        }
    }
}
