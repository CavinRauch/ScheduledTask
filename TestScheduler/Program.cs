using ScheduledTask;
using ScheduledTask.BaseClasses;
using ScheduledTask.Core;
using System;
using System.Collections.Generic;
using System.Threading;
using TestScheduler.Services;

namespace TestScheduler
{
	static class Program
	{
		static void Main(string[] args)
		{
			ScheduledTask.Schedulers.WindowsServiceInstaller.InstallAsWindowsService(new BaseWindowsService());
		}
	}
}
