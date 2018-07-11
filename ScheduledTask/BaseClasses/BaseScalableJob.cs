using ScheduledTask.Core;
using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;

namespace ScheduledTask.BaseClasses
{
	public abstract class BaseScalableJob<T> : BaseJob
	{
		public abstract int ScaleCount { get; set; }
		public abstract IEnumerable<T> GetData();
		public abstract void Process(T singleItem);

		public BaseScalableJob(string jobName, string groupName) : base(jobName, groupName)
		{
			ConcurrentData = new ConcurrentQueue<T>();
		}

		private ConcurrentQueue<T> ConcurrentData { get; set; }
		public override void Execute(IJobExecutionContext context)
		{
			//Enqueue Items
			foreach (var item in GetData())
				ConcurrentData.Enqueue(item);

			//Add Actions to List
			List<Action> jobs = new List<Action>();
			for (int i = 1; i <= ScaleCount; i++)
			{
				jobs.Add(() =>
				{
					T dataItem;
					while (ConcurrentData.TryDequeue(out dataItem))
					{
						Process(dataItem);
					}
				});
			}

			//Execute all Actions Parallel
			ParallelOptions options = new ParallelOptions
			{
				MaxDegreeOfParallelism = 15
			};
			Parallel.Invoke(options, jobs.ToArray());
		}
	}
}
