using Data.HRMS;
using Library.Core.Services;
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
        static string className = "Locations";
        // SharedUtilities.AddLog(className, ex);
       
        public Result<List<ListLeadViewModelXML>> GetEmployeeAttendanceNew()
        {
           var result = new Result<List<ListLeadViewModelXML>>();
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                HRMSWorker hWorker = new HRMSWorker();
                dynamic MyDynamic = new System.Dynamic.ExpandoObject();
                //MyDynamic.A = "A";
                //MyDynamic.B = "B";
                //MyDynamic.C = "C";
                //MyDynamic.Number = 12;
                //MyDynamic.MyMethod = new Func<int>(() =>
                //{
                //    return 55;
                //});

                long roleid = Convert.ToInt64(System.Web.HttpContext.Current.Session["RoleId"]);
                int ProductSaleProfileId = Convert.ToInt32(System.Web.HttpContext.Current.Session["ProductSaleProfileId"]);
                long SelectedOrganizationId = Convert.ToInt64(System.Web.HttpContext.Current.Session["SelectedOrganizationId"]);
                string query = @"EXEC	[dbo].[GetLeadByResponsiblePersonv3]
		                        @ProductSaleProfileId = 4,
		                        @EmployeeId = 21";

                var attendance = hWorker.Repository.db.Database.SqlQuery<string>(query).ToListSafely();
                //var hhh= attendance[0] as System.Dynamic.ExpandoObject;

                if (attendance.CountedPositive())
                {                  
                    string temp = string.Join("", attendance);
                    xmlDoc.LoadXml(temp);
                }

                  

                var nodes = xmlDoc.DocumentElement?.SelectNodes("/Leads/Lead");
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
                var json = new JavaScriptSerializer().Serialize(abc);

                //foreach (XElement element in xmlDoc)
                //{
                //    var jj=element.Name;
                //}

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
            catch (Exception e)
            {
                result.Data = null;
                result.ResultType = ResultType.Exception;
                result.Message = e.GetOriginalException().Message;
               // result.Exception = e;
            }
            return result;
        }


        static void TraverseXMLNode(XmlNode xmlNode, int depth, ref List<LeadViewModelXML> res)
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

        static string getElementTextValue(XmlElement element)
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
