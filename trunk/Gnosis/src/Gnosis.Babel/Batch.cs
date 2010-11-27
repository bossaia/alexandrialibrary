using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel
{
    public class Batch : IBatch
    {
        private readonly IDictionary<Guid, ICommand> _commands = new Dictionary<Guid, ICommand>();

        public IEnumerable<ICommand> Commands
        {
            get { throw new NotImplementedException(); }
        }

        public void AddCommand(ICommand command)
        {
            throw new NotImplementedException();
        }

        public void AddCallback(Guid id, Action<IBatch, object> callback)
        {
            throw new NotImplementedException();
        }

        public void InvokeCallback(Guid id, object value)
        {
            throw new NotImplementedException();
        }

        public ICommand GetCommand(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
