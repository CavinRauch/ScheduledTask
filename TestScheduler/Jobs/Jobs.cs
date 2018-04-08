using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ScheduledTask.Core;
using TestScheduler.Services;
using ScheduledTask.BaseClasses;

namespace TestScheduler.Jobs
{
	public class Job1 : BaseScheduledJob
	{
		public Job1()
		{
			Schedule = TriggerBuilder.Create()
									 .WithCronSchedule("0 0 8 9W * ? *")
									 .Build();
		}

		public override void Execute(IJobExecutionContext context) => 
			LineWriter.WriteLine($"[Job 1] Execute called at \t{DateTime.Now.TimeOfDay}");
	}

	public class Job2 : BaseScheduledJob
	{
		public Job2()
		{
			Schedule = TriggerBuilder.Create()
									 .WithSimpleSchedule(s => s.WithIntervalInSeconds(5).RepeatForever())
									 .Build();
		}

		public override void Execute(IJobExecutionContext context) =>
			LineWriter.WriteLine($"[Job 1] Execute called at \t{DateTime.Now.TimeOfDay}");
	}
}
