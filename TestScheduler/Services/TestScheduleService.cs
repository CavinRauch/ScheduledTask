using ScheduledTask.BaseClasses;
using ScheduledTask.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestScheduler.Services
{
	public class TestScheduleService : BaseScheduledService
	{
		public TestScheduleService() : base()
		{
			this.FindJobs = true;
		}
	}

	public static class LineWriter
	{
		public static void WriteLine(string line)
		{
			Console.WriteLine(line);
		}
	}
}
