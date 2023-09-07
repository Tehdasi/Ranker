using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ranker
{
	public class Game : ICloneable
	{
		public long id;
		public DateTime datetime;
		public int winSide;

		public List<Player> players;

		public Game()
		{
			players= new List<Player>();
		}


        public bool PlayerOnWinSide(string playerName)
        {
            foreach (Player p in players)
            {
                if (p.realname == playerName)
                    return p.side == WinSide();
            }

            return false;
        }


        public string GetPlayerHash()
        {
            string hash = "";
            var pobrn = players.OrderBy(p => p.realname);
            Player first = pobrn.First();

            foreach (Player p in pobrn)
                if( !p.fake )
                    hash += (p.side == first.side) + " " + p.realname + "|";
    
            return hash;
        }


        public object Clone()
		{
			Game ng = (Game)this.MemberwiseClone();

			ng.players = new List<Player>();

			foreach (Player p in players)
				ng.players.Add((Player)p.Clone());

			return ng;
		}

        public Player GetPlayerByRealName(string name)
        {
            foreach (Player p in players)
                if (p.realname == name)
                    return p;
            return null;
        }

		public bool HasUnknowns()
		{
			foreach( Player p in players )
			{
				if( p.realname == "" )
					return true;
			}
			
			return false;
		}

		public void InsertFakePlayers()
		{
			int[] s = new int[2];

			foreach (Player p in players)
				s[p.side]++;

			if (s[0] != s[1])
			{
				Player fp = new Player();
				fp.side = (s[0] < s[1]) ? 0 : 1;
				fp.id = s[0] + s[1] + 1;
                fp.fake = true;
				fp.realname = "EmptyLane" + Math.Max(s[0], s[1]);
				players.Add(fp);
			}
		}

		public bool IsRanked()
		{

			foreach (Player p in players)
			{
				if (p.realname == "")
					return false;
			}


			if (WinSide() == -1)
				return false;

			{
				int s1 = 0, s2 = 0;
				foreach (Player p in players)
					if (p.side == 0) s1++; else s2++;

				if (((s1 == 1) && (s2 == 5)) ||
					((s2 == 1) && (s1 == 5))) return false;
			}

			if (players.Count == 2)
				return false;

			return true;
		}

		public int WinSide()
		{
			switch (winSide)
			{
				case 0:
					return -1; // can't be determined
				case 1:
				case -1:
				case -3:
					return 0;
				case 2:
				case -2:
				case -4:
					return 1;
			}

			return -1;
		}

	}
}
