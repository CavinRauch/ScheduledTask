using ScheduledTask.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;

namespace ScheduledTask.BaseClasses
{
	public abstract class BaseScheduledJob : IScheduledJob
	{
		public IJobDetail JobDetail
		{
			get
			{
				return JobBuilder.Create(this.GetType()).Build();
			}
		}

		public ITrigger Schedule { get; set; }

		public abstract void Execute(IJobExecutionContext context);
	}
}
