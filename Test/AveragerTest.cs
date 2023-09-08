using Ranker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
	[TestFixture]
	internal class AveragerTest
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test( ExpectedResult= 1.5 )]
		public double Test1()
		{
			Averager avg= new Averager(2);

			avg.Add(1);
			avg.Add(2);


			return avg.Average();
		}

		[Test(ExpectedResult = 50)]
		public double Test2()
		{
			Averager avg = new Averager(2);

			avg.Add(0);
			avg.Add(0);
			avg.Add(0);
			avg.Add(100);


			return avg.Average();
		}
	}
}
