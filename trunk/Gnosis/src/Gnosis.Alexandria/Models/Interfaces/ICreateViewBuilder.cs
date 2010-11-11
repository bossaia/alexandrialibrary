using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Interfaces
{
    public interface ICreateViewBuilder
    {
        ICreateViewBuilder CreateView(string name);
        ICreateViewBuilder CreateTempView(string name);
        ICreateViewBuilder IfNotExists { get; }
        ICreateViewBuilder As(ICommand select);

        ICreateViewBuilder AddParameter(string name, object value);

        ICommand ToCommand();
    }
}
