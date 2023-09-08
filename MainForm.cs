using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.IO;
using System.Diagnostics;

using System.Reflection;
using System.Windows.Forms.DataVisualization.Charting;

namespace Ranker
{
    public partial class MainForm : Form
    {
        string filter;
        public Model model;

        Game currentGame;
        bool gameChanged;
        DateTime rankingPeriodStart;

        List<Game> gameLookup;

        List<Graph> graphs;

        RankingAlgorithm rankingAlgo;

        public MainForm()
        {
            InitializeComponent();
            gameChanged = false;
            gameLookup = new List<Game>();
            rankingPeriodStart = DateTime.Today;

            chart.ChartAreas.Add(new ChartArea());
            chart.Legends.Add(new Legend());

            graphs = new List<Graph>();
            foreach (Type t in Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsSubclassOf(typeof(Graph))))
            {
                Graph g = (Graph)Activator.CreateInstance(t);
                graphs.Add(g);
                graphComboBox.Items.Add(g.Title());
            }

            // testing
            //tabControl1.SelectedTab = reportsTabPage;
            //dateTimePicker.Value = new DateTime(2015, 7, 10);
            //            OnReportRankings( null, null);
        }


        void RefreshGamesList()
        {
            gamesListBox.Items.Clear();
            gameLookup.Clear();

            foreach (Game g in model.games)
            {
                gamesListBox.Items.Add(g.datetime.ToString());
                gameLookup.Add(g);
            }

            if (gamesListBox.Items.Count > 0)
                gamesListBox.SelectedIndex = 0;
        }

        void OnLoad(object sender, EventArgs e)
        {
            gameFilterComboBox.SelectedIndex = 0;

            List<string> ap = model.ActivePlayers(DateTime.Today);
            foreach (string s in ap)
            {
                biasComboBox.Items.Add(s);

                notPlayingView.Items.Add(s);
            }

            biasComboBox.SelectedIndex = 0;

            RefreshGamesList();
            rankingAlgo = model.DefaultRankingAlgo();

            RefreshPlayerGrid();
        }

		void RefreshPlayerGrid()
        {
            List<Player> ps = model.players.Where(p => !p.fake).ToList();
            playersGrid.RowCount= ps.Count+1;

            for( int i= 0; i< ps.Count; i++ )
            {
                Player p= ps[i];

                playersGrid.Rows[i].Tag = p;

                playersGrid[namePlayerColumn.Index, i].Value = p.realname;
				playersGrid[initialRankPlayerColumn.Index, i].Value = p.initialRank;
			}
		}

		Game SelectedGame()
        {
            return gameLookup[gamesListBox.SelectedIndex];
        }

        void RefreshGameInfoLabels()
        {
            label1.Text =
                "#" + currentGame.id + "\r\n" +
                "Winner: " + model.SideName(currentGame.WinSide()) + "\r\n";
            label2.Text = "\r\nDate: " + currentGame.datetime;
        }

        void RefreshPlayerList()
        {
            Game g = SelectedGame();

            reviewTeam1Label.Text = "";
            reviewTeam2Label.Text = "";

            //dataGridView1.RowCount= g.players.Count;
            for (int i = 0; i < g.players.Count; i++)
            {
                Player p = g.players[i];
                Label lab;
                if (p.side == 0)
                    lab = reviewTeam1Label;
                else
                    lab = reviewTeam2Label;

                if (p.realname == "")
                    lab.Text += "<unknown>\r\n";
                else
                    lab.Text += p.realname + "\r\n";
            }

            RefreshGameInfoLabels();

        }

        void OnGameSelected(object sender, EventArgs e)
        {
            if (currentGame != null && gameChanged)
            {
                model.Store(currentGame);
                gameChanged = false;
            }

            currentGame = SelectedGame();
            RefreshPlayerList();
        }

        void Label2Click(object sender, EventArgs e)
        {
        }

        void OnFilterSelected(object sender, EventArgs e)
        {
            filter = gameFilterComboBox.Text;
            RefreshGamesList();
        }

        void ReportRankingsInternal(bool showRecentGames)
        {
            RankingCalculator ec = model.GetCalculator();

            foreach (Game g in model.games)
                if (g.IsRanked())
                    ec.AddGame(g);

            ec.Calc();

            if (false)
            {
                int[]
                    wins = new int[model.players.Count],
                    losses = new int[model.players.Count];

                foreach (Game g in model.games)
                {
                    if (g.IsRanked())
                    {
                        foreach (Player p in g.players)
                        {
                            int pl = 0;
                            for (int i = 0; i < model.players.Count; i++)
                            {
                                if (model.players[i].realname == p.realname)
                                    pl = i;
                            }

                            if (p.side == g.WinSide())
                                wins[pl]++;
                            else
                                losses[pl]++;
                        }
                    }
                }

                for (int i = 0; i < model.players.Count; i++)
                {
                    Debug.WriteLine(model.players[i] + " " + wins[i] + " " + losses[i]);
                }
            }

            List<GameInfo> gameInfos = new List<GameInfo>();
            Dictionary<string, PlayerInfo> playersInfo;

            playersInfo = ec.playerInfo;
            gameInfos = ec.gameInfo;


            ec = model.GetCalculator();

            foreach (Game g in model.games)
                if (g.IsRanked() && g.datetime < rankingPeriodStart)
                    ec.AddGame(g);

            ec.Calc();


            Dictionary<string, PlayerInfo> oldElos = ec.playerInfo;


            SortedDictionary<double, string>
                nameByElo = new SortedDictionary<double, string>(),
                nameByOldElo = new SortedDictionary<double, string>();

            // sort names by their elo
            foreach (PlayerInfo pi in playersInfo.Values)
            {
                if (pi.real && pi.active)
                {
                    double usedElo = pi.score;

                    while (nameByElo.ContainsKey(usedElo))
                        usedElo += 0.00000001;

                    nameByElo.Add(usedElo, pi.name);
                }
            }

            // sort names by their elo old
            foreach (PlayerInfo pi in oldElos.Values)
            {
                if (pi.real && pi.active)
                {
                    double usedElo = pi.score;

                    while (nameByOldElo.ContainsKey(usedElo))
                        usedElo += 0.00000001;

                    nameByOldElo.Add(usedElo, pi.name);
                }
            }

            StringWriter sw = new StringWriter();

            sw.WriteLine("<HEAD>");
            sw.WriteLine("<STYLE> td  { font-family:arial; font-size:10pt } ");
            sw.WriteLine("th  { font-family:arial; font-size:12pt }");
            sw.WriteLine("table.main { border-collapse:collapse; width: 500px;text-align:center; }");
            sw.WriteLine("   caption { caption-side:bottom;font-weight:bold;font-style:italic;margin:4px;}");
            sw.WriteLine("table.main,table.main th, table.main td { border: 1px solid gray; } ");
            sw.WriteLine("table.main th, table.main td { height: 20px;padding: 4px;vertical-align:middle;} ");

            sw.WriteLine(" table.game {border-collapse:collapse; width:500px;text-align:center;} " +
                "table.game td { border: 1px solid gray; }");
            sw.WriteLine("</style>");
            sw.WriteLine("</HEAD>");

            sw.WriteLine("<BODY>");
            sw.WriteLine("<table class=main> <caption>Player Rankings</caption>");

            sw.WriteLine("<tr > <th> Player <th> Position <th>Elo<th width=50px># Games<th  width= 50px>Days to retirement </tr>");

            for (int i = nameByElo.Count - 1; i >= 0; i--)
            {
                PlayerInfo pi = playersInfo[nameByElo.ElementAt(i).Value];
                if (!pi.active)
                    continue;

                int pos = nameByElo.Count - i;
                int oldPos = 0;

                bool found = false;

                for (int j = 0; j < nameByOldElo.Count; j++)
                {
                    if (nameByOldElo.ElementAt(j).Value == nameByElo.ElementAt(i).Value)
                    {
                        oldPos = nameByOldElo.Count - j;
                        found = true;
                    }
                }



                double
                    celo = nameByElo.ElementAt(i).Key,
                    oelo = found ? oldElos[nameByElo.ElementAt(i).Value].score : 1500,
                    change = celo - oelo,
                    pchange = oldPos - pos;

                string eloStr = celo.ToString("#.00") + " " + ((change == 0) ? "" : (((change > 0) ? "(+" : "(") + change.ToString("#.00)")));
                string posStr = pos + (found ? ((pchange == 0) ? "" : (" ("+((pchange > 0) ? "+" : "") + pchange.ToString() + ")")) : " (new)");

                sw.WriteLine("<tr>" +
                             "<td>" + nameByElo.ElementAt(i).Value.ToString() +
                             "<td>" + posStr +
                             "<td>" + eloStr +
                             "<td>" + pi.games.ToString() +
                             "<td>" + (Model.retirementDays - Math.Floor((DateTime.Today - pi.lastActivity).TotalDays)).ToString()
                             + "</tr>");
            }
            sw.WriteLine("</table>");

            if (showRecentGames)
            {
                // make hash
                Dictionary<string, List<Game>> gamesByHash = new Dictionary<string, List<Game>>();

                foreach (Game g in model.games)
                {
                    string hash = g.GetPlayerHash();

                    if (!gamesByHash.ContainsKey(hash))
                        gamesByHash.Add(hash, new List<Game>());

                    gamesByHash[hash].Add(g);
                }





                foreach (GameInfo gi in gameInfos)
                {
                    string firstPlayer = gi.game.players.OrderBy(n => n.realname).First().realname;

                    if (gi.game.datetime >= rankingPeriodStart)
                    {

                        sw.WriteLine("<p><table class=game>");

                        sw.WriteLine("<tr><td colspan=3> #" + gi.game.id + " " + gi.game.datetime + "</td></tr>");
                        //sw.WriteLine((gi.winChanceSentinel >= gi.winChanceScourge ? "Sentinel" : "Scourge") + " favoured to win at " +
                        //	 (Math.Max(gi.winChanceSentinel, gi.winChanceScourge).ToString("0.0%")) + "<BR>");

                        //						sw.WriteLine("Sentinel: " + gi.sentinelPoints + "<BR>");
                        sw.WriteLine("<tr>");
                        sw.WriteLine("<td width=200px> Winners: <br><br>");
                        int winSide = (gi.sentinelWin ? 0 : 1);

                        DumpTeam(gi.sentinelWin ? 0 : 1, gi.game, sw);
                        sw.WriteLine("<br>" + gi.WinChance().ToString("0.0%") + " " + gi.WinPoints().ToString("+#.00") + "Pts");
                        //						sw.WriteLine();
                        sw.WriteLine("<td width=100px>");
                        sw.WriteLine("vs.");
                        sw.WriteLine("<td width=200px>Losers: <br><br>");
                        //						sw.WriteLine("Scourge: " + gi.scourgePoints + "<BR>");
                        DumpTeam(gi.sentinelWin ? 1 : 0, gi.game, sw);
                        //						sw.WriteLine("</table></p>");

                        sw.WriteLine("<br>" + gi.LoseChance().ToString("0.0%") + " " + gi.LosePoints().ToString("#.00") + "Pts");
                        sw.WriteLine("</tr >");

                        string gameHash = gi.game.GetPlayerHash();
                        List<Game> otherGames = new List<Game>();
                        foreach (Game g in gamesByHash[gi.game.GetPlayerHash()])
                        {
                            if (g.id != gi.game.id)
                                otherGames.Add( g );
                        }

                        if (otherGames.Count > 0)
                        {
                            int firstWin= 0, firstLose= 0;

                            foreach (Game g2 in otherGames)
                            {
                                if (g2.PlayerOnWinSide(firstPlayer))
                                    firstWin++;
                                else
                                    firstLose++;
                            }


                            if (!gi.game.PlayerOnWinSide(firstPlayer))
                            {
                                int t = firstWin;
                                firstWin = firstLose;
                                firstLose = t;
                            }

                            sw.WriteLine("<tr><td colspan=3 >Previously " + firstWin + "-" + firstLose + "</td></tr>");

                        }


                        sw.WriteLine("<br><br>");
                    }
                }
            }

            sw.WriteLine("<p></p>");

            sw.WriteLine("<table class=game><caption>Synthetic players</caption>");
            sw.WriteLine("<tr><th>Player<th>elo<th># Games</tr>");
            foreach (PlayerInfo pi in playersInfo.Values)
            {
                if (!pi.real)
                    sw.WriteLine("<tr><td>" + pi.name + "<td>" + pi.score.ToString("#.00") + "<td>" + pi.games + "</tr>");
            }
            sw.WriteLine("</table>");
            sw.WriteLine("</BODY>");

            webBrowser1.DocumentText = sw.ToString();

            webBrowser1.Document.ExecCommand("SelectAll", false, null);
            webBrowser1.Document.ExecCommand("Copy", false, null);
        }

        void DumpTeam(int side, Game game, StringWriter sw)
        {
            foreach (Player p in game.players)
            {
                if (p.side == side)
                {
                    if (p.realname == "")
                        sw.Write("<unknown> ");
                    else
                        sw.Write(p.realname + "<br>");
                }
            }
        }

        private void OnReportRankings(object sender, EventArgs e)
        {
            //textBox.Text= "Rankings: \r\n";
            ReportRankingsInternal(true);
        }

        private void OnScrubGames(object sender, EventArgs e)
        {
            model.ScrubDb();
        }




        Dictionary<string, PlayerInfo> GetCurrentPlayerInfo()
        {
            RankingCalculator ec = model.GetCalculator();
            Dictionary<string, Dictionary<string, Tuple<int, int>>> onSide = new Dictionary<string, Dictionary<string, Tuple<int, int>>>();

            // working out if smaller teams win more or not
            Averager avg = new Averager(10);

            int gp = 0, gw = 0;
            foreach (Game g in model.games)
                if (g.IsRanked())
                {
                    ec.AddGame(g);

                    {
                        int s1 = 0, s2 = 0;
                        foreach (Player p in g.players)
                        {
                            if (p.side == 0) s1++; else s2++;
                        }

                        if (s1 != s2)
                        {
                            gp++;
                            if (((s1 > s2) == (g.winSide == 1)))
                            {
                                gw++;
                                avg.Add(1);
                            }
                            else
                            {
                                avg.Add(0);
                            }
                            //							Debug.WriteLine(" bias " + avg.Average());

                        }
                    }

                    // total up how many times players have been on the same side as other playerss
                    foreach (Player p in g.players)
                    {
                        if (!onSide.ContainsKey(p.realname))
                            onSide.Add(p.realname, new Dictionary<string, Tuple<int, int>>());

                        foreach (Player p2 in g.players)
                        {
                            if (!onSide[p.realname].ContainsKey(p2.realname))
                                onSide[p.realname].Add(p2.realname, Tuple.Create(0, 0));

                            Tuple<int, int> t = onSide[p.realname][p2.realname];

                            onSide[p.realname][p2.realname] =
                                Tuple.Create(t.Item1 + ((p.side == p2.side) ? 1 : 0), t.Item2 + 1);
                        }
                    }

                }

            // write out side data to debug.
            if (false)
            {
                string[] pi = onSide.Keys.ToArray();

                Debug.Write(",");
                for (int i = 0; i < pi.Length; i++)
                    Debug.Write(pi[i] + ",");

                for (int y = -1; y < pi.Length; y++)
                {
                    for (int x = -1; x < pi.Length; x++)
                    {
                        if (y == -1)
                        {
                            if (x == -1)
                                Debug.Write(",");
                            else
                                Debug.Write(pi[x] + ",");
                        }
                        else
                        {
                            if (x == -1)
                                Debug.Write(pi[y] + ",");
                            else
                            {
                                if (onSide[pi[y]].ContainsKey(pi[x]))
                                {
                                    var t = onSide[pi[y]][pi[x]];
                                    Debug.Write((t.Item1 / (double)t.Item2) + ",");
                                }
                                else
                                    Debug.Write("0,");
                            }
                        }
                        if (x == pi.Length - 1)
                            Debug.WriteLine("");
                    }
                }
            }

            ec.Calc();

            return ec.playerInfo;
        }


        int PlayerTeam(int split, List<PlayerInfo> players, string playerName)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].name == playerName)
                {
                    if (((split >> i) & 1) == 0)
                        return 1;
                    else
                        return -1;
                }
            }

            return 0;
        }



        private bool BestOnSameTeam(int split, List<PlayerInfo> players, List<PlayerInfo> bestPlayers)
        {
            int t = 0;

            for (int i = 0; i < players.Count; i++)
            {
                if (bestPlayers.Contains(players[i]))
                {
                    if (((split >> i) & 1) == 0)
                        t++;
                    else
                        t--;
                }
            }

            return (t == 2) || (t == -2);
        }

        private void OnStartDateChanged(object sender, EventArgs e)
        {
            rankingPeriodStart = dateTimePicker.Value.AddDays(-1);
        }

        private void OnGraphSelected(object sender, EventArgs e)
        {
            RefreshGraph();
        }


        void RefreshGraph()
        {
            chart.Series.Clear();

            if (graphComboBox.SelectedIndex >= 0)
                graphs[graphComboBox.SelectedIndex].Render(chart, model, rankingAlgo);
        }


        private void OnStartDrag(object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Link);


        }

        private void OnDragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Link;
        }

        private void OnDragDrop(object sender, DragEventArgs e)
        {
            ListViewItem lvi = e.Data.GetData("System.Windows.Forms.ListViewItem") as ListViewItem;

            if (lvi == null)
                return;

            if (notPlayingView.Items.Contains(lvi))
                notPlayingView.Items.Remove(lvi);

            if (team1View.Items.Contains(lvi))
                team1View.Items.Remove(lvi);

            if (team2View.Items.Contains(lvi))
                team2View.Items.Remove(lvi);

            ListView lv = sender as ListView;

            lv.Items.Add(lvi.Text);
            UpdateTeamLabels();
        }

        private void UpdateTeamLabels()
        {
            int totalPlayers = team2View.Items.Count + team1View.Items.Count;
            Dictionary<string, PlayerInfo> allPlayersInfo = GetCurrentPlayerInfo();
            List<PlayerInfo>
                team1 = new List<PlayerInfo>(), team2 = new List<PlayerInfo>();



            for (int i = 0; i < team1View.Items.Count; i++)
                team1.Insert(0, allPlayersInfo[team1View.Items[i].Text]);

            for (int i = 0; i < team2View.Items.Count; i++)
                team2.Insert(0, allPlayersInfo[team2View.Items[i].Text]);

            // make teams even if they are not balanced, by inserting empty lanes
            if (team1View.Items.Count != team2View.Items.Count && team1View.Items.Count >= 1 && team2View.Items.Count >= 1 )
            {
                if (team1View.Items.Count < team2View.Items.Count)
                    team1.Add(allPlayersInfo[$"EmptyLane{(team1View.Items.Count + 1)}"]);
                else
                {
                    team2.Add(allPlayersInfo[$"EmptyLane{(team2View.Items.Count + 1)}"]);
                }
            }

            double pc = model.DefaultRankingAlgo().PercentageChance(team1, team2);

            team1WinLabel.Text = (pc * 100).ToString("f2") + "%";
            team2WinLabel.Text = ((1.0 - pc) * 100).ToString("f2") + "%";
        }



        void MakeGame(int winSide)
        {
            Game g = model.CreateGame();

            g.datetime = DateTime.Now;
            g.winSide = winSide;
            g.players = new List<Player>();

            int pid = 0;
            for (int i = 0; i < team1View.Items.Count; i++)
            {
                Player p = new Player();
                p.id = pid;
                pid++;
                p.side = 0;
                p.realname = team1View.Items[i].Text;

                if (model.IsRealPlayer(p.realname))
                    g.players.Add(p);
            }

            for (int i = 0; i < team2View.Items.Count; i++)
            {
                Player p = new Player();
                p.id = pid;
                pid++;
                p.side = 1;
                p.realname = team2View.Items[i].Text;
                if (model.IsRealPlayer(p.realname))
                    g.players.Add(p);
            }

            model.Store(g);
            model.games.Add(g);
            RefreshGamesList();
        }

        private void OnTeam2Win(object sender, EventArgs e)
        {
            MakeGame(2);
        }

        private void OnTeam1Win(object sender, EventArgs e)
        {
            MakeGame(1);
        }

        private void OnAutobalance(object sender, EventArgs e)
        {
            Dictionary<string, PlayerInfo> playersInfo = GetCurrentPlayerInfo();
            string biasedPlayer = biasComboBox.Text;
            bool favourPlayer = inFavourRadioButton.Checked;
            bool playerBalance = playerBalanceCheckBox.Checked;

            List<PlayerInfo> players = new List<PlayerInfo>();
            List<PlayerInfo> bestPlayers = new List<PlayerInfo>();

            // put all empty lanes back into the not playing list
            for (int i = team1View.Items.Count - 1; i >= 0; i--)
                if (team1View.Items[i].Text.StartsWith("EmptyLane"))
                {
                    notPlayingView.Items.Add(team1View.Items[i].Text);
                    team1View.Items.RemoveAt(i);
                }
            for (int i = team2View.Items.Count - 1; i >= 0; i--)
                if (team2View.Items[i].Text.StartsWith("EmptyLane"))
                {
                    notPlayingView.Items.Add(team2View.Items[i].Text);
                    team2View.Items.RemoveAt(i);
                }


            for (int i = 0; i < team1View.Items.Count; i++)
                players.Add(playersInfo[team1View.Items[i].Text]);
            for (int i = 0; i < team2View.Items.Count; i++)
                players.Add(playersInfo[team2View.Items[i].Text]);

            int totalPlayers = team2View.Items.Count + team1View.Items.Count;

            if (model.DefaultRankingAlgo().UseFakePlayers())
            {
                // if unbalanced, move an empty lane in.
                if ((totalPlayers % 2 == 1) && (totalPlayers > 1))
                {
                    string pl = "EmptyLane" + (totalPlayers / 2 + 1);
                    players.Add(playersInfo[pl]);

                    // remove form not playing
                    for (int i = 0; i < notPlayingView.Items.Count; i++)
                        if (notPlayingView.Items[i].Text == pl)
                            notPlayingView.Items.RemoveAt(i);
                }
            }

            // get the two best players
            {
                var sorted= playersInfo.OrderBy( p=> p.Value.score ).ToList();

                bestPlayers.Add(sorted.ElementAt(sorted.Count - 1).Value);
                bestPlayers.Add(sorted.ElementAt(sorted.Count - 2).Value);
            }

            int bestCombo = -1;
            double comboQuality = Double.MaxValue;
            double percChance = 0;

            List<int> okayCombos = new List<int>();

            if (rule2CheckBox.Checked)
                comboQuality = 0;

            for (int i = 0; i < (int)Math.Pow(2, players.Count); i++)
            {
                List<PlayerInfo> team1 = new List<PlayerInfo>(), team2 = new List<PlayerInfo>();

                for (int j = 0; j < players.Count; j++)
                {
                    if (((i >> j) & 1) == 0)
                        team1.Add(players[j]);
                    else
                        team2.Add(players[j]);
                }

                // don't allow unbalanced teams.
                if (playerBalanceCheckBox.Checked &&
                    ((team1.Count > (team2.Count + 1)) ||
                    (team2.Count > (team1.Count + 1))))
                    continue;


                double pc = model.DefaultRankingAlgo().PercentageChance(team1, team2);
                double ed = Math.Abs(pc - 0.5);


                // cant work out elo diff
                if (Double.IsNaN(ed))
                    continue;





                if (rule2CheckBox.Checked)
                {
                    // the bias rule, try to make the player on the most unbalanced game (ie elo difference really large)
                    int pt = PlayerTeam(i, players, biasedPlayer);

                    if (pt != 0)
                    {
                        if (favourPlayer)
                        {
                            if (((ed <= 0) == (pt < 0)) && (Math.Abs(ed) > Math.Abs(comboQuality)))
                            {
                                bestCombo = i;
                                comboQuality = ed;
                                percChance = pc;
                            }
                        }
                        else
                        {
                            if (((ed >= 0) == (pt < 0)) && (Math.Abs(ed) > Math.Abs(comboQuality)))
                            {
                                bestCombo = i;
                                comboQuality = ed;
                                percChance = pc;
                            }
                        }
                    }
                }
                else
                {
                    if ((Math.Abs(ed) < Math.Abs(comboQuality)) &&
                        (!rule1CheckBox.Checked || !BestOnSameTeam(i, players, bestPlayers)))
                    {
                        bestCombo = i;
                        comboQuality = ed;
                        percChance = pc;
                    }
                }

                double aed = Math.Abs(ed);

                if ((aed < 0.05) && (!rule1CheckBox.Checked || !BestOnSameTeam(i, players, bestPlayers)))
                    okayCombos.Add(i);
            }

            // okay, got a list of combos, pick the best in terms of player sides
            if (okayCombos.Count > 0)
            {
                int bc = okayCombos[0];
                int cq = Int32.MaxValue;

                foreach (int i in okayCombos)
                {
                    int q = 0;

                    for (int j = 0; j < players.Count; j++)
                    {
                        int ps = (((i >> j) & 1) == 0) ? 0 : 1;

                        for (int k = 0; k < players.Count; k++)
                        {
                            int ops = (((i >> k) & 1) == 0) ? 0 : 1;

                            if ((ops != ps) && players[j].onSide.ContainsKey(players[k].name))
                                q += players[j].onSide[players[k].name];
                        }
                    }

                    if (q < cq)
                    {
                        bc = i;
                        cq = q;
                    }
                }

                bestCombo = bc;
            }

            if (bestCombo == -1)
            {
                //				team1Label.Text = "No possible teams.";
                //				team2Label.Text = "";
            }
            else
            {
                team1View.Items.Clear();
                team2View.Items.Clear();

                for (int i = 0; i < players.Count; i++)
                {
                    if (((bestCombo >> i) & 1) == 0)
                        team1View.Items.Add(players[i].name);
                    else
                        team2View.Items.Add(players[i].name);
                }
            }


            // dump the counts of who has played against who.
            /*{
                string ln = ",";
                for (int j = 0; j < players.Count; j++)
                    ln += players[j].name + ",";

                Debug.WriteLine(ln);

                for (int i = 0; i < players.Count; i++)
                {
                    ln = players[i].name + ",";

                    for (int j = 0; j < players.Count; j++)
                    {
                        if (players[j].onSide.ContainsKey(players[i].name))
                            ln += players[j].onSide[players[i].name] + ",";
                        else
                            ln += "0,";
                    }
                    Debug.WriteLine(ln);
                }
            } */

            UpdateTeamLabels();
        }

        private void OnDeleteGame(object sender, EventArgs e)
        {
            Game g = SelectedGame();

            model.DeleteGame(g.id);

            RefreshGamesList();
        }

        private void OnEloSelected(object sender, EventArgs e)
        {
            model.defaultRankingAlgo = model.algos[0];
        }

        private void OnPairSelected(object sender, EventArgs e)
        {
            model.defaultRankingAlgo = model.algos[1];
        }

		private void OnPlayerCellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
            Player p= playersGrid.Rows[e.RowIndex].Tag as Player;
			string name = (string)playersGrid[namePlayerColumn.Index, e.RowIndex].Value ?? "";
			string initialRankStr = (string)playersGrid[initialRankPlayerColumn.Index, e.RowIndex].Value ?? "";
            int initialRank = -1;
            
            if( e.ColumnIndex == initialRankPlayerColumn.Index )
            {
                if (!int.TryParse(initialRankStr, out initialRank))
                {
                    playersGrid[initialRankPlayerColumn.Index, e.RowIndex].Style.ForeColor = Color.Red;
                    MessageBox.Show("Initial rank has to be a number");
                    return;
                }
                else
                    playersGrid[initialRankPlayerColumn.Index, e.RowIndex].Style.ForeColor = Color.Black;
			}


            if (p != null)
            {
                if (name != p.realname || initialRank != p.initialRank)
                {
                    // new pe
                    model.UpdatePlayer( name, initialRank );
                }

            }
            else
            {
                // no player for this row, ergo we are entering a new player
                if (name != "" && initialRank != -1)
                {
                    // make sure player name is not a duplicate
                    if (model.players.Any(x => x.realname == name))
                    {
						MessageBox.Show("That player already exists");
						return;
					}

                    model.UpdatePlayer( name, initialRank );
                }
            }
		}
	}
}
