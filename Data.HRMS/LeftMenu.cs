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
    
    public partial class LeftMenu
    {
        public long LeftMenuId { get; set; }
        public string MenuName { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string IconClass { get; set; }
        public bool IsActive { get; set; }
        public long ParentId { get; set; }
        public bool HaveChild { get; set; }
        public Nullable<long> DisplayOrder { get; set; }
        public Nullable<long> NewParentId { get; set; }
    }
}