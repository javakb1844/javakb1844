using Data.HRMS;
using Library.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Look
{
 public   class LookSelectionProcessService
    {
        public Result<bool> CreateSelectionProcess(LookSelectionProcess  lookSelectionProcess)
        {
            var result = new Result<bool>();
            try
            {
                var hrmsWorker = new HRMSWorker();
                hrmsWorker.Repository.Create(lookSelectionProcess);
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

        public Result<LookSelectionProcess> GetSelectionProcessById(long? id)
        {
            var result = new Result<LookSelectionProcess>();
            try
            {
                var hrmsWorker = new HRMSWorker();
                var dbDepartment = hrmsWorker.Repository.Read<LookSelectionProcess>()
                    .Where(x => x.LookSelectionProcessId == id).FirstOrDefault();
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

        public Result<bool> UpdateSelectionProcess(LookSelectionProcess modelDepartment)
        {
            var result = new Result<bool>();
            try
            {
                var hrmsWorker = new HRMSWorker();
                LookSelectionProcess dbDepartment = hrmsWorker.Repository.Read<LookSelectionProcess>()
                             .Where(b => b.LookSelectionProcessId == modelDepartment.LookSelectionProcessId).FirstOrDefault();
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
        public Result<List<LookSelectionProcess>> SelectionProcessList()
        {
            var result = new Result<List<LookSelectionProcess>>();
            try
            {
                var hrmsWorker = new HRMSWorker();
                var dbDepartment = hrmsWorker.Repository.Read<LookSelectionProcess>()
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
