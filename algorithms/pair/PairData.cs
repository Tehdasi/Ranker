using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ranker
{
	public class PairData
	{
		public string m_nameOne;
		public string m_nameTwo;
		public double m_value;
		public int m_winCount;
		public int m_lossCount;

		public PairData(string in_nameOne, string in_nameTwo)
		{
			m_nameOne = in_nameOne;
			m_nameTwo = in_nameTwo;
			m_value = 0;
			m_winCount = 0;
			m_lossCount = 0;
		}
	}
}