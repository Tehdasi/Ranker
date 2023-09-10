using Ranker;
using System.Diagnostics;
using System.Security.Cryptography.Pkcs;

namespace Test
{
	[TestFixture]
	internal class EloTest
	{
		EloRankingAlgorithm elo;
		PlayerInfo p1, p2, p3, p4;

		[SetUp]
		public void Setup()
		{
			elo = new EloRankingAlgorithm();

			p1 = new PlayerInfo();
			p1.score = 1500;
			p2 = new PlayerInfo();
			p2.score = 1500;
			p3 = new PlayerInfo();
			p3.score = 1600;
			p4 = new PlayerInfo();
			p4.score = 1500;
		}

		[Test(ExpectedResult = 0.5)]
		public double Test1()
		{
			return elo.PercentageChance(
				new List<PlayerInfo> { p1 },
				new List<PlayerInfo> { p2 }
				);
		}

		[Test(ExpectedResult =0.64)]
		[DefaultFloatingPointTolerance(0.001)]
		public double Test2()
		{
			return elo.PercentageChance(
				new List<PlayerInfo> { p1,p3 },
				new List<PlayerInfo> { p2, p4 }
				);
		}

	}
}
