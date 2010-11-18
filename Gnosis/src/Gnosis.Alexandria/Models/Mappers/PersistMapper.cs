using System.Linq;
using Gnosis.Alexandria.Models.Interfaces;
using Gnosis.Babel;
using Gnosis.Babel.SQLite.Persist;

namespace Gnosis.Alexandria.Models.Mappers
{
    public class PersistMapper<T> : IPersistMapper<T>
        where T : IModel
    {
        public PersistMapper(ISchema<T> schema, IFactory<IInsert> insertFactory, IFactory<IUpdate> updateFactory, IFactory<IDelete> deleteFactory)
        {
            Schema = schema;
            InsertFactory = insertFactory;
            UpdateFactory = updateFactory;
            DeleteFactory = deleteFactory;
        }

        #region Protected Members

        protected readonly ISchema<T> Schema;
        protected readonly IFactory<IInsert> InsertFactory;
        protected readonly IFactory<IUpdate> UpdateFactory;
        protected readonly IFactory<IDelete> DeleteFactory;

        protected virtual ICommand GetInsertCommand(T model)
        {
            //return InsertFactory.Create()
            //    .Insert
            //    .OrFail
            //    .Into(Schema.Name)
            //    .ColumnsToValues<T>(Schema.NonPrimaryFields.Select(x => x.Getter), model)
            //    .SetCallback<T>((x, y) => x.Initialize(y), model)
            //    .ToCommand();

            return null;
        }

        protected virtual ICommand GetUpdateCommand(T model)
        {
            //return UpdateFactory.Create()
            //    .Update
            //    .OrFail
            //    .Table(Schema.Name)
            //    .ColumnsToValues(Schema.NonPrimaryFields.Select(x => x.Getter), model)
            //    .Where<T>(x => x.Id).IsEqualTo<T>(x => x.Id, model.Id)
            //    .ToCommand();

            return null;
        }

        protected virtual ICommand GetDeleteCommand(T model)
        {
            //return DeleteFactory.Create()
            //    .Delete
            //    .OrFail
            //    .From(Schema.Name)
            //        .Where<T>(x => x.Id)
            //        .IsEqualTo<T>(x => x.Id, model.Id)
            //    .ToCommand();

            return null;
        }

        #endregion

        public ICommand GetPersistCommand(T model)
        {
            if (model.IsDeleted)
                return GetDeleteCommand(model);
            
            return (model.IsNew) ?
                GetInsertCommand(model) :
                GetUpdateCommand(model);
        }
    }
}
