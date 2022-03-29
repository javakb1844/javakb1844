using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.Look
{
    public class LookDesignationViewModel
    {
        public long LookDesignationId { get; set; }
        public string DesignationName { get; set; }
        public Nullable<long> LookDepartmentId { get; set; }
    }
}
