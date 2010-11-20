using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.SQLite.Schema
{
    #region Create

    public class Create : Statement, ICreate
    {
        public IIndex Index(string name)
        {
            throw new NotImplementedException();
        }

        public IIndex IndexIfNotExists(string name)
        {
            throw new NotImplementedException();
        }

        public IIndex UniqueIndex(string name)
        {
            throw new NotImplementedException();
        }

        public IIndex UniqueIndexIfNotExists(string name)
        {
            throw new NotImplementedException();
        }

        public ITable Table(string name)
        {
            throw new NotImplementedException();
        }

        public ITable TableIfNotExists(string name)
        {
            throw new NotImplementedException();
        }

        public ITable TempTable(string name)
        {
            throw new NotImplementedException();
        }

        public ITable TempTableIfNotExists(string name)
        {
            throw new NotImplementedException();
        }

        public IView TempView(string name)
        {
            throw new NotImplementedException();
        }

        public IView TempViewIfNotExists(string name)
        {
            throw new NotImplementedException();
        }

        public ITrigger Trigger(string name)
        {
            throw new NotImplementedException();
        }

        public ITrigger TriggerIfNotExists(string name)
        {
            throw new NotImplementedException();
        }

        public ITrigger TempTrigger(string name)
        {
            throw new NotImplementedException();
        }

        public ITrigger TempTriggerIfNotExists(string name)
        {
            throw new NotImplementedException();
        }

        public IView View(string name)
        {
            throw new NotImplementedException();
        }

        public IView ViewIfNotExists(string name)
        {
            throw new NotImplementedException();
        }
    }

    #endregion

    #region Create<T>

    public class Create<T> : Statement, ICreate<T>
    {
        public IIndex<T> Index(string name)
        {
            throw new NotImplementedException();
        }

        public IIndex<T> IndexIfNotExists(string name)
        {
            throw new NotImplementedException();
        }

        public IIndex<T> UniqueIndex(string name)
        {
            throw new NotImplementedException();
        }

        public IIndex<T> UniqueIndexIfNotExists(string name)
        {
            throw new NotImplementedException();
        }

        public ITable<T> Table(string name)
        {
            throw new NotImplementedException();
        }

        public ITable<T> TableIfNotExists(string name)
        {
            throw new NotImplementedException();
        }

        public ITable<T> TempTable(string name)
        {
            throw new NotImplementedException();
        }

        public ITable<T> TempTableIfNotExists(string name)
        {
            throw new NotImplementedException();
        }

        public ITrigger<T> Trigger(string name)
        {
            throw new NotImplementedException();
        }

        public ITrigger<T> TriggerIfNotExists(string name)
        {
            throw new NotImplementedException();
        }

        public ITrigger<T> TempTrigger(string name)
        {
            throw new NotImplementedException();
        }

        public ITrigger<T> TempTriggerIfNotExists(string name)
        {
            throw new NotImplementedException();
        }

        public IView<T> View(string name)
        {
            throw new NotImplementedException();
        }

        public IView<T> ViewIfNotExists(string name)
        {
            throw new NotImplementedException();
        }

        public IView<T> TempView(string name)
        {
            throw new NotImplementedException();
        }

        public IView<T> TempViewIfNotExists(string name)
        {
            throw new NotImplementedException();
        }
    }

    #endregion
}
