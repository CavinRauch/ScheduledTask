using Quartz;
using ScheduledTask.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;
using Topshelf.Quartz;
using Common.Logging;

namespace ScheduledTask.Schedulers
{
	public static class WindowsServiceInstaller
	{
		internal static bool Running = false;

		/// <summary>
		/// Will create a windows service which will be used to schedule available jobs.
		/// </summary>
		/// <param name="service"></param>
		public static void InstallAsWindowsService(IScheduledService service)
		{
			if (Running)
				throw new InvalidOperationException("Service has already been created. Only one service may be run at time. Consider using jobs for multiple");

			//Add Enabled Executes
			service.Jobs.AddRange(AppDomain.CurrentDomain.GetAssemblies()
								  .SelectMany(a => a.GetTypes())
								  .Where(t => Attribute.IsDefined(t, typeof(AutoExecuteJobAttribute))
											&& ((AutoExecuteJobAttribute)t.GetCustomAttributes(typeof(AutoExecuteJobAttribute), false).First()).Enabled)
								  .Select(c => Activator.CreateInstance(c) as IScheduledJob)
								  .Where(c => c != null));

			HostFactory.Run(x =>
			{
				x.Service<IScheduledService>(s =>
				{
					s.WhenStarted(a => a.Start());
					s.WhenStopped(a => a.Stop());
					s.ConstructUsing(a => service);

					foreach (var job in service.Jobs)
						s.ScheduleQuartzJob(q =>
								q.WithJob(() => JobBuilder.Create(job.GetType()).Build())
								 .AddTrigger(() => job.Schedule));
				});

				x.RunAsLocalSystem()
				 .DependsOnEventLog()
				 .StartAutomatically()
				 .EnableServiceRecovery(rc => rc.RestartService(1));

				x.SetServiceName(service.ServiceName);
				x.SetDisplayName(service.DisplayName);
				x.SetDescription(service.Description);
			});
		}

	}
}
