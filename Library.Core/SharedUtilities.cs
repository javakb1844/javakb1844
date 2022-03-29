using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;


namespace Library.Core
{
    public enum EnumReaIsoDateFormat
    {
        [Description("")]
        None,
        [Description(@"^\d{4}-\d{2}-\d{2}$")]
        One,
        [Description(@"^\d{4}-\d{2}-\d{2}-\d{2}:\d{2}$")]
        Two,
        [Description(@"^\d{4}-\d{2}-\d{2}-\d{2}:\d{2}:\d{2}$")]
        Three,
        [Description(@"^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}$")]
        Four,
        [Description(@"^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}$")]
        Five,
        [Description(@"^\d{8}$")]
        Six,
        [Description(@"^\d{8}-\d{4}$")]
        Seven,
        [Description(@"^\d{8}-\d{6}$")]
        Eight,
        [Description(@"^\d{8}T\d{4}$")]
        Nine,
        [Description(@"^\d{8}T\d{6}$")]
        Ten
    }

    public static class SharedUtilities
    {
        public static string SerializeToXml<T>(T complex) where T : class
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                using (MemoryStream ms = new MemoryStream())
                {
                    XmlWriterSettings settings = new XmlWriterSettings();
                    settings.Indent = false;
                    settings.OmitXmlDeclaration = true;
                    settings.Encoding = UTF32Encoding.Default;
                    XmlWriter writer = XmlWriter.Create(ms, settings);
                    xmlSerializer.Serialize(writer, complex);
                    return UTF32Encoding.Default.GetString(ms.ToArray());
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public static IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }
        public static T CreateNetObjectsFromXml<T>(string xmlData) where T : class
        {
            try
            {
                //parse XML data to .Net objects
                T complex = default(T);
                XmlSerializer serializer = new XmlSerializer(typeof(T));              
                string x = xmlData; 
                x = RemoveFaultyCharacters(x);
                using (StringReader reader = new StringReader(x))
                {
                    complex = (T)serializer.Deserialize(reader);
                   //serializer.Serialize()
                }

                return complex;
            }
            catch(Exception ex)
            {
                return default(T);
            }
        }

        public static T CreateNetObjectsFromXml<T>(string xmlData, out string error) where T : class
        {
            try
            {
                //parse XML data to .Net objects
                T complex = default(T);
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                string x = xmlData;
                x = RemoveFaultyCharacters(x);
                using (StringReader reader = new StringReader(x))
                {
                    complex = (T)serializer.Deserialize(reader);
                    //serializer.Serialize()
                }
                error = "";
                return complex;
            }
            catch (Exception ex)
            {
                error = ex.GetOriginalException().Message;
                return default(T);
            }
        }

        //public static T CreateNetObjectsFromJson<T>(string xmlData, out string error) where T : class
        //{
        //    try
        //    {
        //        //parse XML data to .Net objects
        //        T complex = default(T);
        //        JsonSerializer serializer = new JsonSerializer();
        //        string x = xmlData;
        //        x = RemoveFaultyCharacters(x);
        //        // complex = JsonConvert.DeserializeObject<T>(x);
        //        using (StreamReader sr = new StreamReader(x))
        //        {
        //            using (JsonReader reader = new JsonTextReader(sr))
        //            {
        //               // JsonSerializer serializer = new JsonSerializer();
        //                // read the json from a stream
        //                // json size doesn't matter because only a small piece is read at a time from the HTTP request
        //                complex = (T)serializer.Deserialize(reader);
        //            }
        //        }

        //        error = "";
        //        return complex;
        //    }
        //    catch (Exception ex)
        //    {
        //        error = ex.GetOriginalException().Message;
        //        return default(T);
        //    }
        //}
       
        public static T JsonDeserialize<T>(string json, out string error)
        {
            try
            {
                var settings = new DataContractJsonSerializerSettings
                {
                    DateTimeFormat = new System.Runtime.Serialization.DateTimeFormat("o")
                };
                var _Bytes = Encoding.Unicode.GetBytes(json);
                using (MemoryStream _Stream = new MemoryStream(_Bytes))
                {

                    var _Serializer = new DataContractJsonSerializer(typeof(T), settings);
                    error = "";
                    return (T)_Serializer.ReadObject(_Stream);
                }
            }
            catch (Exception ex)
            {
                error = ex.GetOriginalException().Message;
                return default(T);
            }
        }
        private static string RemoveFaultyCharacters(string x)
        {
            string[] faultyCharacters = new string[]{
                "&#x4;",
                "&#x6;",
                "&#x1A;",
                "&#x5;",
                "&#x14;",
                "&#x18;",
                "&#x1B;",
                "&#x16;",
                "&#x14;",
                "&#x11;",
                "&#x19;",
                "&#x4;",
                "&#xC;",
                "&#x1D;",
                "&#xF;",
                "&#x17;", 
                "&#x15;"
                };
            foreach (var item in faultyCharacters)
            {
                x = x.Replace(item, "");
            }
            x = x.Replace("&#x1F;", " ");
            x = x.Replace("&#x1E;", "");
            x = x.Replace("&#x10;", "");
            x = x.Replace("&#x11;", "");
            x = x.Replace("&#x12;", "");
            x = x.Replace("&#x5;", "");
            x = x.Replace("&#x4;", "");
            x = x.Replace("&#x6;", "");
            x = x.Replace("&#x1D;", "");
            x = x.Replace("&#x1C;", "");
            x = x.Replace("&#x14;", "");
            x = x.Replace("&#x1A;", "");
            x = x.Replace("&#x1B;", "");
            return x;
        }
        /// <summary>
        /// Search for file until root directory is reached
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static string FindFile(string filename)
        {
            try
            {
                string currentDirectory = Directory.GetCurrentDirectory();
                string root = Directory.GetDirectoryRoot(currentDirectory);
                string[] files = null;
                bool fileFound = false;
                do
                {
                    files = Directory.GetFiles(currentDirectory, filename, SearchOption.AllDirectories);
                    if (files.Any())
                    {
                        fileFound = true;
                        break;
                    }
                    else
                    {
                        currentDirectory = Directory.GetParent(currentDirectory).FullName;
                    }
                } while (currentDirectory != root);

                if (fileFound)
                    return files[0];

                return string.Empty;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        
        /// <summary>
        //should convert these date formats from ISO8601 appearing in REAXML
        //YYYY-MM-DD
        //YYYY-MM-DD-hh:mm
        //YYYY-MM-DD-hh:mm:ss
        //YYYY-MM-DDThh:mm
        //YYYY-MM-DDThh:mm:ss
        //YYYYMMDD
        //YYYYMMDD-hhmm
        //YYYYMMDD-hhmmss
        //YYYYMMDDThhmm
        //YYYYMMDDThhmmss
        // <param name="reaxmlDateString"></param>
        // <returns></returns>
        /// </summary>
        public static string FromREAXMLDateTime(string reaxmlDateString) 
        {
            if (reaxmlDateString.IsNullOrEmpty())
                return null;
            var dateFormatPatterns = GetEnumValueDescPair<EnumReaIsoDateFormat>();
           
            string convertedDate = null;
            EnumReaIsoDateFormat format = EnumReaIsoDateFormat.None;
            foreach (var pattern in dateFormatPatterns.Values)
            {
                if (pattern.IsNullOrEmpty())
                    continue;
                if (new Regex(pattern).Match(reaxmlDateString).Success)
                {
                    format = dateFormatPatterns.FirstOrDefault(x => string.Compare(x.Value, pattern, true) == 0).Key;
                    break;
                }
            }
            if (format != EnumReaIsoDateFormat.None)
            {
                switch (format)
                {
                    case EnumReaIsoDateFormat.One:
                        convertedDate = reaxmlDateString.IsConvertableToDate() ? reaxmlDateString : string.Empty;
                        break;
                    case EnumReaIsoDateFormat.Two:
                    case EnumReaIsoDateFormat.Three:
                        //i = reaxmlDateString.LastIndexOf('-');
                        //reaxmlDateString = reaxmlDateString.Substring(0, i) + " " + reaxmlDateString.Substring(i, reaxmlDateString.Length - i);
                        Regex rgx = new Regex("-");
                        convertedDate = rgx.Replace(reaxmlDateString, " ", 1, reaxmlDateString.LastIndexOf('-')-1);
                        convertedDate = convertedDate.IsConvertableToDate() ? convertedDate : string.Empty;
                        break;
                    case EnumReaIsoDateFormat.Four:
                    case EnumReaIsoDateFormat.Five:
                        //i = reaxmlDateString.LastIndexOf('T');
                        //reaxmlDateString = reaxmlDateString.Substring(0, i) + " " + reaxmlDateString.Substring(i, reaxmlDateString.Length - i);
                        rgx = new Regex("T");
                        convertedDate = rgx.Replace(reaxmlDateString, " ", 1, reaxmlDateString.LastIndexOf('-')-1);
                        convertedDate = convertedDate.IsConvertableToDate() ? convertedDate : string.Empty;
                        break;
                    case EnumReaIsoDateFormat.Six:
                        reaxmlDateString = string.Format("{0}-{1}-{2}", reaxmlDateString.Substring(0, 4), reaxmlDateString.Substring(4, 2), reaxmlDateString.Substring(6, 2));
                        convertedDate = reaxmlDateString.IsConvertableToDate() ? reaxmlDateString : string.Empty;
                        break;
                    case EnumReaIsoDateFormat.Seven:
                    case EnumReaIsoDateFormat.Nine:
                        reaxmlDateString = string.Format("{0}-{1}-{2} {3}:{4}", reaxmlDateString.Substring(0, 4), reaxmlDateString.Substring(4, 2), reaxmlDateString.Substring(6, 2), reaxmlDateString.Substring(9, 2), reaxmlDateString.Substring(11, 2));
                        convertedDate = reaxmlDateString.IsConvertableToDate() ? reaxmlDateString : string.Empty;
                        break;
                    case EnumReaIsoDateFormat.Eight:
                    case EnumReaIsoDateFormat.Ten:
                        reaxmlDateString = string.Format("{0}-{1}-{2} {3}:{4}:{5}", reaxmlDateString.Substring(0, 4), reaxmlDateString.Substring(4, 2), reaxmlDateString.Substring(6, 2), reaxmlDateString.Substring(9, 2), reaxmlDateString.Substring(11, 2), reaxmlDateString.Substring(13, 2));
                        convertedDate = reaxmlDateString.IsConvertableToDate() ? reaxmlDateString : string.Empty;
                        break;
                }
            }
            return convertedDate;

        }
        
        public static Dictionary<TEnum, string> GetEnumValueDescPair<TEnum>()
            where TEnum : struct // can't constrain to enums so closest thing
        {
            try
            {
                Dictionary<TEnum, string> dict = new Dictionary<TEnum, string>();

                Enum.GetValues(typeof(TEnum)).Cast<Enum>().ToList().ForEach(x => dict.Add((x.CastTo<TEnum>()), x.ToDescription()));
                return dict;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static Dictionary<string, TEnum> GetEnumDescValuePair<TEnum>()
            where TEnum : struct
        {
            try
            {
                Dictionary<string, TEnum> dict = new Dictionary<string, TEnum>();

                Enum.GetValues(typeof(TEnum)).Cast<Enum>().ToList().ForEach(x => dict.Add(x.ToDescription(), (x.CastTo<TEnum>())));
                return dict;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        
        public static T ReadAndValidateFromDTDOld<T>(string dtdFilePath, string xmlInRequest) where T : class
        {
            try
            {
                var settings = new XmlReaderSettings
                {
                    //XmlResolver = null,
                    ValidationType = ValidationType.DTD,
                    DtdProcessing = DtdProcessing.Parse
                };
                //load DTD file from path
                //settings.Schemas.Add(null, dtdFilePath);
                settings.ValidationEventHandler += new ValidationEventHandler((o, args) =>
                {
                    Console.WriteLine(args.Message);
                });
                var reader = XmlReader.Create(new StringReader(xmlInRequest), settings);
                while (reader.Read());
                //validated
                return SharedUtilities.CreateNetObjectsFromXml<T>(xmlInRequest);
            }
            catch (Exception ex)
            {
                //throw;
                return default(T);
            }
        }
        
        public static T ReadAndValidateFromDTD<T>(string xmlInRequest) where T : class
        {
            try
            {
                using (var ms = new MemoryStream(System.Text.Encoding.ASCII.GetBytes(xmlInRequest)))
                {
                    using (var tr = new XmlTextReader(ms))
                    {
                        using (var validator = new XmlValidatingReader(tr))
                        {
                            validator.ValidationType = ValidationType.DTD;
                            validator.ValidationEventHandler += ((o, e) =>
                            {
                                Debug.WriteLine(e.Message);
                            });
                            // Validate the entire xml file
                            while (validator.Read());
                            return SharedUtilities.CreateNetObjectsFromXml<T>(xmlInRequest);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //throw;
                return default(T);
            }
        }
        public static bool ReadAndValidateFromDTD(string xmlInRequest)
        {
            try
            {
                using (var ms = new MemoryStream(System.Text.Encoding.ASCII.GetBytes(xmlInRequest)))
                {
                    using (var tr = new XmlTextReader(ms))
                    {
                        using (var validator = new XmlValidatingReader(tr))
                        {
                            validator.ValidationType = ValidationType.DTD;
                            validator.ValidationEventHandler += ((o, e) =>
                            {
                                Debug.WriteLine(e.Message);
                            });
                            // Validate the entire xml file
                            while (validator.Read()) ;
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //throw;
                return false;
            }
        }
        public static string GetReaBooleanValueAsString(string field)
        {
            try
            {
                if (field.IsNotNullOrEmpty())
                {
                    int cVal = 0;
                    bool isInt = false;
                    double dVal = 0.0;
                    if (Int32.TryParse(field, out cVal))
                    {
                        isInt = true;
                        if (cVal < 0)
                        { return null; }                       
                    }
                    if (double.TryParse(field, out dVal) && isInt == false)
                    {
                        return false.ToString();
                    }

                    switch (field.ToLower())
                    { 
                        case "true":
                        case "yes":
                        case "1":
                        case "5":
                            return true.ToString();
                           
                        case "false":
                        case "no":
                        case "0":
                            return false.ToString();                           
                        default:
                            return null;
                    
                    }
                 
                }
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public static bool FromReaBooleanField(string value)
        {
            try
            {
                int v = 0;
                if (value.CompareWithoutCase("yes") || value.CompareWithoutCase("true") || value == "1")
                    return true;
                if (value.CompareWithoutCase("no") || value.CompareWithoutCase("0") || value.CompareWithoutCase("false"))
                    return false;
                if (int.TryParse(value, out v))
                    return true;
                
                return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static string GetReaNumericValueAsString(string field)
        {
            try
            {
                if (field.IsNotNullOrEmpty())
                {
                    int? v = FromReaNumericField(field);
                    return v.Value.ts();
                }
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }        
        public static int? FromReaNumericField(string value)
        {
            try
            {
                int result = 0;
                if (value.CompareWithoutCase("studio") || value.CompareWithoutCase("no") || value.CompareWithoutCase("false"))
                    return 0;
                else if (value.CompareWithoutCase("yes") || value.CompareWithoutCase("true"))
                    return 1;
                else
                    int.TryParse(value, out result);
                
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static bool FromReaInspectionTimes(string inspection, out string inspectionDate, 
                                                    out string inspectionStartTime, out string inspectionEndTime)
        {
            try
            {
                if (inspection.IsNotNullOrEmpty())
                {
                    string[] dtValues = inspection.Split(' ');
                    inspectionDate = dtValues[0] + " 00:00:00";
                    inspectionStartTime = dtValues[0] + " " + dtValues[1];
                    inspectionEndTime = dtValues[0] + " " + dtValues[3];

                    if (inspectionDate.IsConvertableToDate() && inspectionStartTime.IsConvertableToDate() && inspectionEndTime.IsConvertableToDate())
                        return true;
                    // any one or all of the dates are malformatted
                    return false;
                }
                inspectionDate = "";
                inspectionStartTime = "";
                inspectionEndTime = "";
                return false;
                
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static string GetQueryString(this object obj, bool isPlot = false)
        {
            //if (isPlot)
            //{
            var properties = from p in obj.GetType().GetProperties()
                             where p.GetValue(obj, null) != null && p.Name != "pgno" && p.Name != "pgsize" && p.Name != "t" && p.Name != "tl" && p.Name != "q"
                             select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());

            return String.Join("&", properties.ToArray());
            ////}
            ////else
            ////{
            ////    var properties = from p in obj.GetType().GetProperties()
            ////                     where p.GetValue(obj, null) != null && p.Name != "pgno" && p.Name != "pgsize" && p.Name != "t" 
            ////                     select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());

            ////    return String.Join("&", properties.ToArray());
            ////}
        }
        public static void AddLog(string fileName, string textToWrite)
        {
           
            try
            {
                //logService.Addlog("emailService", "AddLog", "->filename= " + fileName + "->testtowrites=" + textToWrite, 1, "email service");
                 string logsfilepath = System.Configuration.ConfigurationManager.AppSettings["logsfilepath"];

                var _fileName = @"~/" + fileName;
                //  var ok= System.Reflection.MethodBase.GetCurrentMethod().Name;
                //   _fileName = siteMapPath + fileName;
                //var logfile = _fileName;
                string SessionID = "noSession";
                DateTime serverTime = DateTime.Now;
                DateTime utcTime = serverTime.ToUniversalTime();
                // convert it to Utc using timezone setting of server computer
                TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("Pakistan Standard Time");
                var logdate = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi);
                var filemonth = logdate.ToString("dd-MM-yyyy");
                string filenname = "Console", useragent="", hostAddress="";
                System.Web.SessionState.HttpSessionState ss = null;
                try
                {
                     filenname = System.Web.HttpContext.Current.Request.Url.Authority;
                     useragent = System.Web.HttpContext.Current.Request.UserAgent;
                     hostAddress = System.Web.HttpContext.Current.Request.UserHostAddress;
                     ss = HttpContext.Current.Session;
                }
                catch (Exception ex)
                {
                    filenname = "Console"; useragent = ""; hostAddress = "";
                }
                if (filenname.IsNotNullOrEmpty())
                {
                    filenname = filenname.Replace(":", "").Replace("//", "").Replace("\\", "").Replace(".", "_") + "\\" + filemonth;
                    fileName = "\\" + fileName;
                }
                var logfileDirectory = "C:\\inetpub\\vhosts\\ilaan.com\\ilaanlogs\\" + filenname;
                var logfile = "C:\\inetpub\\vhosts\\ilaan.com\\ilaanlogs\\" + filenname + fileName;
                if (logsfilepath.IsNotNullOrEmpty())
                {
                     logfileDirectory = logsfilepath + filenname;
                              logfile = logsfilepath + filenname + fileName;
                }
                //  logfile = "C:\\inetpub\\vhosts\\ilaan.com\\tm.ilaan.com\\"+ fileName;
                ////////////
                
                if (ss.IsNotNull() && ss.SessionID.IsNotNullOrEmpty())
                    SessionID = ss.SessionID;
                string url = "";
                string Refurl = "";
                try
                {
                    url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
                    Refurl = System.Web.HttpContext.Current.Request.UrlReferrer.ToString();
                }
                catch { }
                // if (!line.Contains("#") || line.Contains("setclickcount") || line.Contains("bot"))
                if (useragent.IsNotNullOrEmpty() && useragent.ToLower().Contains("bot"))
                    logfile = logfile.Replace(".txt", "_bot.txt");
                else if (textToWrite.Contains("setclickcount"))
                    logfile = logfile.Replace(".txt", "_setclickcount.txt");
                ////////////
                //string createText = "SessionID=>"+ SessionID + "  Time:" + logdate + "  ->  " + textToWrite + Environment.NewLine;
                string createText = SessionID + "#" + hostAddress + "#" + useragent +"#"+ Refurl + "$" + logdate.ToString("dd-MM-yyyy") + "$" + logdate.ToString("H:mm:ss") + "$" + url.Replace(",", "").Replace(";", "")+"#"+ Refurl.Replace(",", "").Replace(";", "") + "$" + textToWrite + Environment.NewLine;
                bool exists = System.IO.Directory.Exists(logfileDirectory);
                if (!exists)
                {
                    DirectorySecurity dSecurity = new DirectorySecurity();
                    dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
                    System.IO.Directory.CreateDirectory(logfileDirectory, dSecurity);
                }
                if (!File.Exists(logfile))
                    File.WriteAllText(logfile, createText);
                else
                    File.AppendAllText(logfile, createText);


            }
            catch (Exception e)
            {

               
            }
        }
        public static void AddLog(string className, Exception e,string CustomText="")
        {
            
            AddLog(className + "_Exception.txt", e.Message + "$" + e.StackTrace+ CustomText);
        }
        public static void AddLog(string className, Exception e)
        {

            AddLog(className + "_Exception.txt", e.Message + "$" + e.StackTrace );
        }
        public static bool IsAllDigits(string s)
        {
            foreach (char c in s)
            {
                if (!char.IsDigit(c))
                    return false;
            }
            return true;
        }
        public static DateTime GetPakistanDateTime()
        {
            DateTime serverTime = DateTime.Now;
            DateTime utcTime = serverTime.ToUniversalTime();          
            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("Pakistan Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi);
        }
        public static int GetPakistanDateTimeYear()
        {
            DateTime serverTime = DateTime.Now;
            DateTime utcTime = serverTime.ToUniversalTime();
            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("Pakistan Standard Time");
           var logdate= TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi);
            return logdate.Year;
        }
        public static string StrForDBEscaping(this string str)
        {
            if (str.IsNotNullOrEmpty())
                str = str.Replace("'", "''").Replace("  ", " ");
            return str;

        }
        public static string StrForEscaping(this string str)
        {
            if (str.IsNotNullOrEmpty())
                str = str.Replace("  ", " ");
            return str;

        }
    }

    #region Helper classes
	public abstract class AbstractAttributes<T, K>
    {
        protected List<K> Attributes = new List<K>();

        public AbstractAttributes()
        {
            foreach (var member in typeof(T).GetMembers())
            {
                foreach (K attribute in member.GetCustomAttributes(typeof(K), true))
                    Attributes.Add(attribute);
            }
        }
    }

    public class DescriptionAttributes<T> : AbstractAttributes<T, DescriptionAttribute>
    {
        public List<string> Descriptions { get; set; }

        public DescriptionAttributes()
        {
            Descriptions = Attributes.Select(x => x.Description).ToList();
        }
    }


    //public class ESDateTimeConverter : IsoDateTimeConverter
    //{
    //    public ESDateTimeConverter()
    //    {
    //        base.DateTimeFormat = "dd-MM-yyyy"; // "yyyy-MM-ddTHH:mm:ss.fffZ";
    //    }
    //}


    #endregion
}
