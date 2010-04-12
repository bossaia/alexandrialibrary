using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Babel.Relational
{
    public class Database :
        IDatabase
    {
        public Database(string name, ISet<IDomain> domains, ISet<ITable> tables, IEngine engine)
        {
            _name = name;
            _domains = domains;
            _tables = tables;
            _engine = engine;
        }

        private string _name;
        private ISet<IDomain> _domains;
        private ISet<ITable> _tables;

        private IEngine _engine;

        #region IDatabase Members

        public string Name
        {
            get { return _name; }
        }

        public ISet<IDomain> Domains
        {
            get { return _domains; }
        }

        public ISet<ITable> Tables
        {
            get { return _tables; }
        }

        public void Initialize()
        {
            using (IDbTransaction transaction = _engine.GetTransaction())
            {
                try
                {
                    foreach(ITable table in _tables)
                    {
                        using (IDbCommand command = _engine.GetCreateTableCommand(table))
                        {
                            command.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                }
                catch
                {
                    if (transaction != null)
                        transaction.Rollback();

                    throw;
                }
            }
        }

        private static IDictionary<string, object> GetDictionary(IQuery query)
        {
            IDictionary<string, object> items = new Dictionary<string, object>();

            foreach (IColumn column in query.Selection)
                items.Add(column.Name, null);

            return items;
        }

        public IEnumerable<IMap<string, object>> Read(IQuery query)
        {
            IList<IMap<string, object>> results = new List<IMap<string, object>>();

            using (IDbCommand command = _engine.GetSelectCommand(query))
            {
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        IDictionary<string, object> row = GetDictionary(query);
                        foreach (IColumn column in query.Selection)
                        {
                            row[column.Name] = reader[column.Name];
                        }

                        results.Add(new Map<string, object>(row));
                    }
                }
            }

            return results;
        }

        public void Execute(ITuple<ICommand> commands)
        {
            using (IDbTransaction transaction = _engine.GetTransaction())
            {
                try
                {
                    foreach (ICommand command in commands)
                    {
                        IDbCommand change = _engine.GetChangeCommand(command);
                        command.SetResult(change.ExecuteScalar());
                    }

                    transaction.Commit();
                }
                catch
                {
                    if (transaction != null)
                        transaction.Rollback();

                    throw;
                }
            }
        }

        #endregion
    }
}
