using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;
using Topshelf.Quartz;

namespace ScheduledTask.Core
{
	public interface IScheduledService
	{
		/// <summary>
		/// This method is executed when the service starts
		/// </summary>
		/// <param name="context"></param>
		void Start();

		/// <summary>
		/// This method is executed when the service stops
		/// </summary>
		/// <param name="context"></param>
		void Stop();

		/// <summary>
		/// Jobs execute when service is running.
		/// </summary>
		List<IScheduledJob> Jobs { get; set; }
		
		string ServiceName { get; }
		string DisplayName { get; }
		string Description { get; }
	}
}
