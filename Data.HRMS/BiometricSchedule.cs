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
    
    public partial class BiometricSchedule
    {
        public int BiometricScheduleId { get; set; }
        public System.TimeSpan TimeSchedule { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> ProductSaleProfileId { get; set; }
    }
}
