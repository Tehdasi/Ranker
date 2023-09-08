using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

using System.Windows.Forms.DataVisualization.Charting;

namespace Ranker
{
	public class Graph
	{
		public virtual void Render(Chart chart, Model model, RankingAlgorithm algo) { }
		public virtual string Title() { return "None"; }

		public static List<Graph> GetAllGraphs()
		{
			return Assembly.GetExecutingAssembly()
				.GetTypes()
				.Where(t => t.IsSubclassOf(typeof(Graph)))
				.Select(t => {
					Graph? g = (Graph?)Activator.CreateInstance(t);
					Debug.Assert(g != null);
					return g;
					}).ToList();
		}
	}
}
