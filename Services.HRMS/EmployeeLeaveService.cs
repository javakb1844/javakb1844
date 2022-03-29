using Data.HRMS;
using Library.Core.Services;
using Library.Lookups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VM.HRMS;
using VM.Look;

namespace Services.HRMS
{
    public class EmployeeLeaveService
    {
        HRMSWorker hrmsWorker = new HRMSWorker();

        public Result<long> CreateEmployeeLeaveSummary(EmployeeLeaveSummary employeeLeave)
        {
            var result = new Result<long>();
            try
            {
                // int oo = DateTime.Now.Year;
                
                hrmsWorker.Repository.Create(employeeLeave);
                hrmsWorker.SaveChanges();
                result.Data = employeeLeave.EmployeeLeaveSummaryId;
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


        public Result<bool> CreateEmployeeLeave(EmployeeLeave employeeLeave)
        {
            var result = new Result<bool>();
            try
            {
                // int oo = DateTime.Now.Year;
                employeeLeave.LookLeaveStatusId= (long)EnumLeaveStatus.Draft;
                employeeLeave.Year = (long)DateTime.Now.Year;
                hrmsWorker.Repository.Create(employeeLeave);
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

        public Result<bool> CreateEmployeeLeave(EmployeeAppliedLeaveViewModel model,long summaryId)
        {

            var result = new Result<bool>();
            try
            {
                EmployeeLeave modelEmployeeLeave = new EmployeeLeave();
                modelEmployeeLeave.EmployeePolicyId = model.EmployeePolicyId;
                modelEmployeeLeave.EmployeeId = model.EmployeeId;
                modelEmployeeLeave.LookPolicyId = model.LookPolicyId;
                modelEmployeeLeave.PolicyName = model.PolicyName;
                modelEmployeeLeave.PolicyUnit = model.PolicyUnit;
                modelEmployeeLeave.LeaveDate = model.LeaveFromDate;
                modelEmployeeLeave.EmployeeLeaveSummaryId = summaryId;
                modelEmployeeLeave.LeaveValue = "1";
                modelEmployeeLeave.Year = DateTime.Now.Year;
                modelEmployeeLeave.LookLeaveStatusId = (long)EnumLeaveStatus.Draft;
                hrmsWorker.Repository.Create(modelEmployeeLeave);
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

        public Result<bool> CreateEmployeeLeave(long summaryId,long EmployeeId,List<LookPolicyViewModel> employeeLeavePolicy,DateTime leaveDate, long leaveType)
        {
            var result = new Result<bool>();
            try
            {
                EmployeeLeave modelEmployeeLeave = new EmployeeLeave();
            modelEmployeeLeave.EmployeePolicyId = Convert.ToInt64(employeeLeavePolicy.Where(x => x.LookPolicyId == leaveType).FirstOrDefault().EmployeePolicyId); //employeeLeave.EmployeePolicyId;
            modelEmployeeLeave.EmployeeId = EmployeeId;
            modelEmployeeLeave.PolicyName = employeeLeavePolicy.Where(x => x.LookPolicyId == leaveType).FirstOrDefault().PolicyName;
            modelEmployeeLeave.PolicyUnit = employeeLeavePolicy.Where(x => x.LookPolicyId == leaveType).FirstOrDefault().PolicyUnit;
            modelEmployeeLeave.LeaveValue = "1";
            modelEmployeeLeave.LeaveDate = leaveDate;
            modelEmployeeLeave.LookPolicyId = leaveType;
                modelEmployeeLeave.EmployeeLeaveSummaryId = summaryId;
            bool tt =CreateEmployeeLeave(modelEmployeeLeave).Data;
                result.Data = tt;
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

        public Result<EmployeeLeave> GetEmployeeLeaveById(long? id)
        {
            var result = new Result<EmployeeLeave>();
            try
            {

                var dbInstitution = hrmsWorker.Repository.Read<EmployeeLeave>()
                    .Where(x => x.EmployeeLeaveId == id).FirstOrDefault();
                if (dbInstitution.IsNotNull())
                {
                    result.Data = dbInstitution;
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

        public Result<bool> UpdateEmployeeLeave(EmployeeLeave modelEmployeeLeave)
        {
            var result = new Result<bool>();
            try
            {
                EmployeeLeave dbEmployeeLeave = hrmsWorker.Repository.Read<EmployeeLeave>()
                             .Where(b => b.EmployeeLeaveId == modelEmployeeLeave.EmployeeLeaveId).FirstOrDefault();
                if (dbEmployeeLeave.IsNotNull())
                {
                    //  dbEmployeeLeave.InstitutionName = modelEmployeeLeave.;
                    hrmsWorker.Repository.Update(dbEmployeeLeave);
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
        public Result<List<EmployeeLeave>> EmployeeLeaveDetailListById(long? id,long summaryId=0)
        {
            var result = new Result<List<EmployeeLeave>>();
            try
            {
                List<EmployeeLeave> dbEmployeeLeave;
                if (summaryId > 0)
                {
                     dbEmployeeLeave = hrmsWorker.Repository.Read<EmployeeLeave>()
                        .Where(x => x.EmployeeId == id && x.EmployeeLeaveSummaryId == summaryId)
                       .ToListSafely();
                }
                else
                {
                    dbEmployeeLeave = hrmsWorker.Repository.Read<EmployeeLeave>()
                       .Where(x => x.EmployeeId == id )
                      .ToListSafely();
                }
                if (dbEmployeeLeave.IsNotNull())
                {
                    result.Data = dbEmployeeLeave;
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

        public Result<List<TeamLeaveViewModel>> TeamLeaveList(long managerId)
        {
            string tempWahere = "";
            if (managerId>0)
            {
                tempWahere = "where Employee.ManagerId=" + managerId;
            }
            string sql = @"select Employee.Email,EmployeePolicy.PolicyName,EmployeePolicy.PolicyUnit ,EmployeeLeaveSummary.*
                        from EmployeeLeaveSummary
                         inner join Employee on EmployeeLeaveSummary.EmployeeId = Employee.EmployeeId
                         inner
                        join EmployeePolicy on EmployeeLeaveSummary.EmployeePolicyId = EmployeePolicy.EmployeePolicyId
                        "+ tempWahere +@" order by StatusUpdateDate";


            var result = new Result<List<TeamLeaveViewModel>>();
            try
            {
                var dbEmployeeLeave = hrmsWorker.Repository.db.Database.SqlQuery<TeamLeaveViewModel>(sql).ToListSafely();
                if (dbEmployeeLeave.IsNotNull())
                {
                    result.Data = dbEmployeeLeave;
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
        public Result<List<EmployeeLeaveSummary>> EmployeeLeaveListByEmpId(long[] ids)
        {
            var result = new Result<List<EmployeeLeaveSummary>>();
            try
            {
                var dbEmployeeLeave = hrmsWorker.Repository.Read<EmployeeLeaveSummary>()
                    .Where(x => ids.Contains(x.EmployeeId))
                   .ToListSafely();
                if (dbEmployeeLeave.IsNotNull())
                {
                    result.Data = dbEmployeeLeave;
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
        public Result<List<EmployeeLeaveSummary>> EmployeeLeaveListByEmpId(long id)
        {
            var result = new Result<List<EmployeeLeaveSummary>>();
            try
            {
                var dbEmployeeLeave = hrmsWorker.Repository.Read<EmployeeLeaveSummary>()
                    .Where(x => x.EmployeeId == id)
                   .ToListSafely();
                if (dbEmployeeLeave.IsNotNull())
                {
                    result.Data = dbEmployeeLeave;
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

        public Result<EmployeeLeaveSummary> EmployeeLeaveListById(long id)
        {
            var result = new Result<EmployeeLeaveSummary>();
            try
            {
                var dbEmployeeLeave = hrmsWorker.Repository.Read<EmployeeLeaveSummary>()
                    .Where(x => x.EmployeeLeaveSummaryId == id)
                   .FirstOrDefault();
                if (dbEmployeeLeave.IsNotNull())
                {
                    result.Data = dbEmployeeLeave;
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
        public Result<List<EmployeeLeave>> EmployeeLeaveDetailList()
        {
            var result = new Result<List<EmployeeLeave>>();
            try
            {
                var dbEmployeeLeave = hrmsWorker.Repository.Read<EmployeeLeave>()
                   .ToListSafely();
                if (dbEmployeeLeave.IsNotNull())
                {
                    result.Data = dbEmployeeLeave;
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
        public Result<List<EmployeeLeaveSummary>> EmployeeLeaveList()
        {
            var result = new Result<List<EmployeeLeaveSummary>>();
            try
            {
                var dbEmployeeLeave = hrmsWorker.Repository.Read<EmployeeLeaveSummary>()
                   .ToListSafely();
                if (dbEmployeeLeave.IsNotNull())
                {
                    result.Data = dbEmployeeLeave;
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

        public int DaysLeft(DateTime startDate, DateTime endDate, Boolean excludeWeekends, List<DateTime> excludeDates, List<string> offDays)
        {
            int count = 0;
            for (DateTime index = startDate; index <= endDate; index = index.AddDays(1))
            {
                //if (excludeWeekends && index.DayOfWeek != DayOfWeek.Sunday && index.DayOfWeek != DayOfWeek.Saturday)
                if (excludeWeekends && !offDays.Contains(index.DayOfWeek.ToString()))
                {
                    bool excluded = false; ;
                    for (int i = 0; i < excludeDates.Count; i++)
                    {
                        if (index.Date.CompareTo(excludeDates[i].Date) == 0)
                        {
                            excluded = true;
                            break;
                        }
                    }

                    if (!excluded)
                    {
                        count++;
                    }
                }
            }

            return count;
        }
        public int DaysLeft(DateTime startDate, DateTime endDate, Boolean excludeWeekends, List<DateTime> excludeDates, List<string> offDays,long summaryId, EmployeeAppliedLeaveViewModel model)
        {
            int count = 0;
            EmployeeLeave employeeLeave;
            for (DateTime index = startDate; index <= endDate; index = index.AddDays(1))
            {
                //if (excludeWeekends && index.DayOfWeek != DayOfWeek.Sunday && index.DayOfWeek != DayOfWeek.Saturday)
                if (excludeWeekends && !offDays.Contains(index.DayOfWeek.ToString()))
                {
                    bool excluded = false; ;
                    for (int i = 0; i < excludeDates.Count; i++)
                    {
                        if (index.Date.CompareTo(excludeDates[i].Date) == 0)
                        {
                            excluded = true;
                            break;
                        }
                    }

                    if (!excluded)
                    {
                        employeeLeave = new EmployeeLeave();
                        employeeLeave.EmployeePolicyId = model.EmployeePolicyId;
                        employeeLeave.EmployeeId = model.EmployeeId;
                        employeeLeave.PolicyName = model.PolicyName;
                        employeeLeave.PolicyUnit = model.PolicyUnit;
                        employeeLeave.LookPolicyId = model.LookPolicyId;
                        employeeLeave.LeaveValue = "1";
                        employeeLeave.EmployeeLeaveSummaryId = summaryId;
                        employeeLeave.LeaveDate = index.Date;

                        bool tt = CreateEmployeeLeave(employeeLeave).Data;
                        count++;
                    }
                }
            }

            return count;
        }

        public Result<DateTime> AdjustDateWithShiftTiming(string date, string time)
        {
            var result = new Result<DateTime>();
            try
            {
                string newDateTimeString = date + " " + time; //"‎ ‎15‎:‎40‎:‎30";
                newDateTimeString = Regex.Replace(newDateTimeString, @"[^\u0000-\u007F]", string.Empty);
                DateTime newDateTimeStringFormated = Convert.ToDateTime(newDateTimeString);
                result.Data = newDateTimeStringFormated;
                result.ResultType = ResultType.Success;
            }
            catch (Exception e)
            {
                // result.Data = null;
                result.ResultType = ResultType.Exception;
                result.Message = e.GetOriginalException().Message;
                result.Exception = e;
            }
            return result;
        }
        public Result<DateTime> AdjustLeaveDate(DateTime CheckInDateTimeFormated, DateTime CheckOutDateTimeFormated, DateTime LeaveDate, List<DateTime> holidaysArray, List<string> offDays, bool isToDate)
        {
            var result = new Result<DateTime>();

            try
            {
                TimeSpan ttt;
                TimeSpan oneDaytemnp = new TimeSpan(1, 0, 0, 0);
                if (LeaveDate > CheckInDateTimeFormated && LeaveDate < CheckOutDateTimeFormated)
                 {

                        if (isToDate == true)
                        ttt = LeaveDate- CheckInDateTimeFormated  ;
                    else
                        ttt = CheckOutDateTimeFormated - LeaveDate;
                    var hour = ttt.Hours;
                    var min = ttt.Minutes;
                    var isHoliday = holidaysArray.Contains(CheckInDateTimeFormated.Date);
                    var isOffday = offDays.Contains(CheckInDateTimeFormated.DayOfWeek.ToString());
                    if (hour <= 1)
                    {
                        if (isToDate == true)
                            result.Data = CheckOutDateTimeFormated - oneDaytemnp;
                        else
                            result.Data = CheckInDateTimeFormated + oneDaytemnp;
                        result.Message = 0.ToString();
                    }
                    else if (hour <= 2 && min < 35 && hour > 1)
                    {
                        if (isHoliday == false && isOffday == false)
                            result.Message = 1.ToString();  //short leave
                        else
                            result.Message = 0.ToString();   // day is off or holiday is full

                        if (isToDate == true)
                            result.Data = CheckOutDateTimeFormated - oneDaytemnp;
                        else
                            result.Data = CheckInDateTimeFormated + oneDaytemnp;
                    }
                    else if (hour >= 2 && hour < 6)
                    {
                        if (isHoliday == false && isOffday == false)
                            result.Message = 2.ToString();   // half leave
                        else
                            result.Message = 0.ToString();   // day is off or holiday is full

                        if (isToDate == true)
                            result.Data = CheckOutDateTimeFormated - oneDaytemnp;
                        else
                            result.Data = CheckInDateTimeFormated + oneDaytemnp;

                    }
                    else
                    {

                        result.Message = 0.ToString();   // concern date is full
                        if (isToDate == true)
                            result.Data = CheckInDateTimeFormated;
                        else
                            result.Data = CheckOutDateTimeFormated;
                    }

                }
                else
                {
                    result.Message = 0.ToString();   // concern date is full
                    if (isToDate == true)
                    {
                        if (LeaveDate < CheckOutDateTimeFormated)
                            result.Data = CheckOutDateTimeFormated - oneDaytemnp;
                        else
                            result.Data = CheckOutDateTimeFormated;
                    }
                    else
                    {
                        if (LeaveDate > CheckOutDateTimeFormated)
                            result.Data = CheckInDateTimeFormated + oneDaytemnp;
                        else
                            result.Data = CheckInDateTimeFormated;
                    }
                   
                }

                result.ResultType = ResultType.Success;


            }
            catch (Exception e)
            {
                // result.Data = null;
                result.ResultType = ResultType.Exception;
                result.Message = e.GetOriginalException().Message;
                result.Exception = e;
            }
            return result;
        }
      
         public Result<bool> UpdateLeaveStatus(long LookLeaveStatusId,long EmployeeLeaveSummaryId)
        {
            var result = new Result<bool>();
            try { 
            string query = @"EXEC [dbo].[LeaveStatusUpdate]
                                @LookLeaveStatusId = " + LookLeaveStatusId + @",
		                        @EmployeeLeaveSummaryId = " + EmployeeLeaveSummaryId;
            var policyList = hrmsWorker.Repository.db.Database.ExecuteSqlCommand(query);

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

        
        public Result<bool> DeleteLeaves(long employeeId, long EmployeeLeaveSummaryId)
        {
            var result = new Result<bool>();
            try
            {
                string query = @"EXEC [dbo].[deleteLeave]
                                @EmployeeId = " + employeeId + @",
		                        @EmployeeLeaveSummaryId = " + EmployeeLeaveSummaryId;
                var policyList = hrmsWorker.Repository.db.Database.ExecuteSqlCommand(query);

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
