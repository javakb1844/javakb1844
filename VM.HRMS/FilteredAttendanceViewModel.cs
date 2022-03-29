using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.HRMS;

namespace VM.HRMS
{
    public class FilteredAttendanceViewModel
    {
        public long EmployeeId { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public IEnumerable<AttendanceViewModel> Attendance { get; set; }

    }
    public class InputAttendanceViewModel
    {
        public int BioMetricId { get; set; }
        public DateTime LogDate { get; set; }
        public int InOutStatusId { get; set; }
        public string LogTime { get; set; }

    }
    public class OutputAttendanceViewModel
    {
        public int BioMetricId { get; set; }
        public DateTime DateTimeRecord { get; set; }
        public DateTime DateOnly { get; set; }
        public string FullName { get; set; }
        public int? dwInOutMode { get; set; }
        public bool? IsEdit { get; set; }     
	  
    }
    public class AttendanceViewModel
    {
        public long EmployeeId { get; set; }
        public string FullName { get; set; }
        public int BiometricId { get; set; }
        public DateTime? InDateOnly { get; set; }
        public DateTime? TimeIn { get; set; }
        public DateTime? LogDate { get; set; }
        public DateTime? OutDateOnly { get; set; }
        public DateTime? TimeOut { get; set; }
        public System.TimeSpan? RTimeIn { get; set; }
        public System.TimeSpan? RTimeOut { get; set; }
        
        public int? TotalMinutes { get; set; }
        public string DayCaption { get; set; }
        public int? IndwInOutMode { get; set; }
        public int? OutdwInOutMode { get; set; }
       
              public bool? InIsEdit { get; set; }
        public bool? OutIsEdit { get; set; }
        public string PolicyValue { get; set; }

    }
}
