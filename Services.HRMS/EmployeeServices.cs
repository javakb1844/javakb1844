using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.HRMS;
using Library.Core;
using Library.Core.Services;
using VM.HRMS;
using VM.Look;

namespace Services.HRMS
{
    public class EmployeeServices
    {
        HRMSWorker hrmsWorker = new HRMSWorker();
        public Result<List<EmployeeViewModel>> GetEmployeeList()
        {
            HRMSWorker hrmsWorker = new HRMSWorker();
            var result = new Result<List<EmployeeViewModel>>();
            try
            {
                long SelectedOrganizationId = Convert.ToInt64(System.Web.HttpContext.Current.Session["SelectedOrganizationId"]);
                string query = @"EXEC [dbo].[GetEmployees] @LookOrganizationId="+ SelectedOrganizationId;
                var Agencylist = hrmsWorker.Repository.db.Database.SqlQuery<EmployeeViewModel>(query).ToListSafely();
                //// var dbEmployee = hrmsWorker.Repository.Read<EmployeeViewModel>()
                //  .ToListSafely();
                if (Agencylist.CountedPositive())
                {
                    result.Data = Agencylist;
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
        public Result<List<EmployeeViewModel>> GetEmployeeList(string LookOrganizationIds)
        {
            HRMSWorker hrmsWorker = new HRMSWorker();
            var result = new Result<List<EmployeeViewModel>>();
            try
            {
                long SelectedOrganizationId = Convert.ToInt64(System.Web.HttpContext.Current.Session["SelectedOrganizationId"]);
                string query = @"EXEC [dbo].[GetEmployees]  @LookOrganizationId=" + SelectedOrganizationId+", @LookOrganizationIds ='"+ LookOrganizationIds+"'";
                var Agencylist = hrmsWorker.Repository.db.Database.SqlQuery<EmployeeViewModel>(query).ToListSafely();
                //// var dbEmployee = hrmsWorker.Repository.Read<EmployeeViewModel>()
                //  .ToListSafely();
                if (Agencylist.CountedPositive())
                {
                    result.Data = Agencylist;
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
        public Result<List<EmployeeViewModel>> GetEmployeeList(long employeeid)
        {
            HRMSWorker hrmsWorker = new HRMSWorker();
            var result = new Result<List<EmployeeViewModel>>();
            try
            {
                long SelectedOrganizationId = Convert.ToInt64(System.Web.HttpContext.Current.Session["SelectedOrganizationId"]);
                string query = @"EXEC [dbo].[GetEmployeesById]  @LookOrganizationId=" + SelectedOrganizationId+", @EmployeeId ="+ employeeid;
                var Agencylist = hrmsWorker.Repository.db.Database.SqlQuery<EmployeeViewModel>(query).ToListSafely();
                //// var dbEmployee = hrmsWorker.Repository.Read<EmployeeViewModel>()
                //  .ToListSafely();
                if (Agencylist.CountedPositive())
                {
                    result.Data = Agencylist;
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
        public Result<List<Employee>> EmployeeList()
        {
            var result = new Result<List<Employee>>();
            try
            {
                List<Employee> dbEmployee;
                   long roleid = Convert.ToInt64(System.Web.HttpContext.Current.Session["RoleId"]);
                int ProductSaleProfileId = Convert.ToInt32(System.Web.HttpContext.Current.Session["ProductSaleProfileId"]);

                long SelectedOrganizationId = Convert.ToInt64(System.Web.HttpContext.Current.Session["SelectedOrganizationId"]);
                if(roleid==0)
                    dbEmployee = hrmsWorker.Repository.Read<Employee>().Where(x => x.LookOrganizationId == SelectedOrganizationId  && x.IsUserOnly == false ).ToListSafely();
                else
                dbEmployee = hrmsWorker.Repository.Read<Employee>().Where(x=>x.LookOrganizationId== SelectedOrganizationId && x.ProductSaleProfileId== ProductSaleProfileId && x.IsUserOnly==false  ).ToListSafely();
                if (dbEmployee.IsNotNull())
                {
                    result.Data = dbEmployee;
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
        public Result<List<Employee>> EmployeeListByManagerId(long id)
        {
            var result = new Result<List<Employee>>();
            try
            {
                long SelectedOrganizationId = Convert.ToInt64(System.Web.HttpContext.Current.Session["SelectedOrganizationId"]);
                var dbEmployee = hrmsWorker.Repository.Read<Employee>()
                    .Where(x => x.ManagerId == id && x.LookOrganizationId== SelectedOrganizationId)
                   .ToListSafely();
                if (dbEmployee.IsNotNull())
                {
                    result.Data = dbEmployee;
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

        public Result<List<Employee>> EmployeeListId(long id)
        {
            var result = new Result<List<Employee>>();
            try
            {
                long SelectedOrganizationId = Convert.ToInt64(System.Web.HttpContext.Current.Session["SelectedOrganizationId"]);
                HRMSWorker hrmsWorker = new HRMSWorker();
                string sql = @"WITH RecQry AS
                        (
                            SELECT *
                              FROM [dbo].[Employee] 
	                            where  LookOrganizationId="+ SelectedOrganizationId+" and EmployeeId=" + id + @"
                            UNION ALL
                            SELECT a.*
                              FROM  [dbo].[Employee]  a INNER JOIN RecQry b
                                ON a.[ManagerId] = b.EmployeeId
                                where  LookOrganizationId=" + SelectedOrganizationId + @"
                        )
                        SELECT *
                          FROM RecQry";
                var dbEmployee = hrmsWorker.Repository.db.Database.SqlQuery<Employee>(sql).ToListSafely();

                if (dbEmployee.IsNotNull())
                {
                    result.Data = dbEmployee;
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
        public Employee getEmployee()
        {
            var sdb = SingletonDB.Instance;
            var s = sdb.GetDBConnection();


            var emp = hrmsWorker.Repository.Read<Employee>().FirstOrDefault();
            return emp;

        }

        public Result<bool> CreateEmployee(Employee employee)
        {
            var result = new Result<bool>();
            try
            {
                long roleid = Convert.ToInt64(System.Web.HttpContext.Current.Session["RoleId"]);
                int ProductSaleProfileId = Convert.ToInt32(System.Web.HttpContext.Current.Session["ProductSaleProfileId"]);
                long SelectedOrganizationId = Convert.ToInt64(System.Web.HttpContext.Current.Session["SelectedOrganizationId"]);
                var hrmsWorker = new HRMSWorker();
                employee.CreateDate = SharedUtilities.GetPakistanDateTime();
                employee.LookOrganizationId = SelectedOrganizationId;
                employee.ProductSaleProfileId = ProductSaleProfileId;
                hrmsWorker.Repository.Create(employee);
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

        public Result<Employee> GetEmployeeByGuid(Guid? id)
        {
            var result = new Result<Employee>();
            try
            {
                Employee dbEmployee;
                long SelectedOrganizationId = Convert.ToInt64(System.Web.HttpContext.Current.Session["SelectedOrganizationId"]);
                long roleid = Convert.ToInt64(System.Web.HttpContext.Current.Session["RoleId"]);
                int ProductSaleProfileId = Convert.ToInt32(System.Web.HttpContext.Current.Session["ProductSaleProfileId"]);
                var hrmsWorker = new HRMSWorker();
                if (roleid == 0)
                {
                    dbEmployee = hrmsWorker.Repository.Read<Employee>()
                       .Where(x => x.EmployeeGuid == id && x.LookOrganizationId == SelectedOrganizationId).FirstOrDefault();
                }else
                {
                    dbEmployee = hrmsWorker.Repository.Read<Employee>()
                                          .Where(x => x.EmployeeGuid == id && x.LookOrganizationId == SelectedOrganizationId && x.ProductSaleProfileId==ProductSaleProfileId).FirstOrDefault();
                }
                if (dbEmployee.IsNotNull())
                {
                    result.Data = dbEmployee;
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

        public Result<Employee> GetEmployeeById(long id)
        {
            var result = new Result<Employee>();
            try
            {
                Employee dbEmployee;
                long roleid = Convert.ToInt64(System.Web.HttpContext.Current.Session["RoleId"]);
                int ProductSaleProfileId = Convert.ToInt32(System.Web.HttpContext.Current.Session["ProductSaleProfileId"]);
                long SelectedOrganizationId = Convert.ToInt64(System.Web.HttpContext.Current.Session["SelectedOrganizationId"]);
                var hrmsWorker = new HRMSWorker();
                if (roleid == 0)
                {
                    dbEmployee = hrmsWorker.Repository.Read<Employee>()
                       .Where(x => x.EmployeeId == id && x.LookOrganizationId == SelectedOrganizationId).FirstOrDefault();
                }else
                {
                    dbEmployee = hrmsWorker.Repository.Read<Employee>()
                       .Where(x => x.EmployeeId == id && x.LookOrganizationId == SelectedOrganizationId && x.ProductSaleProfileId== ProductSaleProfileId).FirstOrDefault();
                }
                if (dbEmployee.IsNotNull())
                {
                    result.Data = dbEmployee;
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

        public Result<List<EmployeeAttachment>> GetAttachmentById(long? id)
        {
            var result = new Result<List<EmployeeAttachment>>();
            try
            {
                List<EmployeeAttachment> dbAttachment;
                long roleid = Convert.ToInt64(System.Web.HttpContext.Current.Session["RoleId"]);
                int ProductSaleProfileId = Convert.ToInt32(System.Web.HttpContext.Current.Session["ProductSaleProfileId"]);
                long SelectedOrganizationId = Convert.ToInt64(System.Web.HttpContext.Current.Session["SelectedOrganizationId"]);
                var hrmsWorker = new HRMSWorker();
                if (roleid == 0)
                {
                    dbAttachment = hrmsWorker.Repository.Read<EmployeeAttachment>()
                   .Where(x => x.EmployeeId == id).ToListSafely();
                }else
                {
                    dbAttachment = hrmsWorker.Repository.Read<EmployeeAttachment>()
                                     .Where(x => x.EmployeeId == id ).ToListSafely();
                }
                
                if (dbAttachment.CountedPositive())
                {
                    result.Data = dbAttachment;
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

        public Result<List<EmployeeEducation>> GetEducationById(long? id)
        {
            var result = new Result<List<EmployeeEducation>>();
            try
            {
                long SelectedOrganizationId = Convert.ToInt64(System.Web.HttpContext.Current.Session["SelectedOrganizationId"]);
                var hrmsWorker = new HRMSWorker();
                var dbEducation = hrmsWorker.Repository.Read<EmployeeEducation>()
                    .Where(x => x.EmployeeId == id).ToListSafely();
                if (dbEducation.CountedPositive())
                {
                    result.Data = dbEducation;
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

        public Result<List<EmployeeExperienceHistory>> GetExperienceById(long? id)
        {
            var result = new Result<List<EmployeeExperienceHistory>>();
            try
            {
                long SelectedOrganizationId = Convert.ToInt64(System.Web.HttpContext.Current.Session["SelectedOrganizationId"]);
                var hrmsWorker = new HRMSWorker();
                var dbExperience = hrmsWorker.Repository.Read<EmployeeExperienceHistory>()
                    .Where(x => x.EmployeeId == id).ToListSafely();
                if (dbExperience.CountedPositive())
                {
                    result.Data = dbExperience;
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

        public Result<List<EmployeesDependent>> GetDependentById(long? id)
        {
            var result = new Result<List<EmployeesDependent>>();
            try
            {
                long SelectedOrganizationId = Convert.ToInt64(System.Web.HttpContext.Current.Session["SelectedOrganizationId"]);
                var hrmsWorker = new HRMSWorker();
                var dbDependent = hrmsWorker.Repository.Read<EmployeesDependent>()
                    .Where(x => x.EmployeeId == id).ToListSafely();
                if (dbDependent.CountedPositive())
                {
                    result.Data = dbDependent;
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
        public Result<bool> UpdateEmployee(DropDownListClass modelEmployee)
        {
            var result = new Result<bool>();
            try
            {
                long SelectedOrganizationId = Convert.ToInt64(System.Web.HttpContext.Current.Session["SelectedOrganizationId"]);
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
                    dbEmployee.RoleId = modelEmployee.RoleId;
                    dbEmployee.LookEmployeeTypeId = modelEmployee.LookEmployeeTypeId;
                    dbEmployee.WorkingHourPolicyId = modelEmployee.WorkingHourPolicyId;
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

        public Result<bool> UpdateEmployeeUserId(Employee modelEmployee)
        {
            var result = new Result<bool>();
            try
            {
                Employee dbEmployee;
                long roleid = Convert.ToInt64(System.Web.HttpContext.Current.Session["RoleId"]);
                int ProductSaleProfileId = Convert.ToInt32(System.Web.HttpContext.Current.Session["ProductSaleProfileId"]);
                long SelectedOrganizationId = Convert.ToInt64(System.Web.HttpContext.Current.Session["SelectedOrganizationId"]);
                var hrmsWorker = new HRMSWorker();
                if (roleid == 0)
                {
                    dbEmployee = hrmsWorker.Repository.Read<Employee>()
                           .Where(b => b.EmployeeId == modelEmployee.EmployeeId && b.LookOrganizationId == SelectedOrganizationId ).FirstOrDefault();
                }
                else
                {
                    dbEmployee = hrmsWorker.Repository.Read<Employee>()
                             .Where(b => b.EmployeeId == modelEmployee.EmployeeId && b.LookOrganizationId == SelectedOrganizationId && b.ProductSaleProfileId == ProductSaleProfileId).FirstOrDefault();
                }
                if (dbEmployee.IsNotNull())
                {

                    dbEmployee.EmployeeId = modelEmployee.EmployeeId;
                 //   dbEmployee.UpdateDate = modelEmployee.UpdateDate;
                    dbEmployee.UpdatedBy = modelEmployee.UpdatedBy;
                    dbEmployee.UpdateDate = SharedUtilities.GetPakistanDateTime();
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

        public Result<bool> CreateEducation(EmployeeEducationViewModel modelQualification)
        {
            var result = new Result<bool>();
            try
            {
                long SelectedOrganizationId = Convert.ToInt64(System.Web.HttpContext.Current.Session["SelectedOrganizationId"]);
                if (modelQualification.IsNotNull())
                {
                    string query = "delete from EmployeeEducation where EmployeeId=" + modelQualification.EmployeeId;
                    var deleteQualification = hrmsWorker.Repository.db.Database.ExecuteSqlCommand(query);
                    bool isDbChanged = false;
                    foreach (var education in modelQualification.EmployeeEducation)
                    {
                        ////modelEmployee.EmployeeEducation.EmployeeId = modelEmployee.EmployeeId ?? 0;
                        //EmployeeEducation qualification = new EmployeeEducation();
                        //qualification.EmployeeId = education.EmployeeEducationlist.EmployeeId;
                        //qualification.EducationYear = education.EmployeeEducation.EducationYear;
                        //qualification.Grade = education.EmployeeEducation.Grade;
                        //qualification.LookQualificationId = modelEmployee.EmployeeEducation.LookQualificationId;
                        //qualification.LookInstitutionId = modelEmployee.EmployeeEducation.LookInstitutionId;
                        isDbChanged = true;
                        hrmsWorker.Repository.Create(education);
                    }
                    if (isDbChanged)
                    {
                        hrmsWorker.SaveChanges();
                    }
                    result.Data = true;
                    result.ResultType = ResultType.Success;
                    result.Message = "Saved";
                }
                else
                {
                    result.Data = false;
                    result.ResultType = ResultType.Failure;
                    result.Message = "There Was no Information to save. Please provide at least one record before saving";
                }

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

        public Result<bool> CreateExperience(EmployeeExperienceViewModel modelExperience)
        {
            var result = new Result<bool>();
            try
            {
                long SelectedOrganizationId = Convert.ToInt64(System.Web.HttpContext.Current.Session["SelectedOrganizationId"]);
                var hrmsWorker = new HRMSWorker();
                if (modelExperience.IsNotNull())
                {
                    string query = "delete from EmployeeExperienceHistory where EmployeeId=" + modelExperience.EmployeeId;
                    var deleteQualification = hrmsWorker.Repository.db.Database.ExecuteSqlCommand(query);
                    bool isDbChanged = false;
                    foreach (var experience in modelExperience.EmployeeExperienceHistory)
                    {
                        isDbChanged = true;
                        hrmsWorker.Repository.Create(experience);
                    }
                    if (isDbChanged)
                    {
                        hrmsWorker.SaveChanges();
                    }
                    result.Data = true;
                    result.ResultType = ResultType.Success;
                    result.Message = "Saved";
                }
                else
                {
                    result.Data = false;
                    result.ResultType = ResultType.Failure;
                    result.Message = "There Was no Information to save. Please provide at least one record before saving";
                }

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

        public Result<bool> CreateDependent(EmployeeDependentViewModel modelDependent)
        {
            var result = new Result<bool>();
            try
            {
                long SelectedOrganizationId = Convert.ToInt64(System.Web.HttpContext.Current.Session["SelectedOrganizationId"]);
                var hrmsWorker = new HRMSWorker();
                if (modelDependent.IsNotNull())
                {
                    string query = "delete from EmployeesDependents where EmployeeId=" + modelDependent.EmployeeId;
                    var deleteQualification = hrmsWorker.Repository.db.Database.ExecuteSqlCommand(query);
                    bool isDbChanged = false;
                    foreach (var experience in modelDependent.EmployeesDependent)
                    {
                        isDbChanged = true;
                        hrmsWorker.Repository.Create(experience);
                    }
                    if (isDbChanged)
                    {
                        hrmsWorker.SaveChanges();
                    }
                    result.Data = true;
                    result.ResultType = ResultType.Success;
                    result.Message = "Saved";
                }
                else
                {
                    result.Data = false;
                    result.ResultType = ResultType.Failure;
                    result.Message = "There Was no Information to save. Please provide at least one record before saving";
                }

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

        public Result<bool> UpdateEmployeePolicy(LookPolicyViewModel modelPolicy)
        {
            var result = new Result<bool>();
            EmployeePolicy employeePolicy = new EmployeePolicy();
            try
            {
                long SelectedOrganizationId = Convert.ToInt64(System.Web.HttpContext.Current.Session["SelectedOrganizationId"]);
                var hrmsWorker = new HRMSWorker();
                if (modelPolicy.IsNotNull())
                {
                    if (modelPolicy.EmployeePolicyId > 0)
                    {
                        EmployeePolicy model = hrmsWorker.Repository.Read<EmployeePolicy>()
                                 .Where(b => b.EmployeePolicyId == modelPolicy.EmployeePolicyId).FirstOrDefault();
                        model.EmployeeId = modelPolicy.EmployeeId;
                        model.LookPolicyGroupId = modelPolicy.LookPolicyGroupId;
                        model.PolicyName = modelPolicy.PolicyName;
                        model.PolicyUnit = modelPolicy.PolicyUnit;
                        model.PolicyValue = modelPolicy.EmployeePolicyValue;
                        model.LookPolicyId = modelPolicy.LookPolicyId;
                        model.LookPolicyGroupId = modelPolicy.LookPolicyGroupId;
                        hrmsWorker.Repository.Update(model);
                        hrmsWorker.SaveChanges();
                        result.Data = true;
                        result.ResultType = ResultType.Success;
                        result.Message = "Saved";
                    }
                    else
                    {
                        employeePolicy.EmployeeId = modelPolicy.EmployeeId;
                        employeePolicy.LookPolicyGroupId = modelPolicy.LookPolicyGroupId;
                        employeePolicy.PolicyName = modelPolicy.PolicyName;
                        employeePolicy.PolicyUnit = modelPolicy.PolicyUnit;
                        employeePolicy.PolicyValue = modelPolicy.EmployeePolicyValue;
                        employeePolicy.LookPolicyId = modelPolicy.LookPolicyId;
                        employeePolicy.LookPolicyGroupId = modelPolicy.LookPolicyGroupId;
                        hrmsWorker.Repository.Create(employeePolicy);
                        hrmsWorker.SaveChanges();
                        result.Data = true;
                        result.ResultType = ResultType.Success;
                        result.Message = "Saved";
                    }
                }
                else
                {
                    result.Data = false;
                    result.ResultType = ResultType.Failure;
                    result.Message = "There Was no Information to save. Please provide at least one record before saving";
                }
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
        public Result<Employee> EmployeeMaping(DropDownListClass ddList)
        {
            long SelectedOrganizationId = Convert.ToInt64(System.Web.HttpContext.Current.Session["SelectedOrganizationId"]);
            Employee model = new Employee();
            var result = new Result<Employee>();
            model.FirstName = ddList.FirstName;
            model.LastName = ddList.LastName;
            model.FatherName = ddList.FatherName;
            model.HusbandName = ddList.HusbandName;
            model.FullName = ddList.FullName;
            model.Email = ddList.Email;
            model.EmployeeGuid = Guid.NewGuid();
            model.MobileNo = ddList.MobileNo;
            model.ConatctInfo = ddList.ConatctInfo;
            model.CNIC = ddList.CNIC;
            model.MaritalStatus = ddList.MaritalStatus;
            model.PresentAddress = ddList.PresentAddress;
            model.PermanentAddress = ddList.PermanentAddress;
            model.Gender = ddList.Gender;
            model.DateOfBirth = ddList.DateOfBirth;
            model.CreateDate = DateTime.Now;
          
            //model.ShortSummary = ddList.ShortSummary;
            //model.NoYearsOfExperience = ddList.NoYearsOfExperience;
            //model.LookQualificationId = ddList.LookQualificationTypeId;
            //model.HRRemarks = ddList.HRRemarks;
            //model.ApplicantRemarks = ddList.ApplicantRemarks;
            //model.LookOrganizationId = ddList.LookOrganizationId;
            //model.ReferenceName = ddList.ReferenceName;
            //model.ReferenceEmail = ddList.ReferenceEmail;
            //model.ExpectedDateOfJoining = ddList.ExpectedDateOfJoining;
            //model.LookEmployeeTypeId = ddList.LookEmployeeTypeId;
            //model.LookDepartmentId = ddList.LookDepartmentId;
            //model.LookDesignationId = ddList.LookDesignationId;
            //model.LookDesignationId = ddList.LookDesignationId;
            result.Data = model;
            return (result);
        }
        public Result<List<Employee>> GetEmployeesbyDeignationDepartment(long designation, long department)
        {
            var result = new Result<List<Employee>>();
            try
            {
                long SelectedOrganizationId = Convert.ToInt64(System.Web.HttpContext.Current.Session["SelectedOrganizationId"]);
                string query = $@"select * from [Employee]
                where [LookDepartmentId]={department} and [LookDesignationId]={designation} and LookOrganizationId={SelectedOrganizationId}";
                var dbEmployee = hrmsWorker.Repository.db.Database.SqlQuery<Employee>(query)
                   .ToListSafely();
                if (dbEmployee.IsNotNull())
                {
                    result.Data = dbEmployee;
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
        public string GetEmployeeName(long id)
        {
            long SelectedOrganizationId = Convert.ToInt64(System.Web.HttpContext.Current.Session["SelectedOrganizationId"]);
            string query = "select [FullName] from [dbo].[Employee] where LookOrganizationId="+ SelectedOrganizationId + " and EmployeeId=" + id;
            var name = hrmsWorker.Repository.db.Database.SqlQuery<string>(query).FirstOrDefault();
            return name;
        }
        public long GetEmployeeDesignation(long id)
        {
            long SelectedOrganizationId = Convert.ToInt64(System.Web.HttpContext.Current.Session["SelectedOrganizationId"]);
            string query = "select [LookDesignationId] from [dbo].[Employee] where  LookOrganizationId=" + SelectedOrganizationId + " and EmployeeId=" + id;
            var designation = hrmsWorker.Repository.db.Database.SqlQuery<long>(query).FirstOrDefault();
            return designation;
        }

    }
}
