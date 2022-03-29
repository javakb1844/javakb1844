using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Library.Core.TimerConsole
{
    public class TimerApp
    {
        public string TimerName { get; set; }
        Timer _timer;
        TimerAppTickHandler _handler;
        public delegate void TimerAppTickHandler();
        public TimerApp(
            string timerName,            
            int defaultInterval,             
            bool immediatelyFireEventAndThenStart,
            TimerAppTickHandler handler)
        {
            TimerName = timerName;
            _timer = new Timer( (TimerName + ".Interval").AppSetting<int>(defaultInterval));
            _timer.Elapsed += timer_Elapsed;
            _handler = handler;
            if (immediatelyFireEventAndThenStart)
            {
                timer_Elapsed(_timer, null);
            }
            else
            {
                _timer.Start();
            }
        }
       
        //For windows services...
        public TimerApp(
    string timerName,
    int defaultInterval,
    bool immediatelyFireEventAndThenStart,
    TimerAppTickHandler handler,
            bool isConsole)
        {
            TimerName = timerName;
            _timer = new Timer((TimerName + ".Interval").AppSetting<int>(defaultInterval));
            _timer.Elapsed += timer_ElapsedService;
            _handler = handler;
            if (immediatelyFireEventAndThenStart)
            {
                timer_ElapsedService(_timer, null);
            }
            else
            {
                _timer.Start();
            }
        }
        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine(String.Format("Timer '{0}' elapsed event STARTED at '{1}'", TimerName, DateTime.UtcNow.ToLongTimeString()));
            _timer.Stop();
            _handler.Invoke();
            Console.WriteLine(String.Format("Timer '{0}' elpase event ENDED at '{1}'", TimerName, DateTime.UtcNow.ToLongTimeString()));
            _timer.Start();
        }
        void timer_ElapsedService(object sender, ElapsedEventArgs e)
        {
            
            _timer.Stop();          
            _handler.Invoke();
            _timer.Start();
        }   

        //Converter:http://www.unitconversion.org/unit_converter/time-ex.html
        public const int MILLI_SECONDS_FROM_SECOND = 1000;
        public const int MILLI_SECONDS_FROM_MINUTE = 60000;
        public const int MILLI_SECONDS_FROM_HOUR = 3600000;
        public const long MILLI_SECONDS_FROM_DAY = 86400000;
        public const long MILLI_SECONDS_FROM_WEEK = 604800000;

        public const int SECONDS_FROM_MINUTE = 60;
        public const int SECONDS_FROM_SECOND = 3600;
        public const int SECONDS_FROM_DAY = 86400;
        public const int SECONDS_FROM_WEEK = 604800;

        public const int MINUTES_FROM_HOUR = 60;
        public const int MINUTES_FROM_DAY = 1440;
        public const int MINUTES_FROM_WEEK = 10080;

    }
}
