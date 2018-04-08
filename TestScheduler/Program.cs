using ScheduledTask;
using ScheduledTask.Core;
using System;
using System.Collections.Generic;
using TestScheduler.Services;

namespace TestScheduler
{
	static class Program
	{
		static void Main(string[] args)
		{
			Common.Logging.LogManager.Adapter = new Common.Logging.Simple.ConsoleOutLoggerFactoryAdapter { Level = Common.Logging.LogLevel.Info };

			//var schedule = new TestScheduleService();

			//schedule.DisplayName = schedule.ServiceName = "Scheduled Service";
			//schedule.Description = "Testing scheduled service.";
			//schedule.FindJobs = true;

			//ScheduledServiceRunner.RunScheduledJobs(schedule);

			//These jobs should be added via code
			ScheduledServiceRunner.ScheduleJobs(GetJobs());



			ScheduledServiceRunner.StartScheduler();

			ScheduledServiceRunner.ScheduleJobs(new Jobs.Job1());

			ScheduledServiceRunner.ShutdownScheduler();

			Console.ForegroundColor = ConsoleColor.DarkGreen;
			Console.WriteLine("Completed press any key to exit...");
			Console.ReadLine();
		}

		private static IEnumerable<IScheduledJob> GetJobs()
		{
			var jobs = new List<IScheduledJob>();

			//Add jobs to list
			jobs.Add(new Jobs.Job1());
			jobs.Add(new Jobs.Job2());

			return jobs;
		}
	}
}
