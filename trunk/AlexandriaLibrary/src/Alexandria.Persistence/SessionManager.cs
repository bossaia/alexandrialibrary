using System.Configuration;
using System.Reflection;
using FluentNHibernate;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using Configuration = NHibernate.Cfg.Configuration;

namespace Telesophy.Alexandria.Persistence
{
    public class SessionManager
    {
        private const string KEY_MAPPING_ASSEMBLY = "NHibernate.Mapping.Assembly";
        private readonly string _mappingAssembly;
        private readonly ISessionFactory _sessionFactory;

        public SessionManager()
        {
            _mappingAssembly = ConfigurationManager.AppSettings[KEY_MAPPING_ASSEMBLY];
            _sessionFactory = GetSessionFactory();
        }

        public ISession GetSession()
        {
            return _sessionFactory.OpenSession();
        }

        private ISessionFactory GetSessionFactory()
        {
            Configuration cfg = new Configuration().Configure();
            
            //bool useFluent = false;

            //if (useFluent)
            //{
                var persistenceModel = new PersistenceModel();
                persistenceModel.addMappingsFromAssembly(Assembly.Load(_mappingAssembly));
                persistenceModel.Configure(cfg);
            //}
            //else
            //{
                cfg.AddAssembly(Assembly.Load(_mappingAssembly));
            //}

            return cfg.BuildSessionFactory();
        }

        internal static void ExportSchema()
        {
            Configuration cfg = new Configuration().Configure();
            var persistenceModel = new PersistenceModel();
            persistenceModel.addMappingsFromAssembly(
                Assembly.Load(ConfigurationManager.AppSettings[KEY_MAPPING_ASSEMBLY]));
            persistenceModel.Configure(cfg);
            new SchemaExport(cfg).Create(true, true);
        }
    }
}
