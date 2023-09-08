using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ranker
{
	class EloRankingAlgorithm : RankingAlgorithm
	{
		public RankingCalculator GetCalculator()
		{
			return new EloRankingCalculator();
		}

		double EloForTeam( List<PlayerInfo> team )
		{
			double elo= 0;

			foreach( PlayerInfo pi in team )
				elo+= pi.score; 

			return elo;
		}

		public double PercentageChance(List<PlayerInfo> team1, List<PlayerInfo> team2)
		{
			double
				side1elo = EloForTeam( team1 ),
				side2elo = EloForTeam( team2 );

			return 1.0 / (1 + Math.Pow(10, (side2elo - side1elo) / 400.0));
		}

		public bool UseFakePlayers()
		{
			return true;
		}

	}
}
