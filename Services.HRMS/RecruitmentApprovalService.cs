using Data.HRMS;
using Library.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.HRMS
{
   public class RecruitmentApprovalService
    {

        HRMSWorker hrmsWorker = new HRMSWorker();

        public Result<bool> Create(RecruitmentApproval recruitmentApproval)
        {
            var result = new Result<bool>();
            try
            {
                hrmsWorker.Repository.Create(recruitmentApproval);
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

        /*  public Result<bool> UpdateRecruitmentApproval(RecruitmentApproval model)
          {
              var result = new Result<bool>();
              try
              {
                  var hrmsWorker = new HRMSWorker();
                  Employee dbEmployee = hrmsWorker.Repository.Read<Employee>()
                               .Where(b => b.EmployeeId == modelEmployee.EmployeeId).FirstOrDefault();
                  if (dbEmployee.IsNotNull())
                  {
                      dbEmployee.FirstName = modelEmployee.FirstName;
                      dbEmployee.LastName = modelEmployee.LastName;
                      dbEmployee.FatherName = modelEmployee.FatherName;
                      dbEmployee.HusbandName = modelEmployee.HusbandName;
                      dbEmployee.FullName = modelEmployee.FullName;
                      dbEmployee.Email = modelEmployee.Email;
                      dbEmployee.MobileNo = modelEmployee.MobileNo;
                      dbEmployee.ConatctInfo = modelEmployee.ConatctInfo;
                      dbEmployee.CNIC = modelEmployee.CNIC;
                      dbEmployee.MaritalStatus = modelEmployee.MaritalStatus;
                      dbEmployee.PresentAddress = modelEmployee.PresentAddress;
                      dbEmployee.PermanentAddress = modelEmployee.PermanentAddress;
                      dbEmployee.Gender = modelEmployee.Gender;
                      dbEmployee.DateOfBirth = modelEmployee.DateOfBirth;
                      dbEmployee.ShortSummary = modelEmployee.ShortSummary;
                      dbEmployee.ICEContactInfo = modelEmployee.ICEContactInfo;
                      dbEmployee.IsDisable = modelEmployee.IsDisable;
                      dbEmployee.DisableDetail = modelEmployee.DisableDetail;
                      dbEmployee.HRRemarks = modelEmployee.HRRemarks;
                      dbEmployee.NoYearsOfExperience = modelEmployee.NoYearsOfExperience;
                      dbEmployee.StartSalary = modelEmployee.StartSalary;
                      dbEmployee.ReferenceName = modelEmployee.ReferenceName;
                      dbEmployee.ReferenceEmail = modelEmployee.ReferenceEmail;
                      dbEmployee.DateOfJoining = modelEmployee.DateOfJoining;
                      dbEmployee.ManagerId = modelEmployee.ManagerId;
                      dbEmployee.LookEmployeeTypeId = modelEmployee.LookEmployeeTypeId;
                      dbEmployee.LookDepartmentId = modelEmployee.LookDepartmentId;
                      dbEmployee.LookDesignationId = modelEmployee.LookDesignationId;
                      dbEmployee.ExitDate = modelEmployee.ExitDate;
                      hrmsWorker.Repository.Update(dbEmployee);
                      hrmsWorker.SaveChanges();
                  }

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

      */
    }
}
