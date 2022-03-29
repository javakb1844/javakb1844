using Data.HRMS;
using Library.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Look
{
 public   class LookInstitutionService
    {
        HRMSWorker hrmsWorker = new HRMSWorker();
        public Result<LookInstitution> CreateInstitution(LookInstitution lookInstitution)
        {
            var result = new Result<LookInstitution>();
            try
            {               
                hrmsWorker.Repository.Create(lookInstitution);
                hrmsWorker.SaveChanges();
                var newInst = lookInstitution;
                result.Data = newInst;
                result.ResultType = ResultType.Success;
            }
            catch (Exception e)
            {
               // result.Data = false;
                result.ResultType = ResultType.Exception;
                result.Message = e.GetOriginalException().Message;
                result.Exception = e;
            }
            return result;
        }

        public Result<LookInstitution> GetInstitutionById(long? id)
        {
            var result = new Result<LookInstitution> ();
            try
            {
                var hrmsWorker = new HRMSWorker();
                var dbDepartment = hrmsWorker.Repository.Read<LookInstitution> ()
                    .Where(x => x.LookInstitutionId == id).FirstOrDefault();
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

        public Result<bool> UpdateInstitution(LookInstitution modelInstitution)
        {
            var result = new Result<bool>();
            try
            {
                LookInstitution dbInstitution = hrmsWorker.Repository.Read<LookInstitution>()
                             .Where(b => b.LookInstitutionId == modelInstitution.LookInstitutionId).FirstOrDefault();
                if (dbInstitution.IsNotNull())
                {
                    dbInstitution.InstitutionName = modelInstitution.InstitutionName;
                    hrmsWorker.Repository.Update(dbInstitution);
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
        public Result<List<LookInstitution>> InstitutionList()
        {
            var result = new Result<List<LookInstitution>>();
            try
            {
                var dbInstitution = hrmsWorker.Repository.Read<LookInstitution>()
                   .ToListSafely();
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

        public Result<bool> UpdateDepartment(LookDepartment modelDepartment)
        {
            var result = new Result<bool>();
            try
            { var hrmsWorker = new HRMSWorker();
                LookDepartment dbDepartment = hrmsWorker.Repository.Read<LookDepartment>()
                             .Where(b => b.LookDepartmentId == modelDepartment.LookDepartmentId).FirstOrDefault();
                if (dbDepartment.IsNotNull())
                {
                    dbDepartment.DepartmentName = modelDepartment.DepartmentName;
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
        public Result<List<LookDepartment>> DepartmentList()
        {
            var result = new Result<List<LookDepartment>>();
            try
            {
                var hrmsWorker = new HRMSWorker();
                var dbDepartment = hrmsWorker.Repository.Read<LookDepartment>()
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
