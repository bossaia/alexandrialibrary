using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Interfaces
{
    public interface ICreateViewCommandBuilder
    {
        ICreateViewCommandBuilder CreateView(string name);
        ICreateViewCommandBuilder CreateTempView(string name);
        ICreateViewCommandBuilder IfNotExists { get; }
        ICreateViewCommandBuilder As(ICommand select);

        ICommand ToCommand();
    }
}
