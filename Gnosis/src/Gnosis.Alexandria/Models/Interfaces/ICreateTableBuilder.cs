using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Interfaces
{
    public interface ICreateTableBuilder : ICommandBuilder
    {
        ICreateTableBuilder CreateTable(string name);
        ICreateTableBuilder CreateTempTable(string name);
        ICreateTableBuilder IfNotExists { get; }
        ICreateTableBuilder As(ICommand select);
        
        ICreateTableBuilder PrimaryKeyColumn(string name);
        ICreateTableBuilder TextColumn(string name);
        ICreateTableBuilder NumericColumn(string name);
        ICreateTableBuilder RealColumn(string name);
        ICreateTableBuilder BlobColumn(string name);
        
        ICreateTableBuilder NotNull { get; }
        ICreateTableBuilder Default(object value);
        ICreateTableBuilder Unique { get; }
        ICreateTableBuilder Check(string expression);
        ICreateTableBuilder ForeignKey { get; }
        ICreateTableBuilder ForeignKeyColumn(string column);
        ICreateTableBuilder References(string table);
        ICreateTableBuilder ReferenceColumn(string column);

        ICreateTableBuilder AddParameter(string name, object value);
    }
}
