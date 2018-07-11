using ScheduledTask.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;

namespace ScheduledTask.BaseClasses
{
	public abstract class BaseJob : IScheduledJob
	{
		public BaseJob(string jobName,string groupName)
		{
			JobDetail = JobBuilder.Create(this.GetType()).WithIdentity(jobName, groupName).Build();
		}

		public IJobDetail JobDetail { get;  }

		public abstract ITrigger Schedule { get; set; }

		public abstract void Execute(IJobExecutionContext context);
	}
}
