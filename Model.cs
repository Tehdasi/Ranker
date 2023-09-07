using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace Ranker
{
	public class Model
	{
		SQLiteConnection dbConnection;
		public List<Game> games;
		public List<Player> players;
		public List<PlayerEvent> playerEvents;
		public List<RankingAlgorithm> algos;
		public RankingAlgorithm defaultRankingAlgo;

		public const int retirementDays = 50;

		public void Start()
		{
			const string dbPath = "ranker.sqlite";

			games = new List<Game>();
			dbConnection = new SQLiteConnection($"data source={dbPath};");
			dbConnection.Open();


			// see if the player table exists, if not, the db is blank, so we'll need to populate it with a schema
			if( !DbBoolQuery("select count(*) from sqlite_master where type='table' and name='player'") )
			{
				DbNonQuery(null, @"
					CREATE TABLE game  ( 
						id INTEGER PRIMARY KEY NOT NULL,
						winner INTEGER NULL,
						time DATETIME NULL,
						ignore BOOL DEFAULT 'FALSE' NOT NULL,
						playdate DATETIME NULL);

					CREATE TABLE player (
						id INTEGER  NULL,
						gameid INTEGER  NULL,
						side INTEGER  NULL,
						realname TEXT  NULL );

					CREATE TABLE playerEvent (
						id INTEGER  PRIMARY KEY AUTOINCREMENT NOT NULL,
						date DATETIME  NOT NULL,
						initialRank INTEGER  NULL,
						name TEXT  NULL );");
			}

			algos = new List<RankingAlgorithm>();

			algos.Add(new EloRankingAlgorithm());
			algos.Add(new PairRankingAlgorithm());

			defaultRankingAlgo = algos[0];
		}

		public bool IsRealPlayer( string name )
		{
			return !name.StartsWith("EmptyLane");
		}

		public RankingAlgorithm DefaultRankingAlgo()
		{
			return defaultRankingAlgo;
		}


		public void DeleteGame( long id )
		{
			DbNonQuery(null, 
				"delete from game where id= " + id + "; " +
				"delete from player where gameid= " + id + ";"	);

			for (int i = 0; i < games.Count; i++)
			{
				if (games[i].id == id)
				{
					games.RemoveAt(i);
					i++;
				}
			}
		}

		public void End()
		{
			dbConnection.Close();
		}

		public void AddNameToLookup(string name, string player)
		{
			DbNonQuery(null, "insert or replace into namelookup (name, player) values ('" + SQLString(name) + "','" + SQLString(player) + "');");
		}

		Dictionary<string,PlayerInfo> EloFromGames( SortedList<DateTime,Game> games, List<GameInfo> gameInfos, bool squaredSkill )
		{
			int plrEvent = 0;

			Averager avg = new Averager(10);
			int usedGames = 0;
			Dictionary<string, PlayerInfo> playerInfo = new Dictionary<string, PlayerInfo>();

			foreach (Game g in games.Values)
			{

				while ((plrEvent < (playerEvents.Count - 1)) && playerEvents[plrEvent].date < g.datetime)
				{
					PlayerEvent pe = playerEvents[plrEvent];

					if (pe.add)
					{
						PlayerInfo pi = new PlayerInfo();
						pi.score = pe.initialRank;
						pi.games = 0;
						pi.name = pe.name;
						playerInfo.Add(pe.name, pi);
					}
					else
						playerInfo.Remove(pe.name);

					plrEvent++;
				}

				double[] telo = new double[2];
				double[] ts = new double[2];
				double[] etelo = new double[2];
				double[] delta = new double[2];

				telo[0] = 0;
				telo[1] = 0;
				ts[0] = 0;
				ts[1] = 0;

				foreach (Player p in g.players)
				{
					double elo = playerInfo[p.realname].score;
					if (squaredSkill)
						telo[p.side] += elo * elo;
					else
						telo[p.side] += elo;

					ts[p.side]++;

					playerInfo[p.realname].games++;
				}

				double maxSize = Math.Max(ts[0], ts[1]);
				double ad= 0.5;

				telo[0] *= (maxSize + ad) / (ts[0] + ad);
				telo[1] *= (maxSize + ad) / (ts[1] + ad);

				etelo[0] = 1.0 / (1 + Math.Pow(10, (telo[1] - telo[0]) / 400.0));
				etelo[1] = 1.0 / (1 + Math.Pow(10, (telo[0] - telo[1]) / 400.0));


				delta[0] = 32 * (((g.WinSide() == 0) ? 1 : 0) - etelo[0]) * ts[1] / maxSize;
				delta[1] = 32 * (((g.WinSide() == 1) ? 1 : 0) - etelo[1]) * ts[0] / maxSize;


				avg.Add( Math.Abs(((g.WinSide() == 0) ? 1 : 0) - etelo[0]) );

				usedGames++;

//				Debug.WriteLine("");
//				Debug.WriteLine(g.filename);
//				Debug.WriteLine(etelo[0]);

				//foreach (Player p in g.players)
				//    Debug.WriteLine(SideName((int)p.side) + ", " + p.name + ", " + p.realname + ", " + playerInfo[p.realname].elo );

				//Debug.WriteLine("team elo: " + telo[0] + "," + telo[1]);
				//Debug.WriteLine("win chance: " + etelo[0] + "," + etelo[1]);
				//Debug.WriteLine(SideName(g.WinSide()) + " wins!");
				//Debug.WriteLine("sentinel delta: " + delta[0]);
				//Debug.WriteLine("scourge delta: " + delta[1]);
//				Debug.WriteLine(avg.Average());

				GameInfo gi = new GameInfo();
				gi.game = g;
				gi.sentinelPoints = delta[0];
				gi.scourgePoints = delta[1];
				gi.winChanceSentinel = etelo[0];
				gi.winChanceScourge = etelo[1];
				gi.preGameScore = new Dictionary<string, double>();
				gi.postGameScore = new Dictionary<string, double>();


				foreach (Player p in g.players)
					gi.preGameScore.Add(p.realname, playerInfo[p.realname].score);

				foreach (Player p in g.players)
					playerInfo[p.realname].score += delta[p.side];

				foreach (Player p in g.players)
				gi.postGameScore.Add(p.realname, playerInfo[p.realname].score);

				if( gameInfos!= null )
					gameInfos.Add(gi);
			}

			return playerInfo;
		}

		public void LoadPlayerEvents()
		{
			playerEvents = new List<PlayerEvent>();
			players = new List<Player>();

			SQLiteCommand comm = new SQLiteCommand("select id, date, initialRank, name from playerEvent order by date", dbConnection);
			SQLiteDataReader reader = comm.ExecuteReader();

			while (reader.Read())
			{
				PlayerEvent pe= new PlayerEvent();

				pe.id = reader.GetInt64(0);
				pe.date = reader.GetDateTime(1);
				pe.initialRank = reader.GetInt32(2);
				pe.name = reader.GetString(3);

				playerEvents.Add( pe );

				var p = players.Find(p2 => p2.realname == pe.name);

				if (p == null)
				{
					p = new Player();
					p.fake = false;
					p.realname = pe.name;
					players.Add(p);
				}

				p.initialRank = pe.initialRank;

			}
			reader.Close();


			for (int i = 2; i <= 6; i++)
			{
				Player p= new Player();
				p.realname = $"EmptyLane{i}";
				p.fake = true;
				players.Add(p);
			}
		}

		public RankingCalculator GetCalculator()
		{
			RankingCalculator ec= defaultRankingAlgo.GetCalculator();
			ec.SetPlayerEvents(playerEvents);
			return ec;
		}

		public void UpdatePlayer(string name, int initialRank)
		{
			Player p = players.Find( x=> x.realname== name );

			if (p == null)
			{
				p = new Player();
				p.fake = false;
				p.realname = name;
				p.initialRank = initialRank;
				players.Add(p);
			}

			p.realname = name;
			p.initialRank = initialRank;

			DbNonQuery(null, $"insert into playerevent (date,initialRank,name) values (datetime('now','localtime'),{p.initialRank},'{p.realname}')");
		}

		public List<string> ActivePlayers(DateTime date)
		{
			List<string> ap = new List<string>();


			for (int i = 0; i < playerEvents.Count; i++)
			{
				PlayerEvent pe = playerEvents[i];

				if (pe.date < date)
					ap.Add(pe.name);
			}

			ap.Add("EmptyLane2");
			ap.Add("EmptyLane3");
			ap.Add("EmptyLane4");
			ap.Add("EmptyLane5");
			ap.Add("EmptyLane6");

			ap.Sort();

			return ap;
		}


		public Dictionary<string, PlayerInfo> GenOldElos( DateTime endPeriod )
		{
			RankingCalculator ec = new RankingCalculator();

			foreach (Game g in games)
				if (g.IsRanked() && (g.datetime <= endPeriod) )
					ec.AddGame(g);

			ec.SetPlayerEvents(playerEvents);

			return ec.Calc();
		}

		string SQLString(string s)
		{
			return s.Replace("'", "''");
		}



		public string SideName( int side )
		{
			switch( side )
			{
			case 0:
				return "Sentinel";
			case 1:
				return "Scourge";
			default:
				return "unknown";
			}
		}
		
		public void StoreLookup()
		{
		}



		public bool DbBoolQuery(string sql)
		{
			SQLiteCommand command = new SQLiteCommand(dbConnection);
			command.CommandText = sql;
			SQLiteDataReader reader = command.ExecuteReader();
			reader.Read();

			return reader.GetBoolean(0);
		}

		public void ScrubDb()
		{
			DbNonQuery(null, "delete from game; delete from player");
		}

		public void Store(Game g)
		{
			SQLiteTransaction trans= dbConnection.BeginTransaction();

			if (g.id == -1)
			{
				g.id = DbLongQuery("select coalesce(max( id )+1,0) from game", 0);
			}
			else
				DbNonQuery(trans, "delete from player where gameid= " + g.id + ";" );

			DbNonQuery(trans, "insert or replace into game ( id, winner, playdate ) " + 
				" values ( " +g.id+ "," + + g.winSide + ",'" + g.datetime.ToString("s") + "' )");
			//yyyy-MM-dd hh:mm:ss

			for( int i= 0; i< g.players.Count; i++ )
			{
				Player p= g.players[i];

				DbNonQuery(trans, "insert or replace into player ( id, gameid, side, realname )" +
					"values ("+ p.id + "," + g.id + "," + p.side + ",'" + SQLString( p.realname ) +"')" );
			}

			trans.Commit();
		}

        public void HackyThing()
        {
            dbConnection.Close();

            dbConnection = new SQLiteConnection("data source=" + "D:\\data\\coding.bak\\ranker5\\ranker.sqlite" + ";");
            dbConnection.Open();

            RestoreGames();


            dbConnection.Close();

            dbConnection = new SQLiteConnection("data source=" + "D:\\data\\coding\\ranker\\ranker.sqlite" + ";");
            dbConnection.Open();

            foreach (Game g in games)
                Store(g);

        }


        public void RestoreGames()
		{
			Dictionary<  long, Game > gameById= new Dictionary<long, Game>();
			
			SQLiteCommand comm = new SQLiteCommand( "select id, winner, ignore, playdate from game", dbConnection );

			SQLiteDataReader reader= comm.ExecuteReader();

			while (reader.Read())
			{
				Game g = new Game();

				g.id = reader.GetInt64(0);
				g.winSide = reader.GetInt32(1);
				g.datetime = reader.GetDateTime(3);



				games.Add(g);
				gameById.Add( g.id, g );
			}
			reader.Close();
			
			
			comm = new SQLiteCommand( "select id, gameid, realname, side from player", dbConnection );

			reader= comm.ExecuteReader();

			while (reader.Read())
			{
				Player p= new Player();

                if (gameById.ContainsKey(reader.GetInt64(1)))
                {
                    Game g = gameById[reader.GetInt64(1)];

                    g.players.Add(p);

                    p.id = reader.GetInt64(0);
                    if (reader.IsDBNull(2))
                        p.realname = "";
                    else
                        p.realname = reader.GetString(2);
                    p.side = reader.GetInt64(3);
                }
			}
			
			reader.Close();
		}

		public void RestoreNameLookup()
		{
		}

		public Game CreateGame()
		{
			Game g = new Game();

			g.id= -1;

			return g;
		}


		public long DbLongQuery(string sql, long defaultValue)
		{
			SQLiteCommand command = new SQLiteCommand(dbConnection);
			command.CommandText = sql;
			SQLiteDataReader reader = command.ExecuteReader();

			if (reader.HasRows)
			{
				reader.Read();
				if (reader.IsDBNull(0))
					return defaultValue;
				else
					return reader.GetInt64(0);
			}
			else
				return defaultValue;
		}

		public void DbNonQuery(SQLiteTransaction trans, string sql)
		{
			SQLiteCommand command = new SQLiteCommand(sql, dbConnection, trans);
			command.ExecuteNonQuery();
		}
	}
}
