using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.HRMS;
using Services.HRMS;
using Library.Core.Services;
using Library.Extensions;
using System.Data;

using System.Web.Hosting;
using System.Text;
using VM.HRMS;
using Services.Look;
using VM.Look;
using Library.Lookups;
using System.Globalization;

namespace HRM_CRM.Controllers
{
    public class AttendanceController : Controller
    {
        // GET: Attendance
        AttendanceServices attendanceService = new AttendanceServices();
        EmployeeServices employeeService = new EmployeeServices();
        public ActionResult List(long employee)
        {
            Result<List<EmployeeViewModel>> employees = null;
            long roleid = Convert.ToInt64(System.Web.HttpContext.Current.Session["RoleId"]);
            if (roleid == 0 || roleid == 1)
            {
                employees = employeeService.GetEmployeeList();
                ///  if(departmentList.ResultType==ResultType.Success )
                //return View(employeeList);
            }
            else if (roleid == 2 || roleid == 3)
            {
                string LookOrganizationIds = Convert.ToString(System.Web.HttpContext.Current.Session["LookOrganizationIds"]);
                if (LookOrganizationIds.IsNullOrEmpty())
                    LookOrganizationIds = Convert.ToString(System.Web.HttpContext.Current.Session["LookOrganizationId"]);
                employees = employeeService.GetEmployeeList(LookOrganizationIds);
                ///  if(departmentList.ResultType==ResultType.Success )
                //return View(employeeList);
            }
            else
            {
                employees = employeeService.GetEmployeeList(Convert.ToInt64(System.Web.HttpContext.Current.Session["EmployeeId"]));
                ///  if(departmentList.ResultType==ResultType.Success )
                //return View(employeeList);
            }
            FilteredAttendanceViewModel attendance = new FilteredAttendanceViewModel();
            //  var employees = employeeService.EmployeeList();
            // var employeesNew = employeeService.EmployeeListId(Convert.ToInt64(System.Web.HttpContext.Current.Session["EmployeeId"]));
            if (employees.Data.CountedPositive())
            {
                var biometircid = employees.Data.Where(x => x.EmployeeId == employee).FirstOrDefault();
                int bioMetricid = 0;
                if (biometircid.IsNotNull() && biometircid.BioMetricId > 0)
                    bioMetricid = biometircid.BioMetricId ?? 0;
                
                if (employee == 0)
                {
                    var temp = employees.Data;
                    employee = temp.FirstOrDefault().EmployeeId;
                }
                attendance.EmployeeId = employee;

                if (employees.ResultType.Equals(ResultType.Exception))
                    return RedirectToAction("No505", "Error");
                ViewBag.EmployeesWithDepartment = employees.Data;
                SelectList ddlEmployees = new SelectList(employees.Data, "EmployeeId", "FullName");
                ViewBag.Employees = ddlEmployees;
                AttendanceServices attendanceService = new AttendanceServices();
                if (bioMetricid > 0)
                {
                    var employeeAttendance = attendanceService.GetEmployeeAttendanceNew(bioMetricid, null, null);
                    if (employeeAttendance.ResultType.Equals(ResultType.Exception))
                    {
                        return RedirectToAction("No505", "Error");
                    }
                    attendance.Attendance = employeeAttendance.Data;
                }
                else
                    attendance.Attendance = null;
            }else
            {
                attendance.Attendance = null;
            }
            return View(attendance);
        }
        [HttpPost]
        public ActionResult List(FilteredAttendanceViewModel attendance)
        {
            Result<List<EmployeeViewModel>> employees = null;
            long roleid = Convert.ToInt64(System.Web.HttpContext.Current.Session["RoleId"]);
            if (roleid == 0 || roleid == 1)
            {
                employees = employeeService.GetEmployeeList();
                ///  if(departmentList.ResultType==ResultType.Success )
                //return View(employeeList);
            }
            else if (roleid == 2 || roleid == 3)
            {
                string LookOrganizationIds = Convert.ToString(System.Web.HttpContext.Current.Session["LookOrganizationIds"]);
                if (LookOrganizationIds.IsNullOrEmpty())
                    LookOrganizationIds = Convert.ToString(System.Web.HttpContext.Current.Session["LookOrganizationId"]);
                employees = employeeService.GetEmployeeList(LookOrganizationIds);
                ///  if(departmentList.ResultType==ResultType.Success )
                //return View(employeeList);
            }
            else
            {
                employees = employeeService.GetEmployeeList(Convert.ToInt64(System.Web.HttpContext.Current.Session["EmployeeId"]));
                ///  if(departmentList.ResultType==ResultType.Success )
                //return View(employeeList);
            }
            //var employees = employeeService.EmployeeListId(Convert.ToInt64( System.Web.HttpContext.Current.Session["EmployeeId"]));
            var biometircid = employees.Data.Where(x => x.EmployeeId == attendance.EmployeeId).FirstOrDefault();
            ViewBag.EmployeesWithDepartment = employees.Data;
            HRPolicyService hRPolicyService = new HRPolicyService();
       //  var ok=   hRPolicyService.GetHRPolicyByDesignationAndGroup(1, biometircid.LookDesignationId??0);
         //   ViewBag.Policy = ok;
            int bioMetricid = 0;
            if (biometircid.IsNotNull() && biometircid.BioMetricId>0)
                bioMetricid = biometircid.BioMetricId??0;
            if (employees.ResultType.Equals(ResultType.Exception))
                return RedirectToAction("No505", "Error");
            SelectList ddlEmployees = new SelectList(employees.Data, "EmployeeId", "FullName");
            ViewBag.Employees = ddlEmployees;
            AttendanceServices attendanceService = new AttendanceServices();
            var employeeAttendance = attendanceService.GetEmployeeAttendanceNew(bioMetricid, attendance.From, attendance.To);
            if (employeeAttendance.ResultType.Equals(ResultType.Exception))
            {
                return RedirectToAction("No505", "Error");
            }
            attendance.Attendance = employeeAttendance.Data;
            return View(attendance);
        }
        public ActionResult DisplayMessage()
        {
            return View();
        }
        public ActionResult Attendance(long? attendanceId)
        {
            LookDepartmentService departmentService = new LookDepartmentService();
            var departments = departmentService.DepartmentList();
            if (departments.ResultType.Equals(ResultType.Exception))
                return RedirectToAction("No505", "Error");
            ViewBag.Departments = new SelectList(departments.Data, "LookDepartmentId", "DepartmentName");
            if (attendanceId.IsNotNull() && !attendanceId.Equals(0))
            {
                var attendance = attendanceService.GetAttendance(attendanceId ?? 0);
                if (attendance.ResultType.Equals(ResultType.Exception))
                {
                    return RedirectToAction("No505", "Error");
                }
                var name = employeeService.GetEmployeeName(attendance.Data.EmployeeAttendanceId);
                ViewBag.EmployeeName = name;
                return View(attendance.Data);
            }
            return View();
        }
        [HttpPost]
        public ActionResult Attendance(EmployeeAttendance attendance)
        {
            if (attendance.ActualTimeIn.IsNull() && attendance.ActualTimOut.IsNull())
            {
                HRPolicyService hRPolicyService = new HRPolicyService();
                List<LookPolicyViewModel> employeePolicy = hRPolicyService.GetEmployeePolicyPolicyByDesignationAndEmployeeId(/*designation*/1, (long)EnumPolicyType.ShiftTiming, attendance.EmployeeId, true /*false*/).Data;
                if (employeePolicy.CountedPositive())
                {
                    List<LookPolicyViewModel> employeeShiftTimePolicy = employeePolicy.Where(x => x.LookPolicyGroupId == (long)EnumPolicyType.ShiftTiming).ToListSafely();

                    var startTime = employeeShiftTimePolicy[0].EmployeePolicyValue;
                    var endTime = employeeShiftTimePolicy[1].EmployeePolicyValue;
                    if (startTime.IsNotNullOrEmpty() && endTime.IsNotNullOrEmpty())
                    {
                        attendance.ActualTimeIn = startTime.ToDateTimeByNoRules();
                        attendance.ActualTimOut = endTime.ToDateTimeByNoRules();
                    }
                    else
                    {
                        var employeeDesignation = employeeService.GetEmployeeDesignation(attendance.EmployeeId);
                        var designationPolicy = hRPolicyService.GetEmployeePolicyPolicyByDesignationAndEmployeeId(employeeDesignation, (long)EnumPolicyType.ShiftTiming, 0, false).Data;
                        startTime = designationPolicy[0].PolicyValue;
                        endTime = designationPolicy[1].PolicyValue;
                        attendance.ActualTimeIn = startTime.ToDateTimeByNoRules();
                        attendance.ActualTimOut = endTime.ToDateTimeByNoRules();
                    }

                }
                else
                {
                    var employeeDesignation = employeeService.GetEmployeeDesignation(attendance.EmployeeId);
                    var designationPolicy = hRPolicyService.GetEmployeePolicyPolicyByDesignationAndEmployeeId(employeeDesignation, (long)EnumPolicyType.ShiftTiming, 0, false).Data;
                    if (designationPolicy.CountedPositive())
                    {
                        var startTime = designationPolicy[0].PolicyValue;
                        var endTime = designationPolicy[1].PolicyValue;
                        attendance.ActualTimeIn = startTime.ToDateTimeByNoRules();
                        attendance.ActualTimOut = endTime.ToDateTimeByNoRules();
                    }
                }
            }
            if (attendance.EmployeeAttendanceId > 0)
            {
                attendanceService.UpdateAttendance(attendance);
            }
            else
            {
                attendanceService.MarkAttendance(attendance);
            }
            return RedirectToAction("List", new { employee = attendance.EmployeeId });
        }
        [HttpPost]
        public ActionResult AttendanceByBId(InputAttendanceViewModel attendance)
        {
            AttendanceServices attendanceServices = new AttendanceServices();
            ViewBag.LogdataTime = attendance.LogDate.ToString("yyyy-MM-dd");
            ViewBag.BioMetricId = attendance.BioMetricId;
           var result = attendanceServices.GetEmployeeAttendanceById(attendance);
            return View( result);
        }
        [HttpPost]        
       [CustomAuthorize(Permission = "AddTimeinTimeOut")]
        public ActionResult UpdateAttendanceByBId(InputAttendanceViewModel attendance)
        {
            AttendanceServices attendanceServices = new AttendanceServices();
            ViewBag.LogdataTime = attendance.LogDate.ToString("yyyy-MM-dd");
            DateTime dateTime = DateTime.ParseExact(attendance.LogTime, "HH:mm",
                                        CultureInfo.InvariantCulture);
            DateTime combined = attendance.LogDate.Date.Add(dateTime.TimeOfDay);

            string message = "";

            HRMSWorker hrWorker = new HRMSWorker();
            var existing = hrWorker.Repository.Read<Employee>()
                      .Where(x => x.BioMetricId == attendance.BioMetricId && x.ExitDate==null).FirstOrDefault();

            string sql = @"INSERT INTO [dbo].[BioMetricData]
           ([BioMetricId]
           ,[DateTimeRecord]
           ,[DateOnly]
           ,[TimeOnly]
           ,[dwInOutMode]
           ,[IsEdit]
           ,[UpdatedBy]
           ,[UpdateDate])
     VALUES
           ("+ attendance.BioMetricId + @"
           ,'"+ combined + @"'
           ,'" + attendance.LogDate + @"'
           ,'" + combined + @"'
           ,"+ attendance.InOutStatusId + @"
           ,1
           ,"+ Convert.ToInt64(System.Web.HttpContext.Current.Session["EmployeeId"]) + @"
           ,getdate()
            ) ";
            try
            {
                if (existing.IsNotNull())
                {
                    if (Convert.ToInt32(System.Web.HttpContext.Current.Session["BioMetricId"]) == attendance.BioMetricId)
                    {
                        if ((System.Web.HttpContext.Current.Session["ManagerId"].IsNotNull() && Convert.ToInt64(System.Web.HttpContext.Current.Session["ManagerId"]) == 3))
                            hrWorker.ExecuteSql(sql);
                        else
                            message = "Only reporting person allow this";
                    }
                    else
                    {
                        if (existing.ManagerId == Convert.ToInt64(System.Web.HttpContext.Current.Session["EmployeeId"]))
                            hrWorker.ExecuteSql(sql);
                        else
                            message = "Only reporting person allow this";

                    }
                }
            }catch(Exception ex)
            {
                message = "Exception";
            }
            var result = attendanceServices.GetEmployeeAttendanceById(attendance);
            result.Message = message;
            return View(result);
        }

        //public ActionResult UploadExcel(Data.Agency.Agency users, HttpPostedFileBase FileUpload)
        //{
        //    AgencyWorker agencyWorker = new AgencyWorker();
        //    ServiceManager service = new ServiceManager();
        //    AgencyServices AgencyBLL = new AgencyServices();
        //    List<string> data = new List<string>();
        //    if (FileUpload != null)
        //    {
        //        if (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" || FileUpload.ContentType == "text/plain")
        //        {
        //            string filename = FileUpload.FileName;
        //            string targetpath = Server.MapPath("~/Uploads/");
        //            FileUpload.SaveAs(targetpath + filename);
        //            string pathToExcelFile = targetpath + filename;
        //            //var connectionString = "";
        //            //if (filename.EndsWith(".xls"))
        //            //{
        //            //    connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", pathToExcelFile);
        //            //}
        //            //else if (filename.EndsWith(".xlsx"))
        //            //{
        //            //    connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", pathToExcelFile);
        //            //}
        //            //var adapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", connectionString);
        //            //var dtable = new DataTable();
        //            //adapter.Fill(dtable);
        //            var dtable = new DataTable();
        //            dtable = ConvertCSVToDataTable(pathToExcelFile, '\t');
        //            if (dtable.IsNullOrEmpty())
        //            {
        //                var faultyDataTable = dtable.Clone();

        //                foreach (DataRow a in dtable.Rows)
        //                {
        //                    try
        //                    {
        //                        Data.Agency.Agency agency = new Data.Agency.Agency();
        //                        if (a["agencyname"].ToString() == "")
        //                        {
        //                            faultyDataTable.Rows.Add(a.ItemArray);
        //                            continue;
        //                        }
        //                        agency.AgencyName = a["agencyname"].ToString();
        //                        var agencyName = a["agencyname"].ToString();
        //                        if (agencyName.IsNotNullOrEmpty())
        //                        {
        //                            Result<bool> duplicate = AgencyBLL.GetAgencySearch(agencyName);
        //                            if (duplicate.ResultType == ResultType.Success)
        //                            {
        //                                if (duplicate.Data == true)
        //                                {
        //                                    faultyDataTable.Rows.Add(a.ItemArray);
        //                                    continue;
        //                                }

        //                            }

        //                        }
        //                        agency.Address1 = a["address"].ToString();
        //                        agency.CountryId = 1;
        //                        if (a["province"].ToString() == "")
        //                        {
        //                            faultyDataTable.Rows.Add(a.ItemArray);
        //                            continue;
        //                        }
        //                        agency.ProvinceId = Convert.ToInt32(a["province"]);
        //                        if (a["city"].ToString() == "")
        //                        {
        //                            faultyDataTable.Rows.Add(a.ItemArray);
        //                            continue;
        //                        }
        //                        agency.CityId = Convert.ToInt32(a["city"]);
        //                        if (a["zone"].ToString() == "")
        //                        {
        //                            faultyDataTable.Rows.Add(a.ItemArray);
        //                            continue;
        //                        }
        //                        agency.LookZoneId = Convert.ToInt32(a["zone"]);
        //                        var city = Convert.ToInt32(a["city"]);
        //                        var ProvinceId = Convert.ToInt32(a["province"]);
        //                        var Society = a["society"].ToString();
        //                        var Area = a["area"].ToString();
        //                        if (Society.IsNotNullOrEmpty())
        //                        {
        //                            Result<int> SocietyId = service.GetInsertNewSocietyId(city, ProvinceId, 1, Society);
        //                            if (SocietyId.ResultType == ResultType.Success)
        //                            {
        //                                agency.LookSocietyId = SocietyId.Data;
        //                            }
        //                        }
        //                        if (Area.IsNotNullOrEmpty())
        //                        {
        //                            Result<int> AreaId = service.GetInsertNewAreaId(city, Area);
        //                            if (AreaId.ResultType == ResultType.Success)
        //                            {
        //                                agency.LookAreaId = AreaId.Data;
        //                            }
        //                        }
        //                        agency.AgencyMobile = a["mobile"].ToString();
        //                        agency.AgencyPhone = a["phone"].ToString();
        //                        agency.AgencyGuid = Guid.NewGuid();
        //                        agency.LeadCreationDate = DateTime.Now;
        //                        agency.CreateDate = DateTime.Now;
        //                        agency.AgencyStatus = (Int16)EnumLookAgencyStatus.Lead;

        //                        var propquery = " EXEC[dbo].[sp_Agency] '', '',null," + agency.CountryId + "," + agency.ProvinceId + "," + agency.CityId + ",'06/23/2017'";
        //                        string IlaanId = agencyWorker.Repository.db.Database.SqlQuery<string>(propquery).FirstOrDefault();

        //                        agency.IlaanAgencyID = IlaanId;

        //                        agencyWorker.Repository.Create(agency);
        //                        agencyWorker.SaveChanges();

        //                    }
        //                    catch (DbEntityValidationException ex)
        //                    {
        //                        faultyDataTable.Rows.Add(a.ItemArray);

        //                        foreach (var entityValidationErrors in ex.EntityValidationErrors)
        //                        {
        //                            foreach (var validationError in entityValidationErrors.ValidationErrors)
        //                            {

        //                                Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);

        //                            }

        //                        }
        //                    }

        //                }

        //                if (faultyDataTable.IsNullOrEmpty())
        //                {
        //                    //ExportToExcel(faultyDataTable,"");
        //                    // ConvertCSVToDataTable("", '\t');
        //                    var faulty = faultyDataTable.ConvertToCsv('\t');
        //                    WriteToCsvFile(faultyDataTable, "");
        //                }

        //                ////deleting excel file from folder  
        //                if ((System.IO.File.Exists(pathToExcelFile)))
        //                {
        //                    System.IO.File.Delete(pathToExcelFile);
        //                }

        //                DisplayMessage();
        //            }
        //            else
        //            {
        //                //alert message for invalid file format  
        //                data.Add("<ul>");
        //                data.Add("<li>Only Excel file format is allowed</li>");
        //                data.Add("</ul>");
        //                data.ToArray();
        //                return Json(data, JsonRequestBehavior.AllowGet);
        //            }
        //        }
        //        else
        //        {
        //            data.Add("<ul>");
        //            if (FileUpload == null) data.Add("<li>Please choose Excel file</li>");
        //            data.Add("</ul>");
        //            data.ToArray();
        //            return Json(data, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    //return Json(data, JsonRequestBehavior.AllowGet);
        //    return DisplayMessage();

        //}

        public static void ExportToExcel(DataTable faultyDataTable, string excelFilePath = null)
        {
            try
            {
                excelFilePath = HostingEnvironment.MapPath("\\Uploads\\faultyFile");
                if (faultyDataTable == null || faultyDataTable.Columns.Count == 0)
                    throw new Exception("ExportToExcel: Null or empty input table!\n");

                // load excel, and create a new workbook
                var excelApp = new Microsoft.Office.Interop.Excel.Application();
                excelApp.Workbooks.Add();

                // single worksheet
                Microsoft.Office.Interop.Excel._Worksheet workSheet = excelApp.ActiveSheet;

                // column headings
                for (var i = 0; i < faultyDataTable.Columns.Count; i++)
                {
                    workSheet.Cells[1, i + 1] = faultyDataTable.Columns[i].ColumnName;
                }

                // rows
                for (var i = 0; i < faultyDataTable.Rows.Count; i++)
                {
                    // to do: format datetime values before printing
                    for (var j = 0; j < faultyDataTable.Columns.Count; j++)
                    {
                        workSheet.Cells[i + 2, j + 1] = faultyDataTable.Rows[i][j];
                    }
                }

                if ((System.IO.File.Exists(excelFilePath)))
                {
                    System.IO.File.Delete(excelFilePath);
                }

                // check file path
                if (!string.IsNullOrEmpty(excelFilePath))
                {
                    try
                    {
                        workSheet.SaveAs(excelFilePath);
                        excelApp.Quit();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("ExportToExcel: Excel file could not be saved! Check filepath.\n"
                                            + ex.Message);
                    }
                }
                else
                { // no file path is given
                    excelApp.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ExportToExcel: \n" + ex.Message);
            }
        }

        public static void WriteToCsvFile(DataTable faultyDataTable, string filePath)
        {
            StringBuilder fileContent = new StringBuilder();
            filePath = HostingEnvironment.MapPath("\\Uploads\\faultyFile.txt");
            foreach (var col in faultyDataTable.Columns)
            {
                fileContent.Append(col.ToString() + ",");
            }

            fileContent.Replace(",", System.Environment.NewLine, fileContent.Length - 1, 1);



            foreach (DataRow dr in faultyDataTable.Rows)
            {

                foreach (var column in dr.ItemArray)
                {
                    fileContent.Append("\"" + column.ToString() + "\",");
                }



                fileContent.Replace(",", System.Environment.NewLine, fileContent.Length - 1, 1);
            }

            ////deleting excel file from folder  
            if ((System.IO.File.Exists(filePath)))
            {
                System.IO.File.Delete(filePath);
            }

            System.IO.File.WriteAllText(filePath, fileContent.ToString());

        }

        private static DataTable ConvertCSVToDataTable(string path, char delimiter)
        {
            if (path == "")
            {
                path = HostingEnvironment.MapPath("\\Uploads\\faultyFile");
            }
            string CSVFilePathName = path;
            string[] Lines = System.IO.File.ReadAllLines(CSVFilePathName);
            string[] Fields;
            Fields = Lines[0].Split(new char[] { delimiter });
            int Cols = Fields.GetLength(0);
            DataTable dt = new DataTable();
            //1st row must be column names; force lower case to ensure matching later on.
            for (int i = 0; i < Cols; i++)
                dt.Columns.Add(Fields[i].ToLower(), typeof(string));
            DataRow Row;
            for (int i = 1; i < Lines.GetLength(0); i++)
            {
                if (Lines[i].IsNotNullOrEmpty())
                {
                    Fields = Lines[i].Split(new char[] { delimiter });
                    Row = dt.NewRow();
                    for (int f = 0; f < Cols; f++)
                        Row[f] = Fields[f];
                    dt.Rows.Add(Row);
                }
            }
            return dt;
        }
    }
}