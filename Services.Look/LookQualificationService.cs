using Data.HRMS;
using Library.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Look
{
   public class LookQualificationService
    {
        HRMSWorker hrmsWorker = new HRMSWorker();
        public Result<List<LookQualification>> QualificationList()
        {
            var result = new Result<List<LookQualification>>();
            try
            {
              
                var dbQualification = hrmsWorker.Repository.Read<LookQualification>()
                   .ToListSafely();
                if (dbQualification.IsNotNull())
                {
                    result.Data = dbQualification;
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
        public Result<List<LookQualificationType>> QualificationTypeList()
        {
            var result = new Result<List<LookQualificationType>>();
            try
            {

                var dbQualification = hrmsWorker.Repository.Read<LookQualificationType>()
                   .ToListSafely();
                if (dbQualification.IsNotNull())
                {
                    result.Data = dbQualification;
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
        public Result<LookQualification> GetQualification(long dId)
        {
            var result = new Result<LookQualification>();
            try
            {
                var dbDesignation = hrmsWorker.Repository.Read<LookQualification>()
                    .Where(x => x.LookQualificationId.Equals(dId)).FirstOrDefault();

                result.Data = dbDesignation;
                result.ResultType = ResultType.Success;
            }
            catch (Exception e)
            {
                result.Data = null;
                result.ResultType = ResultType.Success;
                result.Exception = e;
                result.Message = e.GetOriginalException().Message;
            }
            return result;
        }

        public Result<LookQualificationType> GetQualificationType(long dId)
        {
            var result = new Result<LookQualificationType>();
            try
            {
                var dbDesignation = hrmsWorker.Repository.Read<LookQualificationType>()
                    .Where(x => x.LookQualificationTypeId.Equals(dId)).FirstOrDefault();

                result.Data = dbDesignation;
                result.ResultType = ResultType.Success;
            }
            catch (Exception e)
            {
                result.Data = null;
                result.ResultType = ResultType.Success;
                result.Exception = e;
                result.Message = e.GetOriginalException().Message;
            }
            return result;
        }


        public Result<LookQualification> InsertQualification(LookQualification newDesignation)
        {
            var result = new Result<LookQualification>();
            try
            {
                hrmsWorker.Repository.Create(newDesignation);
                hrmsWorker.SaveChanges();
                var newQual = newDesignation;
                result.ResultType = ResultType.Success;
                result.Data = newQual;
            }
            catch (Exception e)
            {
                //result.Data = false;
                result.ResultType = ResultType.Exception;
                result.Message = e.GetOriginalException().Message;
                result.Exception = e;
            }
            return result;
        }

        public Result<bool> InsertQualification(LookQualificationType newDesignation)
        {
            var result = new Result<bool>();
            try
            {
                hrmsWorker.Repository.Create(newDesignation);
                hrmsWorker.SaveChanges();
                result.ResultType = ResultType.Success;
                result.Data = true;
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

        public Result<bool> UpdateQualification(LookQualification newDesignation)
        {
            var result = new Result<bool>();
            try
            {
                //    var existing = hWorker.Repository.Read<LookDesignation>()
                //        .Where(x => x.LookDesignationId ==newDesignation.LookDesignationId);
                hrmsWorker.Repository.Update(newDesignation);
                hrmsWorker.SaveChanges();
                result.ResultType = ResultType.Success;
                result.Data = true;
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

        public Result<bool> UpdateQualification(LookQualificationType newDesignation)
        {
            var result = new Result<bool>();
            try
            {
                //    var existing = hWorker.Repository.Read<LookDesignation>()
                //        .Where(x => x.LookDesignationId ==newDesignation.LookDesignationId);
                hrmsWorker.Repository.Update(newDesignation);
                hrmsWorker.SaveChanges();
                result.ResultType = ResultType.Success;
                result.Data = true;
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
