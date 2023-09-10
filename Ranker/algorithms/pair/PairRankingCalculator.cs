using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;

namespace Ranker
{
	class PairRankingCalculator : RankingCalculator
	{
		Dictionary<string, PairPlayerData> m_mapPlayerData;
		Dictionary<Tuple<string, string>, PairData> m_mapPairData;

		public PairRankingCalculator()
		{
			m_mapPlayerData = new Dictionary<string, PairPlayerData>();
			m_mapPairData = new Dictionary<Tuple<string, string>, PairData>();
		}

		private PairData GetPairData(string in_nameOne, string in_nameTwo)
		{
			Tuple<string, string> key;
			if (in_nameOne.CompareTo(in_nameTwo) < 0)
				key = new Tuple<string, string>(in_nameOne, in_nameTwo);
			else
				key = new Tuple<string, string>(in_nameTwo, in_nameOne);

			if (m_mapPairData.ContainsKey(key))
				return m_mapPairData[key];

			PairData pairData = new PairData(key.Item1, key.Item2);
			m_mapPairData[key] = pairData;
			return pairData;
		}
		
		private List<PairData> MakePairList(List<string> listNames)
		{
			List<PairData> listPairData = new List<PairData>();
			for (int index = 0; index < listNames.Count(); ++index)
				for (int subIndex = index + 1; subIndex < listNames.Count(); ++subIndex)
					listPairData.Add(GetPairData(listNames[index], listNames[subIndex]));
			return listPairData;
		}


		protected override void RunPlayerEvent(PlayerEvent pe)
		{
			if (pe.add)
			{
				PlayerInfo pi = new PlayerInfo();
				pi.score = 0;
				pi.games = 0;
				pi.name = pe.name;
				pi.real = true;
				playerInfo.Add(pe.name, pi);

			}
			else
			{
				double pts = -playerInfo[pe.name].score;

				playerInfo.Remove(pe.name);

				foreach (PlayerInfo pi in playerInfo.Values)
					pi.score += pts / playerInfo.Count;
			}

		}

		private PairPlayerData GetPairPlayerData(Player p)
		{
			if (m_mapPlayerData.ContainsKey(p.realname))
				return m_mapPlayerData[p.realname];

			PairPlayerData pairData = new PairPlayerData(p.realname);
			pairData.m_value = playerInfo[p.realname].score;
			m_mapPlayerData[p.realname] = pairData;
			return pairData;
		}

		public override GameInfo RunGame(Game g)
		{
			GameInfo gi = new GameInfo(g);
			gi.winChanceSentinel = 0;
			gi.winChanceScourge = 0;
			gi.preGameScore = new Dictionary<string, double>();
			gi.postGameScore = new Dictionary<string, double>();
			gi.sentinelWin = (g.winSide == 0);

			List<PairPlayerData> listPairPlayerDataWinners = new List<PairPlayerData>();
			List<PairPlayerData> listPairPlayerDataLossers = new List<PairPlayerData>();

			g.players.ForEach(delegate(Player in_player)
			{
				PairPlayerData playerData = GetPairPlayerData(in_player);

				if( g.winSide == in_player.side )
					listPairPlayerDataWinners.Add( playerData );
				else
					listPairPlayerDataLossers.Add(playerData);
			});

			if ((0 == listPairPlayerDataWinners.Count()) || (0 == listPairPlayerDataLossers.Count()) )
			{
				gi.sentinelPoints = 0;
				gi.scourgePoints = 0;

				return gi;
			}

			List<PairData> listPairDataWinners = MakePairList(listPairPlayerDataWinners.Select(x => x.m_name).ToList());
			List<PairData> listPairDataLossers = MakePairList(listPairPlayerDataLossers.Select(x => x.m_name).ToList());

			double winTotal = 0;
			double lossTotal = 0;

			listPairPlayerDataWinners.ForEach(delegate(PairPlayerData in_player) { winTotal += in_player.m_value; });
			listPairPlayerDataLossers.ForEach(delegate(PairPlayerData in_player) { lossTotal += in_player.m_value; });
			listPairDataWinners.ForEach(delegate(PairData in_data) { winTotal += in_data.m_value; });
			listPairDataLossers.ForEach(delegate(PairData in_data) { lossTotal += in_data.m_value; });

			double winReward = 90.0F;
			double lossPenalty = -10.0F; 
			
			if (winTotal < lossTotal)
			{
				double delta = lossTotal - winTotal;
				winReward += (delta * 0.5F);
				lossPenalty -= (delta * 0.5F);
			}

			RewardPlayers(
				winReward,
				listPairPlayerDataWinners,
				listPairDataWinners,
				1, 0
				);
			RewardPlayers(
				lossPenalty,
				listPairPlayerDataLossers,
				listPairDataLossers,
				0, 1
				);
			winReward = (winReward * 0.5) / listPairPlayerDataWinners.Count;
			lossPenalty= (lossPenalty* 0.5) / listPairPlayerDataLossers.Count;
			gi.sentinelPoints = (g.winSide == 0 ? winReward : lossPenalty);
			gi.scourgePoints = (g.winSide == 1 ? winReward : lossPenalty);

			return gi;
		}


		private void RewardPlayers(double in_reward, List<PairPlayerData> in_listPlayerData, List<PairData> in_listData, int in_win, int in_loss)
		{
			double playerReward = (in_reward * 0.5F) / in_listPlayerData.Count();
			in_listPlayerData.ForEach(delegate(PairPlayerData player)
			{
				player.m_value += playerReward;
				player.m_winCount += in_win;
				player.m_lossCount += in_loss;
			});
			if (0 < in_listData.Count())
			{
				double pairReward = (in_reward * 0.5F) / in_listData.Count();
				in_listData.ForEach(delegate(PairData data)
				{
					data.m_value += pairReward;
					data.m_winCount += in_win;
					data.m_lossCount += in_loss;
				});

			}
		}
	}
}
