using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ranker
{
	public class Player : ICloneable
	{
		public long id;
		public string realname;
		public long side;
        public bool fake;

		public object Clone()
		{
			return this.MemberwiseClone();
		}
	}
}
