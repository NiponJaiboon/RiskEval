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

namespace Sathish.ServiceScheduler
{
    public enum ScheduleInterval
    {
        EveryDay,
        EveryWeek,
        EveryMonth,
        EveryMinute
    }
}
