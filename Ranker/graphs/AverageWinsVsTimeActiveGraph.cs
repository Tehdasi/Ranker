using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace Ranker.graphs
{
	class AverageWinsVsTimeActiveGraph : Graph
	{
		public override string Title()
		{
			return "Average Wins vs Time (Active)";
		}

		public override void Render(Chart chart, Model model, RankingAlgorithm algo)
		{
			RankingCalculator ec = algo.GetCalculator();

			ec.SetPlayerEvents(model.playerEvents);

			foreach (Game g in model.games)
				if (g.IsRanked())
					ec.AddGame(g);

			ec.Calc();


			foreach (string p in ec.playerInfo.Keys)
			{
				PlayerInfo pi = ec.playerInfo[p];
				if (!pi.active || !pi.real)
					continue;

				Averager avg = new Averager(50);
				Series ser = new Series();
				ser.Name = pi.name;
				ser.ChartType = SeriesChartType.Line;

				for (int i = 0; i < ec.gameInfo.Count; i++)
				{
					GameInfo gi = ec.gameInfo[i];

					if (gi.postGameScore.ContainsKey(pi.name))
					{
						if (gi.postGameScore[p] > gi.preGameScore[p])
							avg.Add(1);
						else
							avg.Add(0);
					}
					if (avg.Average() > 0)
						ser.Points.AddXY(i, avg.Average());
				}

				chart.Series.Add(ser);
			}
			chart.ChartAreas[0].AxisY.Minimum = 0;
			chart.ChartAreas[0].AxisX.Title = "Games";
			chart.ChartAreas[0].AxisY.Title = "Average wins (1 win, 0 loss, 50 samples)";
			chart.ChartAreas[0].AxisX.Minimum = 0;
			chart.ChartAreas[0].AxisX.Maximum = ec.gameInfo.Count;
		}
	}
}
