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
    
    public partial class ProductSaleProfile
    {
        public int ProductSaleProfileId { get; set; }
        public string Name { get; set; }
        public Nullable<long> LookCityId { get; set; }
        public Nullable<long> LookCountryId { get; set; }
        public string Address { get; set; }
        public int LookCustomerStatusId { get; set; }
        public Nullable<System.DateTime> RegistrationDatePk { get; set; }
        public System.DateTime UITCDateTime { get; set; }
    }
}
