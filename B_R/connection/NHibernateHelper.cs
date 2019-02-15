using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace B_R.Models
{
    public class NHibernateHelper
    {
        // @"Server=postgres://znitpwbb:nDmzcRlUqQ6yWQv6OLKNg-IBgIn1esDo@pellefant.db.elephantsql.com:5432/znitpwbb;  Port=5432;User Id = znitpwbb;Password=nDmzcRlUqQ6yWQv6OLKNg-IBgIn1esDo;    Database=BR"
        // private static string ConnectionString = "Server=localhost; Port=5432; User Id=postgres; Password=root; Database=BR";

        private static ISessionFactory session;
       // private static string urlLocal = "Server=pellefant.db.elephantsql.com;  Port=5432; User Id=znitpwbb; Password=nDmzcRlUqQ6yWQv6OLKNg-IBgIn1esDo; Database=znitpwbb";

       // private static string urlServidor = "Server=localhost; Port=5433;User Id=postgres; Database=znitpwbb";



        public static ISession OpenSession()
        {
            var appSettings = ConfigurationManager.AppSettings;
            string result = appSettings["AlterTable"] ?? "Not Found";
            bool alter = Boolean.Parse(result);
            if (session != null)
                return session.OpenSession();

            session = Fluently.Configure()
                .Database(PostgreSQLConfiguration.PostgreSQL82
                  .ConnectionString(c=>c.FromAppSetting
                  ("MainConnectionString")).ShowSql()
                )
               .Mappings(m => m.FluentMappings.AddFromAssemblyOf<RespSolicitacao>())
               .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ComentariosArea>())
               .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ComentariosEquipamento>())
               .Mappings(m => m.FluentMappings.AddFromAssemblyOf<User>())
               .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Area>())
               .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Equipamento>())
               .Mappings(m => m.FluentMappings.AddFromAssemblyOf<LogArea>())
               .Mappings(m => m.FluentMappings.AddFromAssemblyOf<LogEquipamento>())
               .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Solicitacao>())
               .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, alter))
                /* .Execute(false, true) - DESENVOLVIMENTO*/
                /* .Execute(false, false) - PRODUÇÃO*/
                .BuildSessionFactory();
            return session.OpenSession();
        }


    }
}