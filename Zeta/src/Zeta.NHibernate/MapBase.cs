using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentNHibernate.Mapping;

using Zeta.Core;

namespace Zeta.NHibernate
{
    public abstract class MapBase<T> : ClassMap<T>
        where T: IModel
    {
        protected MapBase()
            : base()
        {
            Id(x => x.Id).GeneratedBy.GuidComb();
        }
    }
}
