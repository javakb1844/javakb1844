using Data.HRMS;
using Library.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.HRMS;
using VM.Look;

namespace Services.HRMS
{
  public  class HRPolicyService
    {
        HRMSWorker hrmsWorker = new HRMSWorker();
        public Result<bool> CreateHRPolicy(HRPolicy hrPolicy)
        {
            var result = new Result<bool>();
            try
            {
              
                hrmsWorker.Repository.Create(hrPolicy);
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

        public Result<HRPolicy> GetHRPolicyById(long? id)
        {
            var result = new Result<HRPolicy>();
            try
            {
               
                var dbHRPolicy = hrmsWorker.Repository.Read<HRPolicy>()
                    .Where(x => x.HRPolicyId == id).FirstOrDefault();
                if (dbHRPolicy.IsNotNull())
                {
                    result.Data = dbHRPolicy;
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

        public Result<BiometricSync> GetBioDateTimeSync()
        {
            var result = new Result<BiometricSync>();
            try
            {
                string sql = "Exec dbo.GetLastDataSyncDateTime";
                var dbHRPolicy = hrmsWorker.Repository.db.Database.SqlQuery<BiometricSync>(sql).FirstOrDefault();
             
                if (dbHRPolicy.IsNotNull())
                {
                    result.Data = dbHRPolicy;
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
        public Result<List<HRPolicyViewModelNew>> GetHRPolicyByDesignationAndGroup(long GroupTypeId, long LookDesignationId)
        {
            var result = new Result<List<HRPolicyViewModelNew>>();
            HRMSWorker hrWorker = new HRMSWorker();
            try
            {
                string query = @"EXEC [dbo].[GetHRPolicyByDesignationAndGroup]
                                 @LookDesignationId  = " + LookDesignationId + @",
                                 @GroupTypeId  =" + GroupTypeId;
                               

                var attendance = hrWorker.Repository.db.Database.SqlQuery<HRPolicyViewModelNew>(query).ToListSafely();
                if (attendance.CountedPositive())
                {
                    result.Data = attendance;
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
                result.Message = e.GetOriginalException().Message;
                result.Exception = e;
            }
            return result;
        }

        public Result<List<HRPolicyViewModelNew>> GetOfficeTimingPolicy(int GroupTypeId, long LookDesignationId)
        {
            var result = new Result<List<HRPolicyViewModelNew>>();
            HRMSWorker hrWorker = new HRMSWorker();
            try
            {
                string query = @"EXEC [dbo].[GetOfficeTimingPolicy]
                                 @HRPolicyCaptionId  = " + LookDesignationId;                              


                var attendance = hrWorker.Repository.db.Database.SqlQuery<HRPolicyViewModelNew>(query).ToListSafely();
                if (attendance.CountedPositive())
                {
                    result.Data = attendance;
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
                result.Message = e.GetOriginalException().Message;
                result.Exception = e;
            }
            return result;
        }
        public Result<bool> UpdateHRPolicy(HRPolicy modelHRPolicy)
        {
            var result = new Result<bool>();
            try
            {
              
                //HRPolicy dbHRPolicy = hrmsWorker.Repository.Read<HRPolicy>()
                //             .Where(b => b.HRPolicyId == modelHRPolicy.HRPolicyId).FirstOrDefault();
                //if (dbHRPolicy.IsNotNull())
                //{
                    
                    hrmsWorker.Repository.Update(modelHRPolicy);
                    hrmsWorker.SaveChanges();
                //}

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

        public Result<List<HRPolicyGroupViewModel>> HRPolicyGroupList()
        {
            var result = new Result<List<HRPolicyGroupViewModel>>();
            try
            {
                string sql = @"select distinct HRPolicy.LookDesignationId, LookDesignation.DesignationName ,LookPolicyGroup.PolicyGroupName 
                                from HRPolicy 
                                inner join LookPolicy on LookPolicy.LookPolicyId= HRPolicy.LookPolicyId
                                inner join LookPolicyGroup on LookPolicyGroup.LookPolicyGroupId= LookPolicy.LookPolicyGroupId
                                inner join LookDesignation on LookDesignation.LookDesignationId=HRPolicy.LookDesignationId";

                var dbHRPolicy = hrmsWorker.Repository.db.Database.SqlQuery<HRPolicyGroupViewModel>(sql).ToListSafely().OrderBy(x => x.LookDesignationId).ToListSafely();
               
                if (dbHRPolicy.IsNotNull())
                {
                    result.Data = dbHRPolicy;
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

        public Result<List<PolicyGroupDetailListViewModel>> PolicyGroupDetailList(long PolicyGroupid)
        {
            var result = new Result<List<PolicyGroupDetailListViewModel>>();
            try
            {
                string sql = @"select LookPolicy.LookPolicyGroupId, LookPolicyGroup.PolicyGroupName , LookPolicy.PolicyName, HRPolicy.PolicyValue, LookPolicy.PolicyUnit
                            from HRPolicy
                            inner join LookPolicy on LookPolicy.LookPolicyId= HRPolicy.LookPolicyId
                            inner join LookPolicyGroup on LookPolicyGroup.LookPolicyGroupId= LookPolicy.LookPolicyGroupId
                            inner join LookDesignation on LookDesignation.LookDesignationId= HRPolicy.LookDesignationId
                            where HRPolicy.LookDesignationId= " + PolicyGroupid;

                var dbHRPolicy = hrmsWorker.Repository.db.Database.SqlQuery<PolicyGroupDetailListViewModel>(sql).ToListSafely().OrderBy(x => x.LookPolicyGroupId).ToListSafely();

                if (dbHRPolicy.IsNotNull())
                {
                    result.Data = dbHRPolicy;
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
        public Result<List<HRPolicy>> HRPolicyList()
        {
            var result = new Result<List<HRPolicy>>();
            try
            {
               
                var dbHRPolicy = hrmsWorker.Repository.Read<HRPolicy>()
                    .OrderBy(x=>x.LookDesignationId)
                   .ToListSafely().OrderBy(x=>x.LookPolicyId).ToListSafely();
                if (dbHRPolicy.IsNotNull())
                {
                    result.Data = dbHRPolicy;
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

        public Result<List<LookPolicyViewModel>> GetHRPolicyByDesignation(long lookDesignationId,long lookPolicyGroupId)
        {
            var result = new Result<List<LookPolicyViewModel>>();
            try
            {
                string query = @"EXEC [dbo].[Policies]
                                @LookDesignationId = "+ lookDesignationId + @",
		                        @LookPolicyGroupId = "+ lookPolicyGroupId;
                var policyList = hrmsWorker.Repository.db.Database.SqlQuery<LookPolicyViewModel>(query).ToListSafely();
                if (policyList.IsNotNull())
                {
                    result.Data = policyList;
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

        public Result<List<LookPolicyViewModel>> GetEmployeePolicyPolicyByDesignationAndEmployeeId(long? lookDesignationId, long lookPolicyGroupId, long employeeId, bool isEmployeeOnly)
        {
            var result = new Result<List<LookPolicyViewModel>>();
            try
            { /// LookDesignationId is not used in case of IsEmployeeOnly is true
                int _isEmployeeOnly = 0;
                if(isEmployeeOnly==true)
                    _isEmployeeOnly = 1;
                string query = @"EXEC  [dbo].[EmployeePolicies]
                        @LookDesignationId = " + lookDesignationId + @",
		                @LookPolicyGroupId = " + lookPolicyGroupId + @",
		                @EmployeeId = " + employeeId + @",
                        @IsEmployeeOnly= " + _isEmployeeOnly;

                var policyList = hrmsWorker.Repository.db.Database.SqlQuery<LookPolicyViewModel>(query).ToListSafely();
                if (policyList.IsNotNull())
                {
                    result.Data = policyList;
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
