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
    
    public partial class Tax
    {
        public long TaxId { get; set; }
        public string LimitName { get; set; }
        public Nullable<decimal> StartLimit { get; set; }
        public Nullable<decimal> MaxLimit { get; set; }
        public Nullable<decimal> FixTax { get; set; }
        public Nullable<decimal> TaxPercentageToMaxLimit { get; set; }
    }
}