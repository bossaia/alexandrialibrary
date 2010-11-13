using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Interfaces;

namespace Gnosis.Alexandria.Models.Mappers
{
    public class QueryMapper<T> : IQueryMapper<T>
        where T : IModel
    {
        public QueryMapper(ISchema<T> schema, IFactory<ISelectBuilder> factory)
        {
            Schema = schema;
            Factory = factory;
        }

        #region Protected Members

        protected readonly ISchema<T> Schema;
        protected readonly IFactory<ISelectBuilder> Factory;

        protected ISelectBuilder GetSelectAllBuilder()
        {
            return Factory.Create()
                .SelectDistinct
                .AllColumns
                .From(Schema.Name);
        }

        #endregion

        #region IQueryMapper Members

        public ICommand GetSelectOneCommand(object id)
        {
            return GetSelectAllBuilder()
                .Where<T>(x => x.Id)
                .IsEqualTo<T>(x => x.Id, id)
                .ToCommand();
        }

        public ICommand GetSelectAllCommand()
        {
            return GetSelectAllBuilder()
                .ToCommand();
        }

        #endregion
    }
}
