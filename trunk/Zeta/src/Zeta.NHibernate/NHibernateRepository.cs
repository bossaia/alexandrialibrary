using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using Zeta.Core;

using Hibernate = NHibernate;
using NHibernate.Linq;
using FluentNHibernate;

namespace Zeta.NHibernate
{
    public class NHibernateRepository : IRepository
    {
        private readonly Hibernate.ISession _session;

        //public NHibernateRepository(ISessionSource source)
            //: this(source.CreateSession())
        //{
        //}


        public NHibernateRepository() //ISession session)
        {
            _session = null; //session;
            _session.FlushMode = Hibernate.FlushMode.Commit;
        }

        #region IRepository Members

        private void withinTransaction(Action action)
        {
            Hibernate.ITransaction transaction = _session.BeginTransaction();
            try
            {
                action();
                transaction.Commit();
            }
            catch (Exception)
            {
                // Do something here?
                transaction.Rollback();
                throw;
            }
            finally
            {
                transaction.Dispose();
            }
        }

        public T Find<T>(Guid id) where T : IModel
        {
            return _session.Get<T>(id);
        }

        public void Delete<T>(T target)
        {
            withinTransaction(() => _session.Delete(target));
        }

        public T[] Query<T>(Expression<System.Func<T, bool>> where)
        {
            return _session.Linq<T>().Where(where).ToArray();
        }

        #endregion

        public void Save<T>(T target)
        {
            withinTransaction(() => _session.SaveOrUpdate(target));
        }

        public T FindBy<T, U>(Expression<System.Func<T, U>> expression, U search) where T : class
        {
            string propertyName = ReflectionHelper.GetProperty(expression).Name;
            var criteria = _session.CreateCriteria(typeof(T)).Add(Hibernate.Criterion.Restrictions.Eq(propertyName, search));
            return criteria.UniqueResult() as T;
        }

        public T FindBy<T>(Expression<System.Func<T, bool>> where)
        {
            return _session.Linq<T>().First(where);
        }
    }
}
