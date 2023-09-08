using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ranker
{
	public class PlayerInfo
	{
		public string name= "";
		public double score= 0;
		public int games= 0;
		public int wins= 0;
		public Dictionary<string, int> onSide= new Dictionary<string, int>();
		public bool real= false;
		public DateTime lastActivity= new DateTime( 1,1,1 );
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
