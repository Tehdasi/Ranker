using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using System.Windows.Forms.DataVisualization.Charting;

namespace Ranker.graphs
{
	class RankerQualityGraph : Graph
	{
		public override void Render(Chart chart, Model model, RankingAlgorithm algo ) 
		{
			RankingCalculator ec = algo.GetCalculator();

			ec.SetPlayerEvents( model.playerEvents );

			foreach (Game g in model.games)
				if (g.IsRanked())
					ec.AddGame(g);

			ec.Calc();


			Series ser = new Series();
			ser.Name = "Quality";
			ser.ChartType = SeriesChartType.Bubble;
			ser.MarkerStyle = MarkerStyle.Circle;
			ser.MarkerColor = Color.FromArgb(127, Color.Red);

			Dictionary<double, int> dict = new Dictionary<double, int>();

			for (int i = 0; i < ec.gameInfo.Count; i++)
			{
				GameInfo gi=  ec.gameInfo[i];
				double v = ((int)(gi.winChanceSentinel*40))/40.0 + (gi.sentinelWin ? 0 : 2);

				if (!dict.ContainsKey(v))
					dict.Add(v, 0);
				dict[v]++;
			}

			foreach (double k in dict.Keys)
			{
				bool sw = (k >= 2);
				double wc = sw ? k - 2 : k;
				DataPoint dp = new DataPoint(wc,
					new double[] { sw ? 2 : 1, dict[k] * 0.002 });
				ser.Points.Add(dp);
			}
			chart.Series.Add(ser);
			chart.ChartAreas[0].AxisX.Minimum = 0;
			chart.ChartAreas[0].AxisX.Maximum = 1;
		}

		public override string Title() 
		{ 
			return "Ranker Quality"; 
		}
	}
}
