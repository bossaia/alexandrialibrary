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
            session.FlushMode = FlushMode.Commit;
        }

        public T GetById<T>(object id)
        {
            //using (ISession session = manager.GetSession())
            //{
				//session.Close();

				T record = default(T);

				
				//ITransaction tx = session.BeginTransaction();

				record = (T)session.Get<T>(id, LockMode.None);
					//session.Get(typeof(T), id); //.Get<T>(id);
				//session.Flush();

				Telesophy.Alexandria.Model.MediaSet set = record as Telesophy.Alexandria.Model.MediaSet;
				if (set != null)
				{
				    NHibernateUtil.Initialize(set.Items);
				    int x = set.Items.Count;
				}
            
				//session.Flush();
				//tx.Commit();
				//s.Close();

				

				return record;
            //}
        }

        public virtual IList<T> GetList<T>(string queryString)
        {
            //using (ISession session = manager.GetSession())
            //{
			try
			{
				IList<T> list = new List<T>();
				IQuery query = session.CreateQuery(queryString);
				list = query.List<T>();
				//NHibernateUtil.Initialize(list);
				return list;
			}
			catch (Exception ex)
			{
				throw ex;
			}

            //}
        }

        public virtual void Save(object entity)
        {
            //using (ISession session = manager.GetSession())
            //{
                //session.FlushMode = FlushMode.Commit;

                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.SaveOrUpdate(entity);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            //}
        }

        public virtual void Delete(object entity)
        {
            //using (ISession session = manager.GetSession())
            //{
                //session.FlushMode = FlushMode.Commit;

                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.Delete(entity);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            //}
        }

        //protected void Flush()
        //{
        //    session.Flush();
        //}

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
