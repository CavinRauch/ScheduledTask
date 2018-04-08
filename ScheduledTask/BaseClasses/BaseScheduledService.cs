using Quartz;
using ScheduledTask.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduledTask.BaseClasses
{
	public class BaseScheduledService : IScheduledService
	{
		public string Description { get; set; }
		public string DisplayName { get; set; }
		public string ServiceName { get; set; }

		public BaseScheduledService()
		{
			//Set the name of the service
			DisplayName = ServiceName = this.GetType().Name;
		}

		/// <summary>
		/// This method is executed when the service starts
		/// </summary>
		/// <param name="context"></param>
		public virtual void Start()
		{

		}

		/// <summary>
		/// This method is executed when the service stops
		/// </summary>
		/// <param name="context"></param>
		public virtual void Stop()
		{

		}

		/// <summary>
		/// If true searches for all jobs that implementation IScheduledJob across the assemblies else will revert to the provided jobs.
		/// </summary>
		public bool FindJobs { get; set; } = true;

		/// <summary>
		/// Jobs execute when service is running.
		/// </summary>
		public IEnumerable<IScheduledJob> Jobs { get; set; }
	}
}
