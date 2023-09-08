using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ranker
{
	public interface RankingAlgorithm
	{
		RankingCalculator GetCalculator();

		double PercentageChance(List<PlayerInfo> team1, List<PlayerInfo> team2);
		bool UseFakePlayers();
	}
}
