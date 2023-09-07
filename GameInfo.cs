using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ranker
{
	public class GameInfo
    { 
		public Game game;
        // scores by player
		public Dictionary<string, double> preGameScore, postGameScore;
		public double sentinelPoints, scourgePoints;
		public double winChanceSentinel, winChanceScourge;
		public bool sentinelWin;

        public double WinPoints()
        {
            return sentinelWin ? sentinelPoints : scourgePoints;
        }

        public double WinChance()
        {
            return sentinelWin ? winChanceSentinel : winChanceScourge;
        }

        public double LosePoints()
        {
            return sentinelWin ? scourgePoints : sentinelPoints;
        }

        public double LoseChance()
        {
            return sentinelWin ? winChanceScourge : winChanceSentinel;
        }
    }
}
