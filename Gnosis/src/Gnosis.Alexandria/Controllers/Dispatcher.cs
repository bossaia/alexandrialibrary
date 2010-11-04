using System;
using System.Collections.Generic;
using System.Linq;

namespace Gnosis.Alexandria.Controllers
{
    public abstract class Dispatcher : IDispatcher
    {
        protected Dispatcher()
        {
        }

        protected Dispatcher(IDispatcher parent)
        {
            _parent = parent;
        }

        private readonly Guid _id = Guid.NewGuid();
        private readonly IDispatcher _parent;
        private readonly IDictionary<Guid,IDispatcher> _children = new Dictionary<Guid, IDispatcher>();

        protected abstract void MessageReceived<T>(T message)
            where T : IMessage;

        public Guid Id
        {
            get { return _id; }
        }

        public IDispatcher Parent
        {
            get { return _parent; }
        }

        public IEnumerable<IDispatcher> Children
        {
            get { return _children.Values; }
        }

        public void AddChild(IDispatcher child)
        {
            if (child == null)
                throw new ArgumentNullException("child");

            _children.Add(child.Id, child);
        }

        public void RemoveChild(Guid childId)
        {
            if (_children.ContainsKey(childId))
                _children.Remove(childId);
        }

        public void Dispatch<T>(Guid sender, T message)
            where T : IMessage
        {
            if ((object)message == null)
                throw new ArgumentNullException("message");

            MessageReceived(message);

            if (_parent != null && _parent.Id != sender)
                _parent.Dispatch(_id, message);

            foreach (var child in _children.Values.Where(x => x.Id != sender))
                child.Dispatch(_id, message);
        }
    }
}
