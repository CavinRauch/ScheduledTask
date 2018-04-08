﻿using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduledTask.Core
{
	public interface IScheduledJob : IJob
	{
		ITrigger Schedule { get; set; }

		IJobDetail JobDetail { get; }
	}
}
