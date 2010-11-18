using System.Data;
using Gnosis.Alexandria.Models.Interfaces;
using Gnosis.Alexandria.Utilities;
using Gnosis.Babel;

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

            model.Initialize(record[_schema.PrimaryField.Name]);

            _schema.Fields.Each(x => x.Setter(model, record[x.Name]));

            return model;
        }
    }
}
