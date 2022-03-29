using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.HRMS;
using Library.Core.Services;
using VM.HRMS;

namespace Services.HRMS
{
    public class AttendanceServices
    {
        HRMSWorker hrWorker = new HRMSWorker();
        public Result<EmployeeAttendance> GetAttendance(long id)
        {
            var result = new Result<EmployeeAttendance>();
            try
            {
                var attendance = hrWorker.Repository.Read<EmployeeAttendance>()
                    .Where(x => x.EmployeeAttendanceId.Equals(id)).FirstOrDefault();
                result.Data = attendance;
                result.ResultType = ResultType.Success;
            }
            catch(Exception e)
            {
                result.Data = null;
                result.ResultType = ResultType.Exception;
                result.Message = e.GetOriginalException().Message;
                result.Exception = e;
            }
            return result;
        }
        public Result<List<EmployeeAttendance>> GetEmployeeAttendance(long emp, string to,string from)
        {
            var result = new Result<List<EmployeeAttendance>>();
            try
            {
                string query = $@"select * from [EmployeeAttendance] where EmployeeId={emp}" ;
                if (from.IsNotNullOrEmpty())
                {
                    query += $" and ForDate > ' {from}'";
                }
                if (to.IsNotNullOrEmpty())
                {
                    query +=$" and ForDate < '{to}'";
                }
                var attendance = hrWorker.Repository.db.Database.SqlQuery<EmployeeAttendance>(query).ToListSafely();
                if (attendance.CountedPositive())
                {
                    result.Data = attendance;
                }
                else
                {
                    result.Data = null;
                }
                result.ResultType = ResultType.Success;
            }
            catch(Exception e)
            {
                result.Data = null;
                result.ResultType = ResultType.Exception;
                result.Message = e.GetOriginalException().Message;
                result.Exception = e;
            }
            return result;
        }

        public Result<List<AttendanceViewModel>> GetEmployeeAttendanceNew(int emp,  string from, string to)
        {
            var result = new Result<List<AttendanceViewModel>>();
            try
            {
                long roleid = Convert.ToInt64(System.Web.HttpContext.Current.Session["RoleId"]);
                int ProductSaleProfileId = Convert.ToInt32(System.Web.HttpContext.Current.Session["ProductSaleProfileId"]);
                long SelectedOrganizationId = Convert.ToInt64(System.Web.HttpContext.Current.Session["SelectedOrganizationId"]);

                string query = @"EXEC [dbo].[GetEmployeesAttandence]
                                 @DateFrom  = '" + Convert.ToDateTime(from).ToString("yyyy-MM-dd") + @"',
                                 @DateTo  ='" + Convert.ToDateTime(to).ToString("yyyy-MM-dd") + @"',
                                 @monthNo =0,
                                 @biometricid ="+ emp+@",
                                 @ProductSaleProfileId = " + ProductSaleProfileId + @",
	                             @OrganizationId = " + SelectedOrganizationId;




                var attendance = hrWorker.Repository.db.Database.SqlQuery<AttendanceViewModel>(query).ToListSafely();
                if (attendance.CountedPositive())
                {
                    result.Data = attendance;
                }
                else
                {
                    result.Data = null;
                }
                result.ResultType = ResultType.Success;
            }
            catch(Exception e)
            {
                result.Data = null;
                result.ResultType = ResultType.Exception;
                result.Message = e.GetOriginalException().Message;
                result.Exception = e;
            }
            return result;
        }
        public Result<List<OutputAttendanceViewModel>> GetEmployeeAttendanceById(InputAttendanceViewModel model)
        {
            var result = new Result<List<OutputAttendanceViewModel>>();
            try
            {
                long roleid = Convert.ToInt64(System.Web.HttpContext.Current.Session["RoleId"]);
                int ProductSaleProfileId = Convert.ToInt32(System.Web.HttpContext.Current.Session["ProductSaleProfileId"]);
                long SelectedOrganizationId = Convert.ToInt64(System.Web.HttpContext.Current.Session["SelectedOrganizationId"]);
                string query = @"EXEC [dbo].[GetAttandenceByDateAndBioId]
                                 @AttDate  = '" + model.LogDate.ToString("yyyy-MM-dd") + @"',
                                 @biometricid  =" + model.BioMetricId + @",
                                 @ProductSaleProfileId  =" + ProductSaleProfileId + @",
	                                 @OrganizationId =" + SelectedOrganizationId;



                var attendance = hrWorker.Repository.db.Database.SqlQuery<OutputAttendanceViewModel>(query).ToListSafely();
                if (attendance.CountedPositive())
                {
                    result.Data = attendance;
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
                result.Message = e.GetOriginalException().Message;
                result.Exception = e;
            }
            return result;
        }
        public Result<List<EmployeeAttendance>> GetEmployeeAttendance(long emp)
        {
            var result = new Result<List<EmployeeAttendance>>();
            try
            {
                  var attendance = hrWorker.Repository.Read<EmployeeAttendance>()
                      .Where(a=>a.EmployeeId.Equals(emp)).OrderBy(x=>x.TimeIn).ToListSafely();
                if (attendance.CountedPositive())
                {
                    result.Data = attendance;
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
                result.Message = e.GetOriginalException().Message;
                result.Exception = e;
            }
            return result;
        }
        public Result<bool> MarkAttendance(EmployeeAttendance attendance) {

            var result = new Result<bool>();
            try
            {
                hrWorker.Repository.Create(attendance);
                hrWorker.SaveChanges();
                result.ResultType = ResultType.Success;
            } 
            catch(Exception e)
            {
                result.Data = false;
                result.ResultType = ResultType.Exception;
                result.Message = e.GetOriginalException().Message;
                result.Exception = e;
            }
            return result;
        }

        public Result<bool> UpdateAttendance(EmployeeAttendance attendance)
        {

            var result = new Result<bool>();
            try
            {
                hrWorker.Repository.Update(attendance);
                hrWorker.SaveChanges();
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

    }
}
