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
    
    public partial class BioMetricData
    {
        public long BioMetricDataId { get; set; }
        public int BioMetricId { get; set; }
        public System.DateTime DateTimeRecord { get; set; }
        public string OnBioMetricName { get; set; }
        public Nullable<System.DateTime> DateOnly { get; set; }
        public Nullable<System.DateTime> TimeOnly { get; set; }
        public Nullable<int> dwInOutMode { get; set; }
        public Nullable<bool> IsEdit { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public Nullable<long> EmployeeId { get; set; }
        public Nullable<int> ProductSaleProfileId { get; set; }
        public string BioMetricSerialNo { get; set; }
        public string BioMetricDeviceBrand { get; set; }
        public Nullable<long> BioMetricInfoId { get; set; }
    }
}
