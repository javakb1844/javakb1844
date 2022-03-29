using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Services
{
    public class MethodOptions
    {
        public MethodOptions()
        {
            RealData = true;
            Log = true;
            Validate = true;
            BreakOnEntry = false;
            BreakOnDbHit = false;
        }

        public static MethodOptions Default
        {
            get
            {
                return new MethodOptions 
                {
                    RealData = true,
                    Log = true,
                    Validate = true,
                    BreakOnEntry = false,
                    BreakOnDbHit = false
                };
            }
        }

        public MethodOptions(bool realData, bool validate, bool log)
        {
            RealData = realData;
            Validate = validate;
            Log = log;
            BreakOnEntry = false;
            BreakOnDbHit = false;
        }

        public MethodOptions(MethodOptions options)
        {
            this.RealData = options.RealData;
            this.Validate = options.Validate;
            this.Log = options.Log;            
        }

        public bool RealData { get; set; }

        public bool Validate { get; set; }

        public bool Log { get; set; }

        public bool BreakOnEntry { get; set; }
        public void BreakIfBreakOnEntry()
        {
            if (BreakOnEntry)
            {
                Debugger.Break();
            }
        }

        public bool BreakOnDbHit { get; set; }
        public void BreakIfBreakOnDbHit()
        {
            if (BreakOnDbHit)
            {
                Debugger.Break();
            }
        }

        public bool BreakAfterDbHit { get; set; }
        public void BreakIfBreakAfterDbHit()
        {
            if (BreakAfterDbHit)
            {
                Debugger.Break();
            }
        }
    }
}
