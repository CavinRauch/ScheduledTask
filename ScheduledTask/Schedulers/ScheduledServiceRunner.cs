using Quartz;
using Quartz.Impl;
using ScheduledTask.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;
using Topshelf.Quartz;
using Common.Logging;

namespace ScheduledTask
{
	public static class ScheduledServiceRunner
	{
		private static IScheduler _scheduler = StdSchedulerFactory.GetDefaultScheduler();

		public static void StartScheduler()
		{
			if (!_scheduler.IsStarted)
				_scheduler.Start();
		}

		public static void ShutdownScheduler(bool waitForJobsToComplete = true)
		{
			if (_scheduler.IsStarted)
				_scheduler.Shutdown(waitForJobsToComplete);
		}

		public static void ScheduleJob(IScheduledJob scheduledJob) => ScheduleJobs(new IScheduledJob[] { scheduledJob });

		public static void ScheduleJobs(IEnumerable<IScheduledJob> scheduledJobs)
		{
			foreach (var job in scheduledJobs)
				_scheduler.ScheduleJob(job.JobDetail, job.Schedule);
		}
	}
}
