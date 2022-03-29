
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Core.Logging
{
    public class LogItemDictionary
    {
        public LogItem LogItem { get; set; }

        public long LogItemDictionaryId
        {
            get;set;
        }

        public long LogItemId
        {
            get;
            set;
        }

        public string DictionaryKey
        {
            get;
            set;
        }

        public string DictionaryValue
        {
            get;
            set;
        }
    }
}
