using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Interfaces
{
    public interface ICreateTriggerBuilder : ICommandBuilder
    {
        ICreateTriggerBuilder CreateTempTrigger(string name);
        ICreateTriggerBuilder CreateTrigger(string name);
        ICreateTriggerBuilder IfNotExists { get; }

        ICreateTriggerBuilder BeforeInsertOn(string table);
        ICreateTriggerBuilder BeforeUpdateOn(string table);
        ICreateTriggerBuilder UpdatedColumn(string column);
        ICreateTriggerBuilder BeforeDeleteOn(string table);

        ICreateTriggerBuilder When();
    }
}
