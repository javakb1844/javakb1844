using Data.HRMS;
using Library.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.HRMS;

namespace Services.HRMS
{
 public   class RecruitmentService
    {
        HRMSWorker hrmsWorker = new HRMSWorker();
        public Result<long> CreateRecruitment(Recruitment recruitment)
        {
            var result = new Result<long>();
            try
            {
                hrmsWorker.Repository.Create(recruitment);
                hrmsWorker.SaveChanges();
                result.Data = recruitment.RecruitmentId;
                result.ResultType = ResultType.Success;
            }
            catch (Exception e)
            {
                result.Data = 0;
                result.ResultType = ResultType.Exception;
                result.Message = e.GetOriginalException().Message;
                result.Exception = e;
            }
            return result;
        }

        public Result<Recruitment> GetRecruitmentById(long? id)
        {
            var result = new Result<Recruitment>();
            try
            {

                var dbRecruitment = hrmsWorker.Repository.Read<Recruitment>()
                    .Where(x => x.RecruitmentId == id).FirstOrDefault();
                if (dbRecruitment.IsNotNull())
                {
                    result.Data = dbRecruitment;
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


        public Result<bool> UpdateRecruitmentStatus(long id,long nid,long statusId,long upDateBy)
        {
            var result = new Result<bool>();
            try
            {

                Recruitment dbRecruitment = hrmsWorker.Repository.Read<Recruitment>()
                             .Where(b => b.RecruitmentId == id).FirstOrDefault();
                if (dbRecruitment.IsNotNull())
                {
                    dbRecruitment.LookRecruitmentStatusId = statusId;
                    dbRecruitment.UpdateDate = DateTime.Now;
                    dbRecruitment.UpdatedBy = upDateBy;
                    if(nid>0)
                    dbRecruitment.ForApprovalEmployeeId = nid;
                    hrmsWorker.Repository.Update(dbRecruitment);
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


        public Result<bool> UpdateRecruitment(RecruitmentView ddList)
        {
            var result = new Result<bool>();
            try
            {
                Recruitment model = hrmsWorker.Repository.Read<Recruitment>()
                            .Where(b => b.RecruitmentId == ddList.RecruitmentId).FirstOrDefault();
                if (model.IsNotNull())
                {
                    model.RecruitmentId = ddList.RecruitmentId;
                    model.LookDesignationId = ddList.LookDesignationId;
                    model.LookEmployeeTypeId = ddList.LookEmployeeTypeId;
                    model.Preferred_Start_Date = ddList.Preferred_Start_Date;
                    model.LookJobTypeId = ddList.LookJobTypeId;
                    model.LookDepartmentId = ddList.LookDepartmentId;
                    model.ReportingEmployeeId = ddList.ReportingEmployeeId;
                    model.Length_Of_Term = ddList.Length_Of_Term;
                    model.NoOfPositions = ddList.NoOfPositions;
                    model.ShortSummary = ddList.ShortSummary;
                    model.ForApprovalEmployeeId = ddList.ForApprovalEmployeeId;
                    model.LookGenderId = ddList.LookGenderId;
                    model.PositionDescription = ddList.PositionDescription;
                    model.AdClosingDate = ddList.AdClosingDate;
                    hrmsWorker.Repository.Update(model);
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
        public Result<bool> UpdateRecruitment(Recruitment modeldbRecruitment)
        {
            var result = new Result<bool>();
            try
            {

                //HRPolicy dbHRPolicy = hrmsWorker.Repository.Read<HRPolicy>()
                //             .Where(b => b.HRPolicyId == modelHRPolicy.HRPolicyId).FirstOrDefault();
                //if (dbHRPolicy.IsNotNull())
                //{

                hrmsWorker.Repository.Update(modeldbRecruitment);
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
        public Result<List<Recruitment>> RecruitmentList()
        {
            var result = new Result<List<Recruitment>>();
            try
            {

                var dbRecruitment = hrmsWorker.Repository.Read<Recruitment>()
                    .OrderBy(x => x.LookDesignationId)
                   .ToListSafely().OrderBy(x => x.RecruitmentId).ToListSafely();
                if (dbRecruitment.IsNotNull())
                {
                    result.Data = dbRecruitment;
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

        public Result<RecruitmentView> RecruitmentMaping(Recruitment ddList)
        {
            Result<RecruitmentView> result = new Result<RecruitmentView>();
            RecruitmentView model = new RecruitmentView();
            try
            {
                model.RecruitmentId = ddList.RecruitmentId;
                model.LookDesignationId = ddList.LookDesignationId;
                model.LookJobTypeId = ddList.LookJobTypeId;
                model.LookEmployeeTypeId = ddList.LookEmployeeTypeId;
                model.Length_Of_Term = ddList.Length_Of_Term;
                model.ShortSummary = ddList.ShortSummary;
                model.ReportingEmployeeId = ddList.ReportingEmployeeId;
                model.ForApprovalEmployeeId = ddList.ForApprovalEmployeeId;
                model.Preferred_Start_Date = ddList.Preferred_Start_Date;
                model.LookGenderId = ddList.LookGenderId;
                model.NoOfPositions = ddList.NoOfPositions;
                model.PositionDescription = ddList.PositionDescription;
                model.AdClosingDate = ddList.AdClosingDate;                                    
                result.Data = model;
                result.ResultType = ResultType.Success;
                return result;
            }
            catch (Exception e)
            {
                result.Data = null;
                result.ResultType = ResultType.Exception;
                result.Message = e.GetOriginalException().Message;
                result.Exception = e;
                return result;
            }
        }
    }
}
