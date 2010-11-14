using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Interfaces;

namespace Gnosis.Alexandria.Models.Commands
{
    public class CreateTableBuilder : CommandBuilder, ICreateTableBuilder
    {
        public CreateTableBuilder(IFactory<ICommand> factory)
            : base(factory)
        {
            AddSpaceDelimitedClause(Clauses.Create);
        }

        public ICreateTableBuilder CreateTable(string name)
        {
            throw new NotImplementedException();
        }

        public ICreateTableBuilder CreateTempTable(string name)
        {
            throw new NotImplementedException();
        }

        public ICreateTableBuilder IfNotExists
        {
            get { throw new NotImplementedException(); }
        }

        public ICreateTableBuilder As(ICommand select)
        {
            throw new NotImplementedException();
        }

        public ICreateTableBuilder Column<T>(System.Linq.Expressions.Expression<Func<T, object>> expression, T model) where T : IModel
        {
            throw new NotImplementedException();
        }

        public ICreateTableBuilder Column(string name, string type)
        {
            throw new NotImplementedException();
        }

        public ICreateTableBuilder Column(string name, string type, object defaultValue)
        {
            throw new NotImplementedException();
        }

        public ICreateTableBuilder Columns<T>(IEnumerable<System.Linq.Expressions.Expression<Func<T, object>>> expressions, T model) where T : IModel
        {
            throw new NotImplementedException();
        }

        public ICreateTableBuilder PrimaryKey<T>(System.Linq.Expressions.Expression<Func<T, object>> expression) where T : IModel
        {
            throw new NotImplementedException();
        }

        public ICreateTableBuilder PrimaryKey(string column)
        {
            throw new NotImplementedException();
        }

        public ICreateTableBuilder Check(string expression)
        {
            throw new NotImplementedException();
        }

        public ICreateTableBuilder ForeignKey
        {
            get { throw new NotImplementedException(); }
        }

        public ICreateTableBuilder ForeignKeyColumn(string column)
        {
            throw new NotImplementedException();
        }

        public ICreateTableBuilder References(string table)
        {
            throw new NotImplementedException();
        }

        public ICreateTableBuilder ReferenceColumn(string column)
        {
            throw new NotImplementedException();
        }

        public ICreateTableBuilder AddParameter(string name, object value)
        {
            throw new NotImplementedException();
        }
    }
}
