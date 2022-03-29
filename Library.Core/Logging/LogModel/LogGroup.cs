using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Core.Logging
{
    public class LogGroup
    {

        public LogGroup()
        {
            this.LogGroupFilters = new List<LogGroupFilter>();
            this.LogGroupDictionaries = new List<LogGroupDictionary>();
            this.LogItems = new List<LogItem>();
        }

        public virtual ICollection<LogGroupFilter> LogGroupFilters { get; set; }
        public virtual ICollection<LogGroupDictionary> LogGroupDictionaries { get; set; }
        public virtual ICollection<LogItem> LogItems { get; set; }


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

        public string GroupName
        {
            get;
            set;
        }

        public string ExtraData
        {
            get;
            set;
        }

        
    }
}
