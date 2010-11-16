using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Interfaces;
using Gnosis.Alexandria.Utilities;

namespace Gnosis.Alexandria.Models.Mappers
{
    public class ModelMapper<T> : IModelMapper<T>
        where T : IModel
    {
        public ModelMapper(IFactory<T> factory, ISchema<T> schema)
        {
            _factory = factory;
            _schema = schema;
        }

        private readonly IFactory<T> _factory;
        private readonly ISchema<T> _schema;

        public object GetId(IDataRecord record)
        {
            return record[_schema.PrimaryField.Name];
        }

        public T GetModel(IDataRecord record)
        {
            var model = _factory.Create();
            _schema.Fields
                .Each(x => x.Setter(model, record[x.Name]));

            return model;
        }
    }
}
