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
    
    public partial class EventLog
    {
        public long LogID { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string Query { get; set; }
        public byte[] CreateDate { get; set; }
        public Nullable<long> AppID { get; set; }
        public Nullable<System.DateTime> InsertDate { get; set; }
        public string ProjectName { get; set; }
        public Nullable<long> UserId { get; set; }
    }
}
