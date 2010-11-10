using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Interfaces
{
    public interface ICreateTableCommandBuilder
    {
        ICreateTableCommandBuilder CreateTable(string name);
        ICreateTableCommandBuilder CreateTempTable(string name);
        ICreateTableCommandBuilder IfNotExists { get; }
        ICreateTableCommandBuilder As(ICommand select);
        
        ICreateTableCommandBuilder PrimaryKeyColumn(string name);
        ICreateTableCommandBuilder TextColumn(string name);
        ICreateTableCommandBuilder NumericColumn(string name);
        ICreateTableCommandBuilder RealColumn(string name);
        ICreateTableCommandBuilder BlobColumn(string name);
        
        ICreateTableCommandBuilder NotNull { get; }
        ICreateTableCommandBuilder Default(object value);
        ICreateTableCommandBuilder Unique { get; }
        ICreateTableCommandBuilder Check(string expression);
        ICreateTableCommandBuilder ForeignKey { get; }
        ICreateTableCommandBuilder ForeignKeyColumn(string column);
        ICreateTableCommandBuilder References(string table);
        ICreateTableCommandBuilder ReferenceColumn(string column);

        ICommand ToCommand();
    }
}
