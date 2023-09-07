using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ranker
{
	public class PairPlayerData
	{
		public string m_name;
		public double m_value;
		public int m_winCount;
		public int m_lossCount;

		public PairPlayerData(string in_name)
		{
			m_name = in_name;
			m_value = 0;
			m_winCount = 0;
			m_lossCount = 0;
		}
	}
}
