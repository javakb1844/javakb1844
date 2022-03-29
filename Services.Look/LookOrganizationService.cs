using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.HRMS;
using Library.Core.Services;
using Library.Extensions;

namespace Services.Look
{
   public class LookOrganizationService
    {
        HRMSWorker hrWorker = new HRMSWorker();
        public Result<LookOrganization> GetOrganization(long id)
        {
            var result = new Result<LookOrganization>();
            try { 
            var organization = hrWorker.Repository.Read<LookOrganization>()
                .Where(x => x.LookOrganizationId.Equals(id)).FirstOrDefault();
                result.Data = organization;
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
        public static Result<List<LookOrganization>> GetOrganizations(int LookCompanyStatusId, int ProductSaleProfileId)
        {
            HRMSWorker hrWorker = new HRMSWorker();
            var result = new Result<List<LookOrganization>>();
            try
            {  if (ProductSaleProfileId == 0)
                {
                    var organization = hrWorker.Repository.Read<LookOrganization>()
                      .Where(x => x.LookCompanyStatusId == LookCompanyStatusId ).ToListSafely();
                    result.Data = organization;
                    result.ResultType = ResultType.Success;
                }
                else
                {
                    var organization = hrWorker.Repository.Read<LookOrganization>()
                        .Where(x => x.LookCompanyStatusId == LookCompanyStatusId && x.ProductSaleProfileId == ProductSaleProfileId).ToListSafely();
                    result.Data = organization;
                    result.ResultType = ResultType.Success;
                }
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
        public Result<LookOrganization> CreateOrganization(LookOrganization organization)
        {
            var result = new Result<LookOrganization>();
            try
            {
                hrWorker.Repository.Create(organization);
                hrWorker.SaveChanges();
                var newOrgt = organization;
                result.Data = organization;
                result.ResultType = ResultType.Success;  
            }
            catch(Exception e)
            {
                //result.Data = false;
                result.ResultType = ResultType.Exception;
                result.Message = e.GetOriginalException().Message;
                result.Exception = e;
            }
            return result;
        }
        public Result<bool> UpdateOrganization(LookOrganization organization)
        {
            var result = new Result<bool>();
            try
            {
                var hrmsWorker = new HRMSWorker();
               
                   
                    hrmsWorker.Repository.Update(organization);
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
    }
}
