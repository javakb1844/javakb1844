using Library.Core.Logging.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Library.Core.Logging
{
    public class LogBuilder
    {
        public LogBuilder(string groupName, bool enabled, string connectionString)
        {
            this.Repository = new LogRepository(connectionString);
            this.Group = new LogGroup();
            this.Group.DateCreated = DateTime.UtcNow;
            this.Group.GroupName = groupName;
            this.Enabled = enabled;
            this.Serializer = new JavaScriptSerializer();

            //AddInfoString("Started logging", "Auto message");
        }
        DateTime? _diff = null;
        double Diff
        {
            get
            {
                if (_diff.IsNull())
                {
                    _diff = DateTime.UtcNow;
                }

                var diff = DateTime.UtcNow - _diff.Value;
                var difference = diff.TotalMilliseconds;
                _diff = DateTime.UtcNow;
                return difference;
            }
        }

        public JavaScriptSerializer Serializer { get; set; }

        public LogGroup Group
        {
            get;
            set;
        }

        public LogRepository Repository
        {
            get;
            set;
        }

        public bool Enabled
        {
            get;
            set;
        }

        public void AddExtraData(object extraData)
        {
            if (extraData.IsNotNull())
            {
                this.Group.ExtraData = this.Serializer.Serialize(extraData);
            }
        }

        public void AddGroupFilter(string key, string value)
        {
            Group.LogGroupFilters.Add(new LogGroupFilter 
            {
                FilterName = key,
                FilterValue = value
            });
        }

        public void AddGroupFilter(params LogGroupFilter[] filters)
        {
            foreach (var item in filters)
            {
                Group.LogGroupFilters.Add(item);
            }
        }

        public void AddGroupDictionary(string key, string value)
        {
            Group.LogGroupDictionaries.Add(new LogGroupDictionary
            {
                DictionaryKey = key,
                DictionaryValue = value
            });
        }

        public void AddGroupDictionary(params LogGroupDictionary[] dictionaries)
        {
            foreach (var item in dictionaries)
            {
                Group.LogGroupDictionaries.Add(item);
            }
        }        

        
        private void AddLogItem(
            string title,
            string data,
            LogDataTypeEnum logDataTypeId,
            LogItemTypeEnum itemTypeId,
            double timeDiff,
            ICollection<LogItemDictionary> itemDictionaries)
        {
           // if (checkEnabled())
           // {
                Group.LogItems.Add(new LogItem
                {
                    Data = data,
                    DateCreated = DateTime.UtcNow,
                    LogDataTypeId = (int)logDataTypeId,
                    LogItemDictionaries = itemDictionaries,
                    LogItemTypeId = (int)itemTypeId,
                    TimeDifference = timeDiff,
                    Title = title
                });
            //}
        }

        bool checkEnabled()
        {
            return this.Group.LogItems.CountedPositive()
                && this.Enabled
                && "Logging.Enabled".AppSetting(true) == true;
        }

        public void Save()
        {
            //AddInfoString("Ended logging", "Saving to database");

            if(checkEnabled())
            {
                Repository.SaveLogGroup(this.Group);

                Repository.DB.SaveChanges();
            }
            
        }

        public void AddLogItem(LogItem item)
        {
            this.Group.LogItems.Add(item);
        }
        
        //Info
        public void AddInfoString(string title, string data)
        {
            AddLogItem(title, data, LogDataTypeEnum.Text, LogItemTypeEnum.Info, Diff, null);
        }

        public void AddInfoInt(string title, int data)
        {
            AddLogItem(title, data.ToString(), LogDataTypeEnum.Integer, LogItemTypeEnum.Info, Diff, null);
        }

        public void AddInfoJson(string title, object data)
        {
            AddLogItem(title,  Serializer.Serialize(data) , LogDataTypeEnum.Json, LogItemTypeEnum.Info, Diff, null);
        }
        
        //Warnings
        public void AddWarningString(string title, string data)
        {
            AddLogItem(title, data, LogDataTypeEnum.Text, LogItemTypeEnum.Warning, Diff, null);
        }

        public void AddWarningInt(string title, int data)
        {
            AddLogItem(title, data.ToString(), LogDataTypeEnum.Integer, LogItemTypeEnum.Warning, Diff, null);
        }

        public void AddWarningJson(string title, object data)
        {
            AddLogItem(title, Serializer.Serialize(data), LogDataTypeEnum.Json, LogItemTypeEnum.Warning, Diff, null);
        }

        //Debug
        public void AddDebugString(string title, string data)
        {
            AddLogItem(title, data, LogDataTypeEnum.Text, LogItemTypeEnum.Debug, Diff, null);
        }

        public void AddDebugInt(string title, int data)
        {
            AddLogItem(title, data.ToString(), LogDataTypeEnum.Integer, LogItemTypeEnum.Debug, Diff, null);
        }

        public void AddDebugJson(string title, object data)
        {
            AddLogItem(title, Serializer.Serialize(data), LogDataTypeEnum.Json, LogItemTypeEnum.Debug, Diff, null);
        }

        //Error
        public void AddErrorString(string title, string data)
        {
            AddLogItem(title, data, LogDataTypeEnum.Text, LogItemTypeEnum.Error, Diff, null);
        }

        public void AddErrorInt(string title, int data)
        {
            AddLogItem(title, data.ToString(), LogDataTypeEnum.Integer, LogItemTypeEnum.Error, Diff, null);
        }

        public void AddErrorJson(string title, object data)
        {
            AddLogItem(title, Serializer.Serialize(data), LogDataTypeEnum.Json, LogItemTypeEnum.Error, Diff, null);
        }

        //public void AddDebug(string title)
        //{
        //    throw new NotImplementedException();
        //}

        //public void AddInfo<TData>(string title, TData data)
        //{
        //    throw new NotImplementedException();
        //}

        //public void AddWarning<TData>(string title, TData data)
        //{
        //    throw new NotImplementedException();
        //}

        //public void AddError<TData>(string title, TData data)
        //{
        //    throw new NotImplementedException();
        //}

        //public void AddDebug<TData>(string title, TData data)
        //{
        //    throw new NotImplementedException();
        //}

        //public void AddInfo<TData>(string title, TData data, Dictionary<string, string> logItemDictionary)
        //{
        //    throw new NotImplementedException();
        //}

        //public void AddWarning<TData>(string title, TData data, Dictionary<string, string> logItemDictionary)
        //{
        //    throw new NotImplementedException();
        //}

        //public void AddError<TData>(string title, TData data, Dictionary<string, string> logItemDictionary)
        //{
        //    throw new NotImplementedException();
        //}

        //public void AddDebug<TData>(string title, TData data, Dictionary<string, string> logItemDictionary)
        //{
        //    throw new NotImplementedException();
        //}

        //public void AddInfo<TData>(string title, TData data, params string[] logItemDictionary)
        //{
        //    throw new NotImplementedException();
        //}

        //public void AddWarning<TData>(string title, TData data, params string[] logItemDictionary)
        //{
        //    throw new NotImplementedException();
        //}

        //public void AddError<TData>(string title, TData data, params string[] logItemDictionary)
        //{
        //    throw new NotImplementedException();
        //}

        //public void AddDebug<TData>(string title, TData data, params string[] logItemDictionary)
        //{
        //    throw new NotImplementedException();
        //}

        //public void AddInfo(string title, object data)
        //{
        //    throw new NotImplementedException();
        //}

        //public void AddWarning(string title, object data)
        //{
        //    throw new NotImplementedException();
        //}

        //public void AddError(string title, object data)
        //{
        //    throw new NotImplementedException();
        //}

        //public void AddDebug(string title, object data)
        //{
        //    throw new NotImplementedException();
        //}

        //public void AddInfo(string title, object data, Dictionary<string, string> logItemDictionary)
        //{
        //    throw new NotImplementedException();
        //}

        //public void AddWarning(string title, object data, Dictionary<string, string> logItemDictionary)
        //{
        //    throw new NotImplementedException();
        //}

        //public void AddError(string title, object data, Dictionary<string, string> logItemDictionary)
        //{
        //    throw new NotImplementedException();
        //}

        //public void AddDebug(string title, object data, Dictionary<string, string> logItemDictionary)
        //{
        //    throw new NotImplementedException();
        //}

        //public void AddInfo(string title, object data, params string[] logItemDictionary)
        //{
        //    throw new NotImplementedException();
        //}

        //public void AddWarning(string title, object data, params string[] logItemDictionary)
        //{
        //    throw new NotImplementedException();
        //}

        //public void AddError(string title, object data, params string[] logItemDictionary)
        //{
        //    throw new NotImplementedException();
        //}

        //public void AddDebug(string title, object data, params string[] logItemDictionary)
        //{
        //    throw new NotImplementedException();
        //}

        
    }
}
