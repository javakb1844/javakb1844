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
    public class LookDesignationService
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

        public Result<List<LookDesignation>> GetDesignationList()
        {
            var result = new Result<List<LookDesignation>>();
            try
            {
                var dbDesignations = hWorker.Repository.Read<LookDesignation>().ToListSafely();             
                result.Data = dbDesignations;
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
        public Result<LookDesignation> GetDesignation(long dId)
        {
            var result = new Result<LookDesignation>();
            try
            {
                var dbDesignation = hWorker.Repository.Read<LookDesignation>()
                    .Where(x => x.LookDesignationId.Equals(dId)).FirstOrDefault();

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
        public static Result<string> GetDesignationName(long? dId)
        {
            var result = new Result<string>();
            HRMSWorker hWorker = new HRMSWorker();
            try
            {
                 var dbDesignation = hWorker.Repository.Read<LookDesignation>()
                    .Where(x=>x.LookDesignationId== dId)
                     .FirstOrDefault();
                     
                result.Data = dbDesignation.DesignationName;
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
        public Result<bool> InsertDesignation (LookDesignation newDesignation)
        {
            var result = new Result<bool>();
            try
            {
                hWorker.Repository.Create(newDesignation);
                hWorker.SaveChanges();
                result.ResultType = ResultType.Success;
                result.Data = true;
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

        public Result<bool> UpdateDesignation(LookDesignation newDesignation)
        {
            var result = new Result<bool>();
            try
            {
            //    var existing = hWorker.Repository.Read<LookDesignation>()
            //        .Where(x => x.LookDesignationId ==newDesignation.LookDesignationId);
                hWorker.Repository.Update(newDesignation);
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
