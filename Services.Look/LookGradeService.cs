using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.HRMS;
using VM.Look;
using Library.Extensions;
using Library.Core.Services;

namespace Services.Look
{
 public   class LookGradeService
    {
        HRMSWorker hWorker = new HRMSWorker();

        //public Result<List<LookDesignationViewModel>> GetDesignationList()
        //{
        //    var result = new Result<List<LookDesignationViewModel>>();
        //    try
        //    {
        //        var dbDesignations = hWorker.Repository.Read<LookDesignation>().ToListSafely();
        //        List<LookDesignationViewModel> desginationsList = null;
        //        if (dbDesignations.Count > 0)
        //        {
        //            desginationsList = new List<LookDesignationViewModel>();
        //            foreach (var dbDesignation in dbDesignations)
        //            {
        //                LookDesignationViewModel modelDesignation = new LookDesignationViewModel();

        //                modelDesignation.DesignationName = dbDesignation.DesignationName;
        //                modelDesignation.LookDesignationId = modelDesignation.LookDesignationId;
        //                modelDesignation.LookDepartmentId = modelDesignation.LookDepartmentId;

        //                desginationsList.Add(modelDesignation);
        //            }
        //        }
        //        result.Data = desginationsList;
        //        result.ResultType = ResultType.Success;
        //    }
        //    catch (Exception e)
        //    {
        //        result.Data = null;
        //        result.ResultType = ResultType.Success;
        //        result.Exception = e;
        //        result.Message = e.GetOriginalException().Message;
        //    }
        //    return result;
        //}

        public Result<List<LookGrade>> GetGradeList()
        {
            var result = new Result<List<LookGrade>>();
            try
            {
                var dbGrade = hWorker.Repository.Read<LookGrade>().ToListSafely();
                result.Data = dbGrade;
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
        public Result<LookGrade> GetGrade(int dId)
        {
            var result = new Result<LookGrade>();
            try
            {
                var dbGrade = hWorker.Repository.Read<LookGrade>()
                    .Where(x => x.LookGradeId.Equals(dId)).FirstOrDefault();

                result.Data = dbGrade;
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
        public Result<bool> InsertGrade(LookGrade newGrade)
        {
            var result = new Result<bool>();
            try
            {
                hWorker.Repository.Create(newGrade);
                hWorker.SaveChanges();
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

        public Result<bool> UpdateGrade(LookGrade newGrade)
        {
            var result = new Result<bool>();
            try
            {
                //    var existing = hWorker.Repository.Read<LookDesignation>()
                //        .Where(x => x.LookDesignationId ==newDesignation.LookDesignationId);
                hWorker.Repository.Update(newGrade);
                hWorker.SaveChanges();
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