using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Alexandria.Persistence
{
	public interface IPersistenceEngine
	{
		IDbConnection GetConnection();
		IDataReader GetDataReader(string commandText);
		void CreateTable(DataTable table);
		void FillRow(DataRow row, Guid id);
		void SaveRow(DataRow row);
		void DeleteRow(DataRow row);
	}
}
