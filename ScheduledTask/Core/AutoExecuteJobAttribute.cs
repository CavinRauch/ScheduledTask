using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduledTask.Core
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class AutoExecuteJobAttribute : Attribute
	{
		public bool Enabled = false;
	}
}
