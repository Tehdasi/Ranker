using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ranker
{
	class PairRankingAlgorithm : RankingAlgorithm
	{
		public RankingCalculator GetCalculator()
		{
			return new PairRankingCalculator();
		}

		public double PercentageChance(List<PlayerInfo> team1, List<PlayerInfo> team2)
		{
			return 0;
		}

		public bool UseFakePlayers()
		{
			return false;
		}

	}
}
