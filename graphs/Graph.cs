using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms.DataVisualization.Charting;

namespace Ranker
{
	class Graph
	{
		public virtual void Render(Chart chart, Model model, RankingAlgorithm algo) { }
		public virtual string Title() { return "None"; }
	}
}
