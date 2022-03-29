
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.HRMS
{

    //public class RecuirtmentSelectionViewModel
    //{
    //    public Recruitment Recruitment { get; set; }

    //    public HRRecruitmentProcess HRRecruitmentProcess { get; set; }

    //    public List<LookSelectionProcessViewModel> SelectionProcessList { get; set; }
    //}
    public class LookSelectionProcessViewModel
    {
        public long LookSelectionProcessId { get; set; }
        public string SelectionProcess { get; set; }
        public Nullable<long> Type { get; set; }
        public long? HRRecruitmentProcessId { get; set; }
    }
}
