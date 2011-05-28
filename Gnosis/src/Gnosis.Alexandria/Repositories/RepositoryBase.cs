﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;

using log4net;

using Gnosis.Core;
using Gnosis.Core.Attributes;
using Gnosis.Core.Batches;
using Gnosis.Core.Commands;
using Gnosis.Core.Queries;

//using Gnosis.Alexandria.Models;

namespace Gnosis.Alexandria.Repositories
{
    public abstract class RepositoryBase<T>
        : IRepository<T>
        where T : IEntity
    {
        protected RepositoryBase(IContext context)
        {
            this.context = context;
            this.baseType = typeof(T);
        }

        private static readonly ILog log = LogManager.GetLogger(typeof(RepositoryBase<T>));
        private readonly IContext context;
        private readonly Type baseType;
        private readonly IList<ILookup> lookups = new List<ILookup>();
        private readonly IList<ISearch> searches = new List<ISearch>();
        private string database = "Catalog.db";

        protected void Initialize()
        {
            try
            {
                var batch = new InitializeTypeBatch(() => GetConnection(), baseType, lookups, searches);
                batch.Execute();
            }
            catch (Exception ex)
            {
                log.Error("RepositoryBase.Initialize", ex);
            }
        }

        protected abstract IEnumerable<T> Read(IDataReader reader);
        
        protected IContext Context
        {
            get { return context; }
        }

        protected void AddLookup(ILookup lookup)
        {
            lookups.Add(lookup);
        }

        protected void AddSearch(ISearch search)
        {
            searches.Add(search);
        }

        protected IDbConnection GetConnection()
        {
            return new SQLiteConnection(string.Format("Data Source={0};Version=3;", database));
        }

        //protected ITimeStamp GetTimeStamp(IDataReader reader)
        //{
        //    var createdBy = new Uri(reader["TimeStamp_CreatedBy"].ToString());
        //    var createdDate = DateTime.Parse(reader["TimeStamp_CreatedDate"].ToString());
        //    var lastAccessedBy = new Uri(reader["TimeStamp_LastAccessedBy"].ToString());
        //    var lastAccessedDate = DateTime.Parse(reader["TimeStamp_LastAccessedDate"].ToString());
        //    var lastModifiedBy = new Uri(reader["TimeStamp_LastModifiedBy"].ToString());
        //    var lastModifiedDate = DateTime.Parse(reader["TimeStamp_LastModifiedDate"].ToString());

        //    return new TimeStamp(createdBy, createdDate, lastAccessedBy, lastAccessedDate, lastModifiedBy, lastModifiedDate);
        //}

        //protected int Execute(CommandBuilder commandBuilder)
        //{
        //    using (var connection = GetConnection())
        //    {
        //        connection.Open();
        //        using (var command = commandBuilder.GetCommand(connection))
        //        {
        //            return command.ExecuteNonQuery();
        //        }
        //    }
        //}

        protected IEnumerable<T> Select(IFilter filter)
        {
            var query = new Query<T>(() => GetConnection(), filter);
            return query.Execute();
        }

        public T Lookup(Guid id)
        {
            var whereClause = string.Format("{0}.Id = @Id", baseType.GetTableName());
            var parameters = new Dictionary<string, object> { { "@Id", id } };
            return Select(new Filter(whereClause, parameters)).FirstOrDefault();
        }

        public T Lookup(ILookup lookup, IEnumerable<KeyValuePair<string, object>> parameters)
        {
            return Select(lookup.GetFilter(parameters)).FirstOrDefault();
        }

        public IEnumerable<T> Search()
        {
            var search = searches.Where(x => x.IsDefault == true).FirstOrDefault();
            return Select(search.GetFilter());
        }

        public IEnumerable<T> Search(ISearch search, IEnumerable<KeyValuePair<string, object>> parameters)
        {
            return Select(search.GetFilter(parameters));
        }

        public void Save(IEnumerable<T> items)
        {
            var batch = new SaveEntitiesBatch<T>(() => GetConnection(), items);
            batch.Execute();
        }

        public void Delete(IEnumerable<T> items)
        {
            var batch = new DeleteEntitiesBatch<T>(() => GetConnection(), items);
            batch.Execute();
        }
    }
}
