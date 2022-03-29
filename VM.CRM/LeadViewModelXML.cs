using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.CRM
{
    public class LeadViewModelXML
    {
        public string Key { get; set; }
        public string KeyValue { get; set; }
        public string ParentKey { get; set; }
        public int Depth { get; set; }
        public bool HasChild { get; set; }

    }
    public class ListLeadViewModelXML
    {
        public List<LeadViewModelXML> Nodes { get; set; }
    }
    }
