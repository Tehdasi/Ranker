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
                    return p.side == winSide;
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

        public Player? GetPlayerByRealName(string name)
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
	}
}
