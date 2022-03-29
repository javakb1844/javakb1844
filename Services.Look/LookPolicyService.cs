using Data.HRMS;
using Library.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.HRMS;
using VM.Look;

namespace Services.Look
{
  public  class LookPolicyService
    {
        HRMSWorker hrmsWorker = new HRMSWorker();
         public Result<bool> CreatePolicy(LookPolicy lookPolicy)
         {
             var result = new Result<bool>();
             try
             {
                hrmsWorker.Repository.Create(lookPolicy);
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

        public Result<bool> CreatePolicyGroup(LookPolicyGroup lookPolicy)
        {
            var result = new Result<bool>();
            try
            {
                hrmsWorker.Repository.Create(lookPolicy);
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

        public Result<LookPolicyGroup> GetPolicyGroupById(long? id)
         {
             var result = new Result<LookPolicyGroup>();
             try
             {
                 var dbPolicyGroup = hrmsWorker.Repository.Read<LookPolicyGroup>()
                     .Where(x => x.LookPolicyGroupId == id).FirstOrDefault();
                 if (dbPolicyGroup.IsNotNull())
                 {
                     result.Data = dbPolicyGroup;
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

        public Result<LookPolicy> GetPolicyById(long? id)
        {
            var result = new Result<LookPolicy>();
            try
            {
                var dbPolicy = hrmsWorker.Repository.Read<LookPolicy>()
                    .Where(x => x.LookPolicyGroupId == id).FirstOrDefault();
                if (dbPolicy.IsNotNull())
                {
                    result.Data = dbPolicy;
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

        public Result<bool> UpdatePolicy(LookPolicy modelPolicy)
         {
             var result = new Result<bool>();
             try
             {

                LookPolicy dbPolicy = hrmsWorker.Repository.Read<LookPolicy>()
                              .Where(b => b.LookPolicyId == modelPolicy.LookPolicyId).FirstOrDefault();
                 if (dbPolicy.IsNotNull())
                 {
                    dbPolicy.PolicyName = modelPolicy.PolicyName;
                     hrmsWorker.Repository.Update(dbPolicy);
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

        public Result<bool> UpdatePolicyGroup(LookPolicyGroup modelPolicyGroup)
        {
            var result = new Result<bool>();
            try
            {

                LookPolicyGroup dbPolicy = hrmsWorker.Repository.Read<LookPolicyGroup>()
                              .Where(b => b.LookPolicyGroupId == modelPolicyGroup.LookPolicyGroupId).FirstOrDefault();
                if (dbPolicy.IsNotNull())
                {
                    dbPolicy.PolicyGroupName = modelPolicyGroup.PolicyGroupName;
                    hrmsWorker.Repository.Update(dbPolicy);
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
        public Result<List<LookPolicy>> PolicyList()
        {
            var result = new Result<List<LookPolicy>>();
            try
            {
               
                var dbDepartment = hrmsWorker.Repository.Read<LookPolicy>()
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
        public Result<List<LookPolicyGroup>> PolicyGroupList()
        {
            var result = new Result<List<LookPolicyGroup>>();
            try
            {
                
                var dbDepartment = hrmsWorker.Repository.Read<LookPolicyGroup>()
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


        public Result<List<LookGENPolicyViewModel>> GetPolicyByHRPolicyCaptionId(int HRPolicyCaptionId ,int LookPolicyGroupId)
        {
            var result = new Result<List<LookGENPolicyViewModel>>();
            try
            {
                long roleid = Convert.ToInt64(System.Web.HttpContext.Current.Session["RoleId"]);
                int ProductSaleProfileId = Convert.ToInt32(System.Web.HttpContext.Current.Session["ProductSaleProfileId"]);
                long SelectedOrganizationId = Convert.ToInt64(System.Web.HttpContext.Current.Session["SelectedOrganizationId"]);
                string query = @"EXEC dbo.GetPolicyByHRPolicyCaptionId  
                       @HRPolicyCaptionId =" + HRPolicyCaptionId + @",
                       @ProductSaleProfileId  =" + ProductSaleProfileId + @",
                       @OrganizationId  =" + SelectedOrganizationId + @",
                       @LookPolicyGroupId  =" + LookPolicyGroupId;              
                
               
                var str = hrmsWorker.Repository.db.Database.SqlQuery<LookGENPolicyViewModel>(query).ToListSafely();
                result.Data = str;
                result.ResultType = ResultType.Success;
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


        /*  public Result<List<LookDepartment>> GetDepartmentList()
          {
              var result = new Result<List<LookDepartment>>();
              try
              {
                  var hrmsWorker = new HRMSWorker();
                  var dbDepartment = hrmsWorker.Repository.Read<LookDepartment>().ToListSafely();

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

          }*/
    }
}
