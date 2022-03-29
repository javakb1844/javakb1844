using Library.Core.Logging.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Core.Logging
{
    public class LogItem
    {
        public LogItem()
        {
            this.LogItemDictionaries = new List<LogItemDictionary>();
        }

        public LogItem(string title,
            string data,
            LogDataTypeEnum logDataTypeId,
            LogItemTypeEnum itemTypeId,
            double timeDiff,
            ICollection<LogItemDictionary> itemDictionaries)
        {
            this.Title = title;
            this.Data = data;
            this.LogDataTypeId = (int)logDataTypeId;
            LogItemTypeId = (int)itemTypeId;
            TimeDifference = timeDiff;
            LogItemDictionaries = itemDictionaries;
        }

        public LogGroup LogGroup { get; set; }
        public ICollection<LogItemDictionary> LogItemDictionaries { get; set; }

        public long LogItemId
        {
            get;
            set;
        }

        public long LogGroupId
        {
            get;
            set;
        }

        public DateTime DateCreated
        {
            get;
            set;
        }

        public double TimeDifference
        {
            get;
            set;
        }

        public int LogItemTypeId
        {
            get;
            set;
        }

        public int LogDataTypeId
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public string Data
        {
            get;
            set;
        }
    }
}
