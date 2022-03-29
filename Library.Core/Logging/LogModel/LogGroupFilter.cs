using System;
namespace Library.Core.Logging
{
    public class LogGroupFilter
    {

        public long LogGroupFilterId
        {
            get;
            set;
        }

        public long LogGroupId
        {
            get;
            set;
        }

        public string FilterName
        {
            get;
            set;
        }

        public string FilterValue
        {
            get;
            set;
        }

        public LogGroup LogGroup { get; set; }
    }
}
