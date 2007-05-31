using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Mono.Data.SqliteClient;
using Alexandria;

namespace Alexandria.SQLite
{
	public class SQLiteDataProvider
	{
		public string Test()
		{
			string data = string.Empty;
		
			try
			{
				System.Data.IDbConnection cnn;
				string dbDir = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\Alexandria\\";
				if (!System.IO.Directory.Exists(dbDir))
					System.IO.Directory.CreateDirectory(dbDir);
				string dbPath = dbDir + "Alexandria.db";
				
				using(cnn = new Mono.Data.SqliteClient.SqliteConnection("URI=file:" + dbPath)) //C:\Dev\Data\SQLite\test.db"))
				{
					cnn.Open();
					
					IDbCommand createTable = cnn.CreateCommand();
					createTable.CommandText = "CREATE TABLE audiotrack ( Id TEXT PRIMARY KEY, IdType TEXT NOT NULL, Name TEXT NOT NULL, Artist TEXT NOT NULL, Album TEXT NOT NULL, Duration INTEGER NOT NULL, ReleaseDate INTEGER NOT NULL, TrackNumber INTEGER NOT NULL, Format TEXT NOT NULL)";
					createTable.ExecuteNonQuery();
					
					IDbCommand insertData = cnn.CreateCommand();
					insertData.CommandText = "INSERT INTO audiotrack (Id, IdType, Name, Artist, Album, Duration, ReleaseDate, TrackNumber, Format) VALUES ('5dc9742c-c68b-4534-b0f4-91b341ad09d3', 'MusicBrainzID', 'H.', 'Tool', 'Ænima', 369000, 1996, 3, 'ogg')";
					insertData.ExecuteNonQuery();
					
					IDbCommand selectData = cnn.CreateCommand();
					selectData.CommandText = "SELECT * FROM audiotrack";
					IDataReader reader = selectData.ExecuteReader();
					while(reader.Read())
					{
						data += "Id=" + reader.GetString(0) + " Name=" + reader.GetString(2) + "\n";
					}
					
					cnn.Close();
				}
			}
			catch (Exception ex)
			{
				throw new AlexandriaException("SQLite Error: " + ex.Message, ex);
			}
			
			return data;
		}
	}
}
