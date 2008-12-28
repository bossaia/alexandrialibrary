using System;
using System.Xml;
using FluentNHibernate;
using FluentNHibernate.Mapping;
using Telesophy.Alexandria.Core;

namespace Telesophy.Alexandria.Persistence
{
    [CLSCompliant(false)]
    public abstract class MapBase<T> : ClassMap<T>, IMapGenerator
        where T: IEntity
    {
        public MapBase() : base()
        {
            Id(x => x.Id).GeneratedBy.GuidComb();
        }

        #region IMapGenerator Members

        public virtual XmlDocument Generate()
        {
            return CreateMapping(new MappingVisitor());
        }

        #endregion
    }
}
