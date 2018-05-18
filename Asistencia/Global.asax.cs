using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using Asistencia.Clases;
using Asistencia.Servidor;
using log4net.Config;

namespace Asistencia
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            XmlConfigurator.Configure();

            HangfireBootstrapper.Instance.Start();

        }
        protected void Application_End(object sender, EventArgs e)
        {
            HangfireBootstrapper.Instance.Stop();
        }
        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started  
            if (Session[Constantes.WEB_VARIABLE_SESSION_USUARIO] != null)
            {
                //Redirect to Welcome Page if Session is not null  
                Response.Redirect(Constantes.WEB_PAGINA_SISTEMA);

            }
            else
            {
                //Redirect to Login Page if Session is null & Expires   
                Response.Redirect(Constantes.WEB_PAGINA_INICIO_SESION);

            }


        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends.   
            // Note: The Session_End event is raised only when the sessionstate mode  
            // is set to InProc in the Web.config file. If session mode is set to StateServer   
            // or SQLServer, the event is not raised.  

        }
    }
}