using System.Data.SQLite;

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


		public Model()
		{
			games= new List<Game>();
			playerEvents = new List<PlayerEvent>();
			players = new List<Player>();

			algos = new List<RankingAlgorithm>();

			algos.Add(new EloRankingAlgorithm());
			algos.Add(new PairRankingAlgorithm());

			defaultRankingAlgo = algos[0];

			dbConnection = new SQLiteConnection();
		}

		public void Start()
		{
			const string dbPath = "ranker.sqlite";

			dbConnection.ConnectionString= $"data source={dbPath};";
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


		public void LoadPlayerEvents()
		{
			playerEvents.Clear();
			players.Clear();

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
			Player? p = players.Find( x=> x.realname== name );

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

			{
				PlayerEvent pe = new PlayerEvent();
				pe.date= DateTime.Now;
				pe.name = name;
				pe.initialRank = initialRank;
				playerEvents.Add(pe);
			}
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

		public void DbNonQuery(SQLiteTransaction? trans, string sql)
		{
			SQLiteCommand command = new SQLiteCommand(sql, dbConnection, trans);
			command.ExecuteNonQuery();
		}
	}
}
