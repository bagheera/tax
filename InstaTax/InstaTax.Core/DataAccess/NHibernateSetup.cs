using System.Collections.Generic;
using System.Data;
using System.IO;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace InstaTax.Core
{
    public class NHibernateSetup
    {
        protected static ISessionFactory SessionFactory;
        protected static Configuration Configuration;
        protected ISession Session  { get; set;}

        public NHibernateSetup(FileInfo[] nHibernateMappingFiles)
        {
            InitalizeSessionFactory(nHibernateMappingFiles);
            Session = CreateSession();
        }

        public static void InitalizeSessionFactory(params FileInfo[] hbmFiles)
        {
            if (SessionFactory != null)
                return;

            var properties = new Dictionary<string, string>
                                 {
                                     {"connection.driver_class", "NHibernate.Driver.SQLite20Driver, NHibernate"},
                                     {"dialect", "NHibernate.Dialect.SQLiteDialect, NHibernate"},
                                     {"connection.provider", "NHibernate.Connection.DriverConnectionProvider, NHibernate"},
                                     //{"connection.provider", "", "System.Data.SQLite"},
                                     {"connection.connection_string", "Data Source=:memory:;Version=3;New=True;"},
                                     {"connection.release_mode", "on_close"},
                                     {"show_sql", "true"}
                                 };

            Configuration = new Configuration {Properties = properties};

            foreach (FileInfo mappingFile in hbmFiles)
            {
                Configuration = Configuration.AddFile(mappingFile);
            }
            Configuration.BuildMapping();
            SessionFactory = Configuration.BuildSessionFactory();
        }

        public ISession CreateSession()
        {
            ISession openSession = SessionFactory.OpenSession();
            IDbConnection connection = openSession.Connection;
            new SchemaExport(Configuration).Execute(false, true, false, true, connection, null);
            return openSession;
        }

    }
}
