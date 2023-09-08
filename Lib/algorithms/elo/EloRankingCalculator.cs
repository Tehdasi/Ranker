using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace Ranker
{
	class EloRankingCalculator : RankingCalculator
	{
		const int fillerPlayerElo = 700;
		bool
			// adjust the elo changes after every game so that the total points in the system do not change
			gameInflationAdjust;


		public EloRankingCalculator()
		{
			useFakePlayers = true;
			gameInflationAdjust = true;
		}

		void SpreadPoints(double amount)
		{
			int active = 0;

			foreach (PlayerInfo pi in playerInfo.Values)
				if (pi.active)
					active++;

			foreach (PlayerInfo pi in playerInfo.Values)
				if (pi.active)
					pi.score += amount / active;
		}

		public override void RetirePlayer(PlayerInfo p )
		{
			p.active = false;
			SpreadPoints(p.score - 1500);
		}



		public override void UnRetirePlayer(PlayerInfo p)
		{
			SpreadPoints(  1500 - p.score );
			p.active = true;
		}

		protected override void RunPlayerEvent(PlayerEvent pe)
		{
			PlayerInfo pi = new PlayerInfo();
			pi.score = pe.initialRank;
			pi.games = 0;
			pi.name = pe.name;
			pi.real = true;
			pi.active = false;

			SpreadPoints( 1500 - pe.initialRank );

			playerInfo.Add(pe.name, pi);
			pi.lastActivity = new DateTime(3000,1,1);
		}

		public override GameInfo RunGame(Game g)
		{
			const double eloK = 32;

			double[] telo = new double[2];
			int[] ts = new int[2];
			double[] etelo = new double[2];
			double[] delta = new double[2];

			telo[0] = 0;
			telo[1] = 0;

			ts[0] = 0;
			ts[1] = 0;


			Debug.WriteLineIf(verbose, "Game: #" + g.id + " " + g.datetime);

			foreach (Player p in g.players)
			{
				PlayerInfo pi = playerInfo[p.realname];
				double elo = pi.score;

				Debug.WriteLineIf(verbose, "Player: " + p.realname + " elo: " + elo + " side: " + p.side);

				telo[p.side] += elo;

				ts[p.side]++;

				pi.games++;

				foreach (Player p2 in g.players)
					if (p2.side != p.side)
					{
						if (!pi.onSide.ContainsKey(p2.realname))
							pi.onSide.Add(p2.realname, 0);
						pi.onSide[p2.realname]++;
					}
			}

			Debug.WriteLineIf(verbose, "Team 0 total elo: " + telo[0]);
			Debug.WriteLineIf(verbose, "Team 1 total elo: " + telo[1]);

			double maxSize = Math.Max(ts[0], ts[1]);
			double ad = 0.5;

			telo[0] *= (maxSize + ad) / (ts[0] + ad);
			telo[1] *= (maxSize + ad) / (ts[1] + ad);

			Debug.WriteLineIf(verbose, "Adjusting due to uneven teams.");
			Debug.WriteLineIf(verbose, "Team 0 total elo: " + telo[0]);
			Debug.WriteLineIf(verbose, "Team 1 total elo: " + telo[1]);


			etelo[0] = 1.0 / (1 + Math.Pow(10, (telo[1] - telo[0]) / 400.0));
			etelo[1] = 1.0 / (1 + Math.Pow(10, (telo[0] - telo[1]) / 400.0));

			if (gameInflationAdjust)
			{
				delta[0] = eloK * (((g.WinSide() == 0) ? 1 : 0) - etelo[0]) * ts[1] / maxSize;
				delta[1] = eloK * (((g.WinSide() == 1) ? 1 : 0) - etelo[1]) * ts[0] / maxSize;
			}
			else
			{
				delta[0] = eloK * (((g.WinSide() == 0) ? 1 : 0) - etelo[0]);
				delta[1] = eloK * (((g.WinSide() == 1) ? 1 : 0) - etelo[1]);
			}



			//				Debug.WriteLine("");
			//				Debug.WriteLine(g.filename);
			//				Debug.WriteLine(etelo[0]);

			//foreach (Player p in g.players)
			//    Debug.WriteLine(SideName((int)p.side) + ", " + p.name + ", " + p.realname + ", " + playerInfo[p.realname].elo );

			//Debug.WriteLine("team elo: " + telo[0] + "," + telo[1]);
			//Debug.WriteLine("win chance: " + etelo[0] + "," + etelo[1]);
			//Debug.WriteLine(SideName(g.WinSide()) + " wins!");
			//Debug.WriteLine("sentinel delta: " + delta[0]);
			//Debug.WriteLine("scourge delta: " + delta[1]);
			//				Debug.WriteLine(avg.Average());

			GameInfo gi = new GameInfo(g);
			gi.sentinelPoints = delta[0];
			gi.scourgePoints = delta[1];
			gi.winChanceSentinel = etelo[0];
			gi.winChanceScourge = etelo[1];
			gi.preGameScore = new Dictionary<string, double>();
			gi.postGameScore = new Dictionary<string, double>();
			gi.sentinelWin = (g.WinSide() == 0);

			foreach (Player p in g.players)
				gi.preGameScore.Add(p.realname, playerInfo[p.realname].score);

			foreach (Player p in g.players)
			{
				double adj = ((p.side == 1) == gi.sentinelWin) ? gi.sentinelPoints : gi.scourgePoints;
				gi.postGameScore.Add(p.realname, playerInfo[p.realname].score + adj);
			}

			return gi;
		}
	
	}
}
