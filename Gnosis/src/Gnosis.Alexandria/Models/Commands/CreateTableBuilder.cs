using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using Gnosis.Alexandria.Models.Interfaces;

namespace Gnosis.Alexandria.Models.Commands
{
    public class CreateTableBuilder : CommandBuilder, ICreateTableBuilder
    {
        public CreateTableBuilder(IFactory<ICommand> factory)
            : base(factory)
        {
            AddKeywordClause(Clauses.Create);
            AddListClause(Clauses.Columns);
        }

        public ICreateTableBuilder CreateTable
        {
            get
            {
                SetCurrentClause(Clauses.Create);
                Append(Constants.Create, Constants.Table);
                return this;
            }
        }

        public ICreateTableBuilder CreateTempTable
        {
            get
            {
                SetCurrentClause(Clauses.Create);
                Append(Constants.Create, Constants.Temp, Constants.Table);
                return this;
            }
        }

        public ICreateTableBuilder IfNotExists
        {
            get
            { 
                Append(Constants.IfNotExists);
                return this;
            }
        }

        public ICreateTableBuilder Name(string name)
        {
            Append(name);
            return this;
        }

        public ICreateTableBuilder As(ICommand select)
        {
            throw new NotImplementedException();
        }

        public ICreateTableBuilder Column<T>(Expression<Func<T, object>> expression, T model) where T : IModel
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

        public ICreateTableBuilder Columns<T>(IEnumerable<Expression<Func<T, object>>> expressions, T model) where T : IModel
        {
            foreach (var expression in expressions)
                Column<T>(expression, model);

            return this;
        }

        public ICreateTableBuilder PrimaryKey<T>(Expression<Func<T, object>> expression, T model) where T : IModel
        {
            throw new NotImplementedException();
        }

        public ICreateTableBuilder PrimaryKey(string name, string type)
        {
            SetCurrentClause(Clauses.Columns);
            Append(name, type, Constants.NotNull, Constants.PrimaryKey);
            return this;
        }

        public ICreateTableBuilder Check(string expression)
        {
            Append(Constants.Check, Constants.OpenParen, expression, Constants.CloseParen);
            return this;
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
