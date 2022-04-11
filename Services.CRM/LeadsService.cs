using Data.HRMS;
using Library.Core;
using Library.Core.Services;
using Library.Lookups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Xml;
using System.Xml.Linq;
using VM.CRM;

namespace Services.CRM
{
    public class LeadsService
    {
        static string className = "LeadsService";
        // SharedUtilities.AddLog(className, ex);
       
        public Result<List<ListLeadViewModelXML>> GetLeadsList()
        {
           var result = new Result<List<ListLeadViewModelXML>>();
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                HRMSWorker hWorker = new HRMSWorker();               
                long roleid = Convert.ToInt64(System.Web.HttpContext.Current.Session["RoleId"]);
                int ProductSaleProfileId = Convert.ToInt32(System.Web.HttpContext.Current.Session["ProductSaleProfileId"]);
                long SelectedOrganizationId = Convert.ToInt64(System.Web.HttpContext.Current.Session["SelectedOrganizationId"]);
                long EmployeeId=Convert.ToInt64(System.Web.HttpContext.Current.Session["EmployeeId"]);
                // EmployeeId = 21;
                string query = @"EXEC	[dbo].[Lead_GetLeadListSlectionColumn]
		                        @ProductSaleProfileId = " + ProductSaleProfileId + @",
		                        @EmployeeId = " + EmployeeId+ @",
                                    @TableId=" + (int)EnumLookCustTableInfo.LeadListColumnSetting;
                var LeadSelection = hWorker.Repository.db.Database.SqlQuery<LeadSelectionColumnViewModel>(query).ToListSafely();

                if (LeadSelection.CountedPositive())
                {
                    var emselection = LeadSelection.Where(x => x.Employeeid > 0).ToListSafely();
                    var emselectionZero = LeadSelection.Where(x => x.Employeeid == 0).ToListSafely();
                    if (emselection.CountedPositive()) {
                        var tt = emselection.Select(x => x.ASCaption).ToArray();
                        string hh = string.Join(",", tt);
                        query = @"EXEC	[dbo].[GetLeadByResponsiblePersonv3]
		                        @ProductSaleProfileId = " + ProductSaleProfileId + @",
		                        @EmployeeId = " + 21 + @",
                                @leadid=0,
                                @SelectQuery='" + hh + @"'";
                    }else if (emselectionZero.CountedPositive())
                    {
                        var tt = emselectionZero.Select(x => x.ASCaption).ToArray();
                        string hh = string.Join(",", tt);
                        query = @"EXEC	[dbo].[GetLeadByResponsiblePersonv3]
		                        @ProductSaleProfileId = " + ProductSaleProfileId + @",
		                        @EmployeeId = " + 21 + @",
                                 @leadid=0,
                                @SelectQuery='" + hh + @"'";
                    }
                    var attendance = hWorker.Repository.db.Database.SqlQuery<string>(query).ToListSafely();
                    //var hhh= attendance[0] as System.Dynamic.ExpandoObject;

                    if (attendance.CountedPositive())
                    {
                        string temp = string.Join("", attendance);
                        xmlDoc.LoadXml(temp);
                    }

                    var nodes = xmlDoc.DocumentElement?.SelectNodes(" / Leads/Lead");
                    List<ListLeadViewModelXML> abc = new List<ListLeadViewModelXML>();
                    foreach (var item in nodes)
                    {
                        var ok = new ListLeadViewModelXML();
                        List<LeadViewModelXML> res = new List<LeadViewModelXML>();
                        var tt = item as XmlElement;
                        TraverseXMLNode(tt, 0, ref res);
                        ok.Nodes = res;
                        abc.Add(ok);
                        res = null;
                    }
                    // var json = new JavaScriptSerializer().Serialize(abc);              
                    if (attendance.CountedPositive())
                    {
                        result.Data = abc;
                    }
                    else
                    {
                        result.Data = null;
                    }
                    result.ResultType = ResultType.Success;
                }else
                {
                    result.Data = null;
                    result.ResultType = ResultType.Failure;
                }
            }
            catch (Exception e)
            {
                SharedUtilities.AddLog(className, e);
                result.Data = null;
                result.ResultType = ResultType.Exception;
                result.Message = e.GetOriginalException().Message;
               // result.Exception = e;
            }
            return result;
        }

        public Result<List<ListLeadViewModelXML>> GetLeadsListByLid(LeadSelectionColumnByLidViewModel model)
        {
            var result = new Result<List<ListLeadViewModelXML>>();
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                HRMSWorker hWorker = new HRMSWorker();
                model.LID = 1;
                long roleid = Convert.ToInt64(System.Web.HttpContext.Current.Session["RoleId"]);
                int ProductSaleProfileId = Convert.ToInt32(System.Web.HttpContext.Current.Session["ProductSaleProfileId"]);
                long SelectedOrganizationId = Convert.ToInt64(System.Web.HttpContext.Current.Session["SelectedOrganizationId"]);
                long EmployeeId = Convert.ToInt64(System.Web.HttpContext.Current.Session["EmployeeId"]);
                // EmployeeId = 21;
                string query = @"EXEC	[dbo].[Lead_GetLeadListSlectionColumn]
		                        @ProductSaleProfileId = " + ProductSaleProfileId + @",
		                        @EmployeeId = " + EmployeeId + @",
                                    @TableId=" + (int)EnumLookCustTableInfo.LeadEditColumnSetting;
                var LeadSelection = hWorker.Repository.db.Database.SqlQuery<LeadSelectionColumnViewModel>(query).ToListSafely();

                if (LeadSelection.CountedPositive())
                {
                    var emselection = LeadSelection.Where(x => x.Employeeid > 0).ToListSafely();
                    var emselectionZero = LeadSelection.Where(x => x.Employeeid == 0).ToListSafely();
                    if (emselection.CountedPositive())
                    {
                        var tt = emselection.Select(x => x.ASCaption).ToArray();
                        string hh = string.Join(",", tt);
                        query = @"EXEC	[dbo].[GetLeadByResponsiblePersonv3]
		                        @ProductSaleProfileId = " + ProductSaleProfileId + @",
		                        @EmployeeId = " + 21 + @",
                                @leadid="+ model.LID  + @",
                                @SelectQuery='" + hh + @"'";
                    }
                    else if (emselectionZero.CountedPositive())
                    {
                        var tt = emselectionZero.Select(x => x.ASCaption).ToArray();
                        string hh = string.Join(",", tt);
                        query = @"EXEC	[dbo].[GetLeadByResponsiblePersonv3]
		                        @ProductSaleProfileId = " + ProductSaleProfileId + @",
		                        @EmployeeId = " + 21 + @",
                                 @leadid=" + model.LID + @",
                                @SelectQuery='" + hh + @"'";
                    }
                    var attendance = hWorker.Repository.db.Database.SqlQuery<string>(query).ToListSafely();
                    //var hhh= attendance[0] as System.Dynamic.ExpandoObject;

                    if (attendance.CountedPositive())
                    {
                        string temp = string.Join("", attendance);
                        xmlDoc.LoadXml(temp);
                    }

                    var nodes = xmlDoc.DocumentElement?.SelectNodes(" / Leads/Lead");
                    List<ListLeadViewModelXML> abc = new List<ListLeadViewModelXML>();
                    foreach (var item in nodes)
                    {
                        var ok = new ListLeadViewModelXML();
                        List<LeadViewModelXML> res = new List<LeadViewModelXML>();
                        var tt = item as XmlElement;
                        TraverseXMLNode(tt, 0, ref res);
                        ok.Nodes = res;
                        abc.Add(ok);
                        res = null;
                    }
                    // var json = new JavaScriptSerializer().Serialize(abc);              
                    if (attendance.CountedPositive())
                    {
                        result.Data = abc;
                    }
                    else
                    {
                        result.Data = null;
                    }
                    result.ResultType = ResultType.Success;
                }
                else
                {
                    result.Data = null;
                    result.ResultType = ResultType.Failure;
                }
            }
            catch (Exception e)
            {
                SharedUtilities.AddLog(className, e);
                result.Data = null;
                result.ResultType = ResultType.Exception;
                result.Message = e.GetOriginalException().Message;
                // result.Exception = e;
            }
            return result;
        }


        public Result<List<ListLeadListCoulmnSettingViewModel>> GetLeadsColumnSettings(int aid)
        {
            var result = new Result<List<ListLeadListCoulmnSettingViewModel>>();
            try
            {               
                HRMSWorker hWorker = new HRMSWorker();            

                long roleid = Convert.ToInt64(System.Web.HttpContext.Current.Session["RoleId"]);
                int ProductSaleProfileId = Convert.ToInt32(System.Web.HttpContext.Current.Session["ProductSaleProfileId"]);
                long SelectedOrganizationId = Convert.ToInt64(System.Web.HttpContext.Current.Session["SelectedOrganizationId"]);
                long EmployeeId = Convert.ToInt64(System.Web.HttpContext.Current.Session["EmployeeId"]);
                //EmployeeId = 0;
                string query = @"EXEC	[dbo].[Lead_GetLeadListColumnSettingV2]
		                                 @Employeeid =" + EmployeeId + @",
	                                    @ProductSaleProfileId  =" + ProductSaleProfileId + @",
	                                   @TableId  =" + aid;

                var attendance = hWorker.Repository.db.Database.SqlQuery<ListLeadListCoulmnSettingViewModel>(query).ToListSafely();
                                     
                if (attendance.CountedPositive())
                {
                    result.Data = attendance;
                }
                else
                {
                    result.Data = null;
                }
                result.ResultType = ResultType.Success;
            }
            catch (Exception e)
            {
                SharedUtilities.AddLog(className, e);
                result.Data = null;
                result.ResultType = ResultType.Exception;
                result.Message = e.GetOriginalException().Message;
                // result.Exception = e;
            }
            return result;
        }





        public Result<List<ListLeadListCoulmnSettingViewModel>> GetLeadsColumnSettings(ListLeadListCoulmnSettingPostViewModel model, EnumLookCustTableInfo _EnumLookCustTableInfo)
        {
            var result = new Result<List<ListLeadListCoulmnSettingViewModel>>();
            try
            {
                HRMSWorker hWorker = new HRMSWorker();
                string columnids = "";
                if (model.ColumnId.CountedPositive())
                    columnids = string.Join(",", model.ColumnId);

                long roleid = Convert.ToInt64(System.Web.HttpContext.Current.Session["RoleId"]);
                int ProductSaleProfileId = Convert.ToInt32(System.Web.HttpContext.Current.Session["ProductSaleProfileId"]);
                long SelectedOrganizationId = Convert.ToInt64(System.Web.HttpContext.Current.Session["SelectedOrganizationId"]);
                long EmployeeId = Convert.ToInt64(System.Web.HttpContext.Current.Session["EmployeeId"]);
                //EmployeeId = 0;
                string query = @"EXEC	[dbo].[Lead_GetLeadListColumnSettingV2]
		                                 @Employeeid =" + EmployeeId + @",
	                                    @ProductSaleProfileId  =" + ProductSaleProfileId + @",
	                                   @TableId  =" + (int)_EnumLookCustTableInfo;
                var attendance = hWorker.Repository.db.Database.SqlQuery<ListLeadListCoulmnSettingViewModel>(query).ToListSafely();

                if (model.IsCurrentUser == true)
                {
                    var ok = attendance.Where(x => x.EmpCustTableInfoLID > 0).ToListSafely();
                    if (ok.CountedPositive())
                    {
                        query = @"EXEC	[dbo].[Lead_UpdateNewLeadListColumnSettingForUser]
		                                 @EmployeeId =" + EmployeeId + @",
	                                     @ProductSaleProfileId  =" + ProductSaleProfileId + @",
                                         @TableId  =" + (int)_EnumLookCustTableInfo + @",
		                                 @ColumnId ='" + columnids + "'";
                        hWorker.ExecuteSql(query);

                    }
                    else
                    {
                        query = @"EXEC	[dbo].[Lead_AddNewLeadListColumnSettingForUser]
		                                 @EmployeeId =" + EmployeeId + @",
	                                     @ProductSaleProfileId  =" + ProductSaleProfileId+ @",
                                         @TableId  =" + (int)_EnumLookCustTableInfo + @",
		                                 @ColumnId ='" + columnids + "'";
                        hWorker.ExecuteSql(query);
                      //  query= "update [CUST].["+ ProductSaleProfileId + "_CustTableInfo]  set IsVisible='v' where Employeeid="+ EmployeeId + " and ProductSaleProfileId="+ ProductSaleProfileId + "  and TableId=

                    }
                }
                else
                {
                    query = @"EXEC	[dbo].[Lead_UpdateNewLeadListColumnSettingForUser]
		                                 @EmployeeId =0,
	                                     @ProductSaleProfileId  =" + ProductSaleProfileId + @",
                                         @TableId  =" + (int)_EnumLookCustTableInfo + @",
		                                 @ColumnId ='" + columnids + "'";
                    hWorker.ExecuteSql(query);
                }

                if (attendance.CountedPositive())
                {
                    result.Data = attendance;
                }
                else
                {
                    result.Data = null;
                }
                result.ResultType = ResultType.Success;
            }
            catch (Exception e)
            {
                SharedUtilities.AddLog(className, e);
                result.Data = null;
                result.ResultType = ResultType.Exception;
                result.Message = e.GetOriginalException().Message;
                // result.Exception = e;
            }
            return result;
        }


        private   static void TraverseXMLNode(XmlNode xmlNode, int depth, ref List<LeadViewModelXML> res)
        {
            //Loop through results
          
            depth = depth + 1;
            string indent = new String(' ', depth);
            if (xmlNode.NodeType == XmlNodeType.Element )
            {
                LeadViewModelXML node = new LeadViewModelXML();
                 XmlElement element = (XmlElement)xmlNode;
                // res.Add(indent + " elementName=" + element.Name + " Value=" + getElementTextValue(element));
                node.Key = element.Name;
                node.ParentKey = element.ParentNode.Name;
                node.Depth = depth;
                if (xmlNode.ChildNodes.Count > 1)
                    node.HasChild = true;
                else
                    node.HasChild = false;
                node.KeyValue = getElementTextValue(element);
                res.Add(node);
            }

            foreach (XmlNode childNode in xmlNode.ChildNodes)
            {
                TraverseXMLNode(childNode, depth,ref res);
            }

           

        }

        private static string getElementTextValue(XmlElement element)
        {
            XmlNode firstChildNode = element.FirstChild;
            if (firstChildNode != null)
            {
                return firstChildNode.Value;
            }
            else
            {
                return "";
            }
        }
    }
}
