using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms.DataVisualization.Charting;

namespace Ranker
{
	class EloVsTimeGraph : Graph
	{
		public override string Title()
		{
			return "Elo vs Time";
		}


		public override void Render( Chart chart, Model model, RankingAlgorithm algo )
		{
			RankingCalculator ec = algo.GetCalculator();

			ec.SetPlayerEvents( model.playerEvents );

			foreach (Game g in model.games)
				ec.AddGame(g);

			ec.Calc();

			foreach (string p in ec.playerInfo.Keys)
			{
				PlayerInfo pi = ec.playerInfo[p];

				Averager avg = new Averager(1);
				Series ser = new Series();
				ser.Name = pi.name;
				ser.ChartType = SeriesChartType.Line;
				for (int i = 0; i < ec.gameInfo.Count; i++)
				{
					GameInfo gi = ec.gameInfo[i];

					if (gi.postGameScore.ContainsKey(pi.name))
						avg.Add(gi.postGameScore[p]);
					if (avg.Average() > 0)
					{
						ser.Points.AddXY(i, avg.Average());
					}
				}

				chart.Series.Add(ser);
			}

			chart.ChartAreas[0].AxisX.Title = "games";
			chart.ChartAreas[0].AxisY.Title = "ELO";
			chart.ChartAreas[0].AxisY.Minimum = 1000;
			chart.ChartAreas[0].AxisX.Minimum = 0;
			chart.ChartAreas[0].AxisX.Maximum = ec.gameInfo.Count;
		}
	}
}
