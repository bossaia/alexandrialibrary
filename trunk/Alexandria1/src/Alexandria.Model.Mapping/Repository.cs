using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Telesophy.Alexandria.Core;
using Telesophy.Alexandria.Persistence;

namespace Telesophy.Alexandria.Model.Mapping
{
    public class Repository : DataProvider
    {
        public Repository() : base()
        {
        }

        //public T GetById<T>(Guid id)
        //    where T: IEntity
        //{
        //    return base.GetById<T>(id);
        //}

        //public IList<T> GetList<T>(string queryString)
        //    where T: IEntity
        //{
        //    return base.GetListBase<T>(queryString);
        //}

        //public void Save(IEntity entity)
        //{
        //    base.Save(entity);
        //}

        //public void Delete(IEntity entity)
        //{
        //    base.Delete(entity);
        //}
    }
}
