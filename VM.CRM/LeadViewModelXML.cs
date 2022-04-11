using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.WebPages;
namespace VM.CRM
{
    public class LeadViewModelXML
    {
        public string Key { get; set; }
        public string KeyValue { get; set; }
        public string ParentKey { get; set; }
        public int Depth { get; set; }
        public bool HasChild { get; set; }

    }
    public class ListLeadViewModelXML
    {
        public List<LeadViewModelXML> Nodes { get; set; }
    }

    public class ListLeadListCoulmnSettingViewModel
    {
        public long CustTableInfoLID { get; set; }
        public long? EmpCustTableInfoLID { get; set; }
        
        public long ParentLID { get; set; }
        
        public int ColumnId { get; set; }
        
        public string ColumnName { get; set; }
        public bool IsVisible { get; set; }
        public bool? EmpIsVisible { get; set; }
        
    }
    public class ListLeadListCoulmnSettingPostViewModel
    {
        public long[] ColumnId { get; set; }
        public long[] CustTableInfoLID { get; set; }
        public bool IsCurrentUser { get; set; }
    }
    public class LeadSelectionColumnViewModel
    {
        public long Employeeid { get; set; }
      
        public string ASCaption { get; set; }
    }
    public class LeadSelectionColumnByLidViewModel
    {
        public long LID { get; set; }

       
    }
    public class DrawerViewModel
    {
        public List<DrawerDetailViewModel> HtmlTabList { get; set; }
        public String DrawerTitle { get; set; }
    }
    public class DrawerDetailViewModel
    {
        public HelperResult  HtmlTab { get; set; }
        public String HtmlTabTitle { get; set; }
    }

}
