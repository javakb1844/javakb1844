
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Core.Logging
{
    public class LogGroupDictionary
    {
        public LogGroup LogGroup { get; set; }

        public long LogGroupDictionaryId
        {
            get;
            set;
        }

        public long LogGroupId
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
