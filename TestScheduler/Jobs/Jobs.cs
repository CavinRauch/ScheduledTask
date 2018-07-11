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
	public class SimpleJob : BaseJob
	{
		public SimpleJob() : base("SimpleJob", "Test")
		{
			JobCount.Count++;
			Schedule = TriggerBuilder.Create()
									 // .UsingJobData("Instance", $"{JobCount.Count}")
									 //.WithSimpleSchedule(s => s.WithIntervalInSeconds(1))
									 .WithCronSchedule("")
									 .Build();
		}

		public override ITrigger Schedule { get; set; }

		public override void Execute(IJobExecutionContext context)
		{
			string instance = context.Trigger.JobDataMap["Instance"].ToString();
			Console.WriteLine($"[{instance}] Executed at {DateTime.Now.TimeOfDay}.");

			string name, message = "";
			while (CocurrentQueueService<string>.Dequeue(out name))
			{
				if (!string.IsNullOrWhiteSpace(name))
				{
					Console.ForegroundColor = ConsoleColor.DarkGreen;
					message = $"[{instance}]: {name}.";
					Console.WriteLine(message);
				}
				else if (name != "Not Set")
				{
					Console.ForegroundColor = ConsoleColor.DarkRed;
					message = "Failed to get item from queue";
					Console.WriteLine(message);
				}
				Console.ResetColor();
			}
		}
	}

	public class Enqueuer : BaseJob
	{
		public Enqueuer() : base("Enqueuer", "Test")
		{
			Schedule = TriggerBuilder.Create()
									 .WithSimpleSchedule(s => s.WithIntervalInSeconds(10))
									 .Build();
		}

		public override ITrigger Schedule { get; set; }

		public override void Execute(IJobExecutionContext context)
		{
			//Generate Cocurrent Queue
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine($"[Enqueuer] Executed at {DateTime.Now.TimeOfDay}.");
			foreach (var item in new string[] { "Jerry", "Sam", "Micheal", "John", "Pierre", "Joe", "Billy", "Simon" })
			{
				Console.WriteLine($"[Enqueuer] Enqueued {item}.");
				CocurrentQueueService<string>.Enqueue(item);
			}
			Console.ResetColor();
		}
	}

	public static class JobCount
	{
		public static int Count { get; set; } = 0;
	}
}
