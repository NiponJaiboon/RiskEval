/*
This program can be used in any way one sees fit. It needs more testing and if any of you
find/fix bugs I would love to hear from you. If you make enhancements, please send me a copy/link at
mailsash@gmail.com
I haven't tested it fully. In case this screws up any of your work you have only yourselves to blame.

Since this program uses the email functionality, you are advised to tweak/turn-off some features of your anti-virus (esp. McAfee)


USAGE:
(In the OnStart() of your service)
Sathish.ServiceScheduler.Scheduler sch = new Sathish.ServiceScheduler.Scheduler("MyServiceName");
Sathish.ServiceScheduler.MailConfiguration mailConfig = new Sathish.ServiceScheduler.MailConfiguration("yourmail@gmail.com", "admin@yourcompany.com", "Service Down", "localhost", "MyServiceName");
sch.MailComponent = mailConfig; //If you don't do this, all your exceptions will be logged to the event log under "Service Scheduler"
sch.SchedulerFired += new EventHandler(YourServiceMethod); //I used EventHandler because it was straightforward. You can write your own delegate signature.
sch.ScheduleWeekly(DayOfWeek.Friday, "3:00 AM");
//sch.ScheduleMonthly(4, "6:20 PM");
//sch.ScheduleDaily("4:00 AM");

Sathish.P. (mailsash@gmail.com)
*/

using System;
using System.Diagnostics;
using System.Timers;
using System.Net;
using System.Net.Mail;
using log4net;

namespace Sathish.ServiceScheduler
{
    

    /// <summary>
    /// Makes it easy to schedule windows services
    /// </summary>
    public class Scheduler
    {
        private static readonly ILog log =
            LogManager.GetLogger(typeof(Scheduler));

        # region Fields
        /// <summary>
        /// This event is fired whenever the scheduling criteria is met.
        /// Run your code in the handler.
        /// </summary>
        public event EventHandler SchedulerFired;
        private System.Timers.Timer _timer;
        private ScheduleInterval _interval;
        private string _timeString;
        private DayOfWeek _dayOfWeek;
        private int _dayOfMonth;
        private bool _mailConfigured = false;
        
        private EventLog evtLog;
        # endregion

        # region Constructors
        /// <summary>
        /// Makes it easy to schedule windows services
        /// </summary>
        /// <param name="serviceName">Name of the service. Without white spaces</param>
        public Scheduler(string serviceName)
        {
            _timer = new System.Timers.Timer();
            _timer.Elapsed += new ElapsedEventHandler(_timer_Elapsed);
            CreateEventLog(serviceName);
        }
        # endregion

       

        # region Methods
        private void CreateEventLog(string serviceName)
        {
            if (!EventLog.SourceExists(serviceName))
            {
                EventLog.CreateEventSource(serviceName, "Service Scheduler");
            }

            this.evtLog = new EventLog();
            evtLog.Source = serviceName;
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (SchedulerFired != null)
            {
                //EventArgs null for now
                try
                {
                    SchedulerFired(this, null);
                }
                catch (Exception ex)
                {
                    if (_mailConfigured)
                    {
                        this.SendMail(ex.ToString(), false);
                    }
                    else
                    {
                        evtLog.WriteEntry(ex.Message, EventLogEntryType.Error);
                    }
                }
            }
            _timer.Stop();
            System.Threading.Thread.Sleep(500);
            SetTimer();
        }

        # region Logic to get the number of milliseconds before the next fire
        private double GetNextInterval()
        {
            DateTime t = DateTime.Parse(_timeString);
            TimeSpan ts = new TimeSpan();
            int x;

            switch (_interval)
            {
                case ScheduleInterval.EveryMinute:

                    //ts = t - System.DateTime.Now;
                    //if (ts.TotalMilliseconds < 0)
                    //{
                    //    ts = t.AddDays(1) - System.DateTime.Now;
                    //}
                    break;
                case ScheduleInterval.EveryDay:

                    ts = t - System.DateTime.Now;
                    if (ts.TotalMilliseconds < 0)
                    {
                        ts = t.AddDays(1) - System.DateTime.Now;
                    }
                    break;

                case ScheduleInterval.EveryMonth:
                    int daysInMonth = System.DateTime.DaysInMonth(System.DateTime.Now.Year, System.DateTime.Now.Month);
                    if (System.DateTime.Now.Day > _dayOfMonth)
                    {
                        t = t.AddDays((daysInMonth - System.DateTime.Now.Day) + _dayOfMonth);
                    }
                    else if (_dayOfMonth == System.DateTime.Now.Day)
                    {
                        if (t < System.DateTime.Now)
                        {
                            t = t.AddDays(daysInMonth);
                        }
                    }
                    else
                    {
                        x = _dayOfMonth - System.DateTime.Now.Day;
                        t = t.AddDays(x);
                    }

                    ts = (TimeSpan)(t - System.DateTime.Now);
                    break;

                case ScheduleInterval.EveryWeek:
                    if (System.DateTime.Now.DayOfWeek > this._dayOfWeek)
                    {
                        x = System.DateTime.Now.DayOfWeek - this._dayOfWeek;
                        t = t.AddDays(7 - x);

                    }
                    else if (System.DateTime.Now.DayOfWeek == this._dayOfWeek)
                    {
                        if (t < System.DateTime.Now)
                        {
                            t = t.AddDays(7);
                        }
                    }
                    else
                    {
                        x = this._dayOfWeek - System.DateTime.Now.DayOfWeek;
                        t = t.AddDays(x);
                    }

                    ts = (TimeSpan)(t - System.DateTime.Now);
                    break;
            }

            return ts.TotalMilliseconds;
        }
        # endregion




        private void SetTimer()
        {
            double inter = (double)GetNextInterval();

            _timer.Interval = inter;
            _timer.Start();
        }

        # region Schedulers
        /// <summary>
        /// Schedules the service to run at a specified time, daily.
        /// </summary>
        /// <param name="time">Takes the format HH:MM:SS AM/PM. Ex: 9:30 AM</param>
        public void ScheduleDaily(string time)
        {
            this._interval = ScheduleInterval.EveryDay;
            this._timeString = time;
            Validate();

            SetTimer();
        }


        /// <summary>
        /// Schedules the service to run on a specified day of the week, at a specified time
        /// </summary>
        /// <param name="dayOfWeek">System.DayOfWeek enumeration</param>
        /// <param name="time">Takes the format HH:MM:SS AM/PM. Ex: 9:30 AM</param>
        public void ScheduleWeekly(DayOfWeek dayOfWeek, string time)
        {
            this._interval = ScheduleInterval.EveryWeek;
            this._dayOfWeek = dayOfWeek;
            this._timeString = time;
            Validate();

            SetTimer();
        }


        /// <summary>
        /// Schedules the service to run once a month on a specified day and specified time.
        /// </summary>
        /// <param name="dayOfMonth">Integer value between 1 and 31. Automatically adjusts for 30/28/29 days months</param>
        /// <param name="time">Takes the format HH:MM:SS AM/PM. Ex: 9:30 AM</param>
        public void ScheduleMonthly(int dayOfMonth, string time)
        {
            int daysInMonth = DateTime.DaysInMonth(System.DateTime.Now.Year, System.DateTime.Now.Month);
            if (_dayOfMonth > daysInMonth)
            { _dayOfMonth = daysInMonth; }

            this._interval = ScheduleInterval.EveryMonth;
            this._dayOfMonth = dayOfMonth;
            this._timeString = time;
            Validate();

            SetTimer();
        }

        # endregion


        public void ScheduleMinutes(string time)
        {
            this._interval = ScheduleInterval.EveryMinute;
            this._timeString = time;
            Validate();

            SetTimer();
        }


        # region Validation
        private void Validate()
        {
            if (this._timeString == null || this._timeString.Trim() == "")
            {
                throw new Exception("Time to fire cannot be null");
            }

        }
        # endregion

        # region Send Mail
        public void SendMail(string ex, bool testMail)
        {
            /*
            MailMessage message = new MailMessage();
            message.To.Add(this._mailComponent.to);
            message.From = new MailAddress(this._mailComponent.from);
            if (!testMail)
            {
                message.Subject = this._mailComponent.subject;
                message.Body = this._mailComponent.mailBody + ex;
            }
            else
            {
                message.Subject = "Service Scheduler - Test Mail";
                message.Body = ex;
            }
            SmtpClient mailClient = new SmtpClient(this._mailComponent.SMTPHost);
            mailClient.Send(message);
            */
            log.Info("send email " + ex);           
        }
        # endregion

        # endregion
    }
}
