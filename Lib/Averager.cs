using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ranker
{
	public class Averager
	{
		Queue<double> queue;
		int window;

		public Averager(int p)
		{
			queue = new Queue<double>();
			window = p;
		}

		public void Add(double v)
		{
			queue.Enqueue(v);
			if (queue.Count > window)
				queue.Dequeue();
		}

		public double Average()
		{
			double v = 0;
			int numer = Math.Min( queue.Count, window );

			foreach (double d in queue)
				v += d;

			return v / numer;
		}
	}
}
