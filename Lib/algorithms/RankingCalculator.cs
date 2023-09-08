using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Diagnostics;

namespace Ranker
{
	public class RankingCalculator
	{
		List<Game> gamesByTime;
		List<PlayerEvent> playerEvents;
		public bool verbose; 
		protected bool useFakePlayers;


		public Dictionary<string, PlayerInfo> playerInfo;
		public List<GameInfo> gameInfo;

		public RankingCalculator()
		{
			gamesByTime = new List<Game>();
			playerInfo = new Dictionary<string, PlayerInfo>();
			gameInfo = new List<GameInfo>();
			playerEvents= new List<PlayerEvent>();

			verbose = false;
			useFakePlayers = false;
		}

		public virtual void AddGame(Game g)
		{
			Game ng = (Game)g.Clone();

			if( useFakePlayers )
				ng.InsertFakePlayers();

			gamesByTime.Add(ng);
		}

		public void SetPlayerEvents( List<PlayerEvent> pe )
		{
			playerEvents = pe;
		}


		public virtual GameInfo RunGame(Game g)
		{
			return new GameInfo(g);
		}

		public void RetireOldPlayers( DateTime fromDate )
		{
			List<PlayerInfo> old = new List<PlayerInfo>();
			foreach (string pn in playerInfo.Keys)
			{
				PlayerInfo pi= playerInfo[pn];
				if ( pi.real && pi.active && ((fromDate - pi.lastActivity).Days > Model.retirementDays))
					old.Add(pi);
			}

			foreach (PlayerInfo pi in old)
				RetirePlayer(pi);
		}

		public virtual void RetirePlayer(PlayerInfo p)
		{
		}

		public void UnRetirePlayers(Game game)
		{
			foreach (Player plr in game.players)
			{
				PlayerInfo pi = playerInfo[plr.realname];
				
				if( !pi.active )
					UnRetirePlayer(playerInfo[plr.realname]);
			}
		}

		protected virtual void RunPlayerEvent(PlayerEvent pe)
		{
		}

		public virtual void UnRetirePlayer(PlayerInfo p)
		{
		}

		public Dictionary<string, PlayerInfo> Calc()
		{
			gamesByTime = gamesByTime.OrderBy(item => item.datetime).ToList();


			foreach (PlayerEvent pe in playerEvents)
				RunPlayerEvent(pe);

			if( useFakePlayers )
				for (int i = 2; i <= 6; i++)
				{
					PlayerInfo pi = new PlayerInfo();
					pi.score = 1500;
					pi.games = 0;
					pi.name = "EmptyLane" + i;
					pi.real = false;
					pi.active = true;
					playerInfo.Add(pi.name, pi);
				}

			foreach (Game g in gamesByTime)
			{
				UnRetirePlayers(g);

				GameInfo gi = RunGame(g);

				foreach (Player p in g.players)
				{
					PlayerInfo pi = playerInfo[p.realname];
					pi.score += (p.side == 0) ? gi.sentinelPoints : gi.scourgePoints;
					pi.lastActivity = g.datetime;
				}

				gameInfo.Add(gi);

				RetireOldPlayers(g.datetime);
			}

			return playerInfo;
		}
	}
}
