using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Views.Interfaces;

namespace Gnosis.Alexandria.Controllers.Interfaces
{
    public interface ITabController : IController
    {
        void AddTab(ITabView view);
        void RemoveTab(Guid id);
        void SelectTab(Guid id);
    }
}
