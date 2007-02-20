using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Mono.Data.SqliteClient;
using AlexandriaOrg.Alexandria;

namespace AlexandriaOrg.Alexandria.SQLite
{
	public class SQLiteDataProvider
	{
		public string Test()
		{
			string data = string.Empty;
		
			try
			{
				System.Data.IDbConnection cnn;
				using(cnn = new Mono.Data.SqliteClient.SqliteConnection(@"URI=file:C:\Dev\Data\SQLite\test.db"))
				{
					cnn.Open();
					
					IDbCommand createTable = cnn.CreateCommand();
					createTable.CommandText = "CREATE TABLE artist ( ArtistId TEXT PRIMARY KEY, Name TEXT NOT NULL, DateOfBirth INTEGER)";
					createTable.ExecuteNonQuery();
					
					IDbCommand insertData = cnn.CreateCommand();
					insertData.CommandText = "INSERT INTO artist (ArtistId, Name, DateOfBirth) VALUES ('AAAA', 'David Bowie', 19470108)";
					insertData.ExecuteNonQuery();
					
					IDbCommand selectData = cnn.CreateCommand();
					selectData.CommandText = "SELECT * FROM artist";
					IDataReader reader = selectData.ExecuteReader();
					while(reader.Read())
					{
						data += "Id=" + reader.GetString(0) + " Name=" + reader.GetString(1) + "\n";
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
