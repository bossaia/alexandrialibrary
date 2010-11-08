using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Gnosis.Alexandria.Controllers;
using Gnosis.Alexandria.Messages;
using Gnosis.Alexandria.Messages.Interfaces;
using Gnosis.Alexandria.Models;
using Gnosis.Alexandria.Models.Commands;
using ICmd = Gnosis.Alexandria.Models.Interfaces.ICommand;
using Gnosis.Alexandria.Models.Factories;
using Gnosis.Alexandria.Models.Interfaces;
using Gnosis.Alexandria.Models.Mappers;
using Gnosis.Alexandria.Models.Repositories;

namespace Gnosis.Alexandria.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window, IDispatcher, IProcessor
    {
        public MainView()
        {
            InitializeComponent();

            //TODO: Replace this with StructureMap
            var commandFactory = new GenericFactory<ICmd, Command>();
            var commandBuilderFactory = new CommandBuilderFactory(commandFactory);
            var countryFactory = new GenericFactory<ICountry, Country>();
            var countryModelMapper = new CountryModelMapper();
            var countryCommandMapper = new CountryCommandMapper(commandBuilderFactory);
            var countryRepository = new CountryRepository(countryFactory, countryModelMapper, countryCommandMapper);
            var artistFactory = new GenericFactory<IArtist, Artist>();
            var artistModelMapper = new ArtistModelMapper(countryRepository);
            var artistCommandMapper = new ArtistCommandMapper(commandBuilderFactory);
            var artistRepository = new ArtistRepository(artistFactory, artistModelMapper, artistCommandMapper);

            AddChild(new TabController(this, tabControl));
            AddChild(new RepositoryController(this, artistRepository));

            DoAddTabClick();
        }

        private readonly Guid _id = Guid.NewGuid();
        private readonly IDispatcher _parent = null;
        private readonly IDictionary<Guid, IDispatcher> _children = new Dictionary<Guid, IDispatcher>();

        #region IDispatcher Members

        public Guid Id
        {
            get { return _id; }
        }

        new public IDispatcher Parent
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

        #endregion

        #region IProcessor Members

        private readonly IDictionary<Type, IList<IHandler>> _handlerMap = new Dictionary<Type, IList<IHandler>>();

        protected virtual void MessageReceived<T>(T message)
            where T : IMessage
        {
            Process<T>(message);
        }

        protected virtual void BeforeProcessing<T>(T message)
            where T : IMessage
        {
        }

        protected virtual void AfterProcessing<T>(T message)
            where T : IMessage
        {
        }

        public bool HasHandler(IHandler handler)
        {
            if (handler == null)
                throw new ArgumentNullException("handler");

            return (_handlerMap.ContainsKey(handler.Type))
                       ? _handlerMap[handler.Type].Contains(handler)
                       : false;
        }

        public void AddHandler(IHandler handler)
        {
            if (handler == null)
                throw new ArgumentNullException("handler");

            if (!_handlerMap.ContainsKey(handler.Type))
                _handlerMap[handler.Type] = new List<IHandler>();

            _handlerMap[handler.Type].Add(handler);
        }

        public void RemoveHandler(IHandler handler)
        {
            if (handler == null)
                throw new ArgumentNullException("handler");

            if (_handlerMap.ContainsKey(handler.Type) && _handlerMap[handler.Type].Contains(handler))
                _handlerMap[handler.Type].Remove(handler);
        }

        public void Process<T>(T message)
            where T : IMessage
        {
            if ((object)message == null)
                throw new ArgumentNullException("message");

            BeforeProcessing(message);

            if (_handlerMap.ContainsKey(typeof(T)))
            {
                foreach (var handler in _handlerMap[typeof(T)].Cast<IHandler<T>>())
                    handler.Handle(message);
            }

            AfterProcessing(message);
        }

        #endregion

        private void DoAddTabClick()
        {
            Dispatch<INewHomeTabRequestedMessage>(_id, new NewHomeTabRequestedMessage());
        }

        private void btnAddTab_Click(object sender, RoutedEventArgs e)
        {
            DoAddTabClick();
        }
    }
}
