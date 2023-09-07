using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ranker
{
	public class PlayerInfo
	{
		public string name;
		public double score;
		public int games;
		public int wins;
        public int floor;
		public Dictionary<string, int> onSide;
		public bool real;
		public DateTime lastActivity;
		public bool active;
	}

	public class EloSet
	{
		public Dictionary<string, PlayerInfo> players;

		public EloSet()
		{
			players = new Dictionary<string, PlayerInfo>();
		}

	}
}
