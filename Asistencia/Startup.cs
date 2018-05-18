using Microsoft.Owin;
using Owin;
using Hangfire;
using Asistencia.Clases;
using System.Configuration;
using Asistencia.Servidor;

[assembly: OwinStartupAttribute(typeof(Asistencia.Startup))]
namespace Asistencia
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseHangfireDashboard(ConfigurationManager.AppSettings[Constantes.CONFIG_WEB_TABLERO_PROCESOS], new DashboardOptions
            {
                Authorization =  new[] { new AuthorizationFilter() }
            });
            app.UseHangfireServer();

        }
    }
}
