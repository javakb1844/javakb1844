using Data.HRMS;
using Library.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.HRMS
{
  public  class HRRecruitmentProcessService
    {
        HRMSWorker hrmsWorker = new HRMSWorker();
        public Result<bool> CreateProcess(HRRecruitmentProcess lookHRRecruitmentProcess)
        {
            var result = new Result<bool>();
            try
            {
               
                hrmsWorker.Repository.Create(lookHRRecruitmentProcess);
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

        public Result<bool> DeleteProcessByRecruitmentId(long id)
        {
            var result = new Result<bool>();
            try
            {
                string sql = "delete from HRRecruitmentProcess where RecruitmentId=" + id;
                var deleteQualification = hrmsWorker.Repository.db.Database.ExecuteSqlCommand(sql);
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

        public Result<HRRecruitmentProcess> GetHRRecruitmentProcessById(long? id)
        {
            var result = new Result<HRRecruitmentProcess>();
            try
            {
               
                var dbDepartment = hrmsWorker.Repository.Read<HRRecruitmentProcess>()
                    .Where(x => x.HRRecruitmentProcessId == id).FirstOrDefault();
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

        public Result<bool> UpdateHRRecruitmentProcess(HRRecruitmentProcess modelDepartment)
        {
            var result = new Result<bool>();
            try
            {
               
                HRRecruitmentProcess dbDepartment = hrmsWorker.Repository.Read<HRRecruitmentProcess>()
                             .Where(b => b.HRRecruitmentProcessId == modelDepartment.HRRecruitmentProcessId).FirstOrDefault();
                if (dbDepartment.IsNotNull())
                {
                    ///  dbDepartment.DepartmentName = modelDepartment.DepartmentName;
                    hrmsWorker.Repository.Update(dbDepartment);
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
        public Result<List<HRRecruitmentProcess>> HRRecruitmentProcessList()
        {
            var result = new Result<List<HRRecruitmentProcess>>();
            try
            {
                
                var dbDepartment = hrmsWorker.Repository.Read<HRRecruitmentProcess>()
                   .ToListSafely();
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
    }
}
