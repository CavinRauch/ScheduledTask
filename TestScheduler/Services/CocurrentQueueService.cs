using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestScheduler.Services
{
	public static class CocurrentQueueService<T>
	{
		public static ConcurrentQueue<T> Queue = new ConcurrentQueue<T>();

		public static void Enqueue(T item)
		{
			Queue.Enqueue(item);
		}

		public static bool Dequeue(out T item) => Queue.TryDequeue(out item);
	}
}
