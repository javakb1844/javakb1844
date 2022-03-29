using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Lookups;
using Data.HRMS;
using Library.Core.Services;



namespace Services.HRMS
{
    public class InterviewServices
    {
        HRMSWorker hrmsWorker = new HRMSWorker();
        public Result<bool> CreateInterView (ApplicantInterview interview)
        {
            var result = new Result<bool>();
            try
            {
                result.Data = true;
                result.ResultType = ResultType.Success;
                hrmsWorker.Repository.Create(interview);
                hrmsWorker.SaveChanges();
                result.Data = true;
                result.ResultType = ResultType.Success;
            }
            catch(Exception e )
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
