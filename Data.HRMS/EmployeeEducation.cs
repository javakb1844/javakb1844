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
    
    public partial class EmployeeEducation
    {
        public long EmployeeEducationId { get; set; }
        public long EmployeeId { get; set; }
        public Nullable<long> LookQualificationId { get; set; }
        public string EducationYear { get; set; }
        public Nullable<long> LookInstitutionId { get; set; }
        public string Grade { get; set; }
    }
}