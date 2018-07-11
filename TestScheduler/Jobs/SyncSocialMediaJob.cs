using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using TestScheduler.Services;
using System.Threading;
using System.Configuration;
using System.Collections.Concurrent;
using ScheduledTask.Core;

namespace TestScheduler.Jobs
{
	[AutoExecuteJob(Enabled = true)]
	public class SyncSocialMediaJob : ScheduledTask.BaseClasses.BaseScalableJob<string>
	{
		public SyncSocialMediaJob() : base ("SyncMedia","Syncs")
		{
			#region NOTES
			//1)Get Items from database where Pending
			//1)Status = Lock //Save
			//1)Add to CocurrentData

			//2)Status = InProcess //Save
			//2)ExecuteScalarJobs

			//3)Complete 
			//3)Status = Success //Save 
			#endregion

			ScaleCount = 4;
			Schedule = TriggerBuilder.Create()
									 .WithSimpleSchedule(s => s.WithIntervalInSeconds(30).RepeatForever())
									 .Build();
		}

		public override int ScaleCount { get; set; }
		public override ITrigger Schedule { get; set; }

		public override IEnumerable<string> GetData() => new string[] { "Billy", "Bobby", "Sammy", "Jessy", "William", "Michael", "Candice", "Mary" };

		public override void Process(string singleItem)
		{
			Console.WriteLine($"{DateTime.Now.TimeOfDay}:{singleItem}");
			Thread.Sleep(5000);
		}
	}
}
