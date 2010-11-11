using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Gnosis.Alexandria.Models.Interfaces
{
    public interface ICreateTableBuilder : ICommandBuilder
    {
        ICreateTableBuilder CreateTable(string name);
        ICreateTableBuilder CreateTempTable(string name);
        ICreateTableBuilder IfNotExists { get; }
        ICreateTableBuilder As(ICommand select);

        ICreateTableBuilder Column<T>(Expression<Func<T, object>> expression, T model) where T : IModel;
        ICreateTableBuilder Column(string name, string type);
        ICreateTableBuilder Column(string name, string type, object defaultValue);
        ICreateTableBuilder Columns<T>(IEnumerable<Expression<Func<T, object>>> expressions, T model) where T : IModel;

        ICreateTableBuilder PrimaryKey<T>(Expression<Func<T, object>> expression) where T : IModel;
        ICreateTableBuilder PrimaryKey(string column);
        ICreateTableBuilder Check(string expression);
        ICreateTableBuilder ForeignKey { get; }
        ICreateTableBuilder ForeignKeyColumn(string column);
        ICreateTableBuilder References(string table);
        ICreateTableBuilder ReferenceColumn(string column);

        ICreateTableBuilder AddParameter(string name, object value);
    }
}
