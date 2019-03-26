using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.SessionState;
using B_R.Models;
using NHibernate;
using NHibernate.Linq;
using ISession = NHibernate.ISession;

namespace B_R
{
    public class MvcApplication : System.Web.HttpApplication
    {

        protected void Session_End(Object sender, EventArgs e)
        {

        }

 

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }



        //    Application["usuarios"] = 0;

        //    using (ISession session = NHibernateHelper.OpenSession())
        //    {
        //        using (ITransaction transacao = session.BeginTransaction())
        //        {
        //            try
        //            {
        //                //session.Delete(area);
        //                transacao.Commit();
        //            }
        //            catch (Exception ex)
        //            {
        //                if (!transacao.WasCommitted)
        //                {
        //                    transacao.Rollback();
        //                }
        //                throw new Exception("Erro ao Deletar Cliente : " + ex.Message);
        //            }
        //        }
        //    }
    }

}
