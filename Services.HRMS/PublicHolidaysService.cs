using Data.HRMS;
using Library.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.HRMS
{
  public  class PublicHolidaysService
    {
        HRMSWorker hrmsWorker = new HRMSWorker();
        public Result<bool> CreatePublicHoliday(PublicHoliday publicHoliday)
        {
            var result = new Result<bool>();
            try
            {
                hrmsWorker.Repository.Create(publicHoliday);
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

        public Result<PublicHoliday> GetPublicHolidayById(long? id)
        {
            var result = new Result<PublicHoliday>();
            try
            {
               
                var dbPublicHoliday = hrmsWorker.Repository.Read<PublicHoliday>()
                    .Where(x => x.PublicHolidayId == id).FirstOrDefault();
                if (dbPublicHoliday.IsNotNull())
                {
                    result.Data = dbPublicHoliday;
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

        public Result<List<PublicHoliday>> GetPublicHolidayBetweenDates(DateTime fromDate, DateTime toDate)
        {
            var result = new Result<List<PublicHoliday>>();
            try
            {
               
                var dbPublicHoliday = hrmsWorker.Repository.Read<PublicHoliday>()
                    .Where(x => x.HoIidayDate>= fromDate.Date && x.HoIidayDate <= toDate.Date).ToListSafe();
                if (dbPublicHoliday.IsNotNull())
                {
                    result.Data = dbPublicHoliday;
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

        public Result<bool> UpdatePublicHoliday(PublicHoliday modelPublicHoliday)
        {
            var result = new Result<bool>();
            try
            {

             //   PublicHoliday dbPublicHoliday = hrmsWorker.Repository.Read<PublicHoliday>()
             //                .Where(b => b.PublicHolidayId == modelPublicHoliday.PublicHolidayId).FirstOrDefault();
                if (modelPublicHoliday.IsNotNull())
                {
                   // dbPublicHoliday.DepartmentName = modelPublicHoliday.DepartmentName;
                    hrmsWorker.Repository.Update(modelPublicHoliday);
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
        public Result<List<PublicHoliday>> PublicHolidaytList()
        {
            var result = new Result<List<PublicHoliday>>();
            try
            {
               
                var dbPublicHoliday = hrmsWorker.Repository.Read<PublicHoliday>()
                   .ToListSafely();
                if (dbPublicHoliday.IsNotNull())
                {
                    result.Data = dbPublicHoliday;
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
