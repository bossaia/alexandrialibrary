using System;
using System.Configuration;
using System.Reflection;
//using FluentNHibernate;
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
			try
			{
				ISession session = _sessionFactory.OpenSession();
				session.CacheMode = CacheMode.Ignore;
				
				return session;
			}
			catch (Exception ex)
			{
				throw ex;
			}
        }

        private ISessionFactory GetSessionFactory()
        {
            Configuration cfg = new Configuration().Configure();
            
            //bool useFluent = false;

            //if (useFluent)
            //{
                //var persistenceModel = new PersistenceModel();
                //persistenceModel.addMappingsFromAssembly(Assembly.Load(_mappingAssembly));
                //persistenceModel.Configure(cfg);
            //}
            //else
            //{
                cfg.AddAssembly(Assembly.Load(_mappingAssembly));
				//cfg.AddAssembly(Assembly.Load("Alexandria.Model"));
            //}

            return cfg.BuildSessionFactory();
        }

        internal static void ExportSchema()
        {
			//NOTE: This is needed for Fluent NHibernate
			//Configuration cfg = new Configuration().Configure();
			//var persistenceModel = new PersistenceModel();
			//persistenceModel.addMappingsFromAssembly(
			//    Assembly.Load(ConfigurationManager.AppSettings[KEY_MAPPING_ASSEMBLY]));
			//persistenceModel.Configure(cfg);
			//new SchemaExport(cfg).Create(true, true);
        }
    }
}
