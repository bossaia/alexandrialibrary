using System;
using System.Collections.Generic;
using NHibernate;

namespace Telesophy.Alexandria.Persistence
{
    public abstract class DataProvider
    {
        private SessionManager manager;
        private ISession session;

        protected DataProvider()
        {
            manager = new SessionManager();
            session = manager.GetSession();
        }

        protected T GetById<T>(object id)
        {
            return session.Get<T>(id);
        }

        protected IList<T> GetListBase<T>(string queryString)
        {
            IQuery query = session.CreateQuery(queryString);
            return query.List<T>();
        }

        protected void Save(object entity)
        {
            session.SaveOrUpdate(entity);
        }

        protected void Delete(object entity)
        {
            session.Delete(entity);
        }

        protected void Flush()
        {
            session.Flush();
        }

        //public string Save(Cat item)
        //{
        //   return _session.Save(item) as string;
        //}

        //public Cat GetById(string id)
        //{
        //    _session.Flush();
        //    return _session.Get<Cat>(id);
        //}
    }
}
