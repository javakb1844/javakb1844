//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Data.HRMS
{
    using System;
    using System.Collections.Generic;
    
    public partial class Recruitment
    {
        public long RecruitmentId { get; set; }
        public Nullable<long> LookDesignationId { get; set; }
        public Nullable<long> LookBranchId { get; set; }
        public Nullable<long> LookEmployeeTypeId { get; set; }
        public Nullable<System.DateTime> Preferred_Start_Date { get; set; }
        public Nullable<System.DateTime> AdClosingDate { get; set; }
        public Nullable<long> LookJobTypeId { get; set; }
        public Nullable<long> LookDepartmentId { get; set; }
        public Nullable<long> ReportingEmployeeId { get; set; }
        public Nullable<long> Length_Of_Term { get; set; }
        public long LookRecruitmentStatusId { get; set; }
        public Nullable<long> NoOfPositions { get; set; }
        public Nullable<long> LookGenderId { get; set; }
        public string PositionDescription { get; set; }
        public System.DateTime CreateDate { get; set; }
        public long CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public Nullable<long> ForApprovalEmployeeId { get; set; }
        public string ShortSummary { get; set; }
    }
}