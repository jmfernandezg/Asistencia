using Asistencia.Clases;
using Hangfire;
using Hangfire.SqlServer;
using log4net;
using System;
using System.Configuration;
using System.Web.Hosting;

namespace Asistencia.Servidor
{
    public class HangfireBootstrapper : IRegisteredObject
    {
        // Logger
        public static readonly ILog logger = LogManager.GetLogger(Constantes.LOG_ASISTENCIA_APP);
        public static readonly HangfireBootstrapper Instance = new HangfireBootstrapper();

        private readonly object _lockObject = new object();
        private bool _started;

        private BackgroundJobServer _backgroundJobServer;

        private HangfireBootstrapper()
        {
        }

        public void Start()
        {
            logger.Warn("PROCESO AUTOMATICO: Esta instancia esta INICIANDO el Servicio de Ejecucion Automatica de los procesos");

            lock (_lockObject)
            {
                if (_started) return;
                _started = true;

                HostingEnvironment.RegisterObject(this);

                GlobalConfiguration.Configuration.UseSqlServerStorage(Constantes.CONFIG_DATABASE_CONNECTION_NAME, new SqlServerStorageOptions { QueuePollInterval = TimeSpan.FromSeconds(1) });

                if (Boolean.Parse(ConfigurationManager.AppSettings[Constantes.CONFIG_PROCESO_COLECTOR_INICIAR]))
                {
                    RecurringJob.AddOrUpdate(() => Tareas.ProcesoColector.iniciarColeccion(), ConfigurationManager.AppSettings[Constantes.CONFIG_PROCESO_COLECTOR_INTERVALO]);
                    logger.Info("PROCESO AUTOMATICO: Esta instancia inicia correctamente el proceso de coleccion de controles de acceso");
                }
                else
                {
                    RecurringJob.RemoveIfExists("ProcesoColector.iniciarColeccion");
                    logger.Warn("PROCESO AUTOMATICO: Esta instancia no inicia el proceso de coleccion de controles de acceso");
                }

                if (Boolean.Parse(ConfigurationManager.AppSettings[Constantes.CONFIG_PROCESO_SERVICIO_WEB_INICIAR]))
                {
                    RecurringJob.AddOrUpdate(() => Tareas.ProcesoWebService.iniciarEjecuciondeServicioWeb(), ConfigurationManager.AppSettings[Constantes.CONFIG_PROCESO_SERVICIO_WEB_INTERVALO]);
                    logger.Info("PROCESO AUTOMATICO: Esta instancia inicia correctamente el proceso de insertado al servicio web");
                }
                else
                {
                    RecurringJob.RemoveIfExists("ProcesoWebService.iniciarEjecuciondeServicioWeb");
                    logger.Warn("PROCESO AUTOMATICO: Esta instancia no inicia el proceso de insertado al servicio web");
                }

                _backgroundJobServer = new BackgroundJobServer();
            }
        }

        public void Stop()
        {
            logger.Warn("PROCESO AUTOMATICO: Esta instancia esta DETENIENDO el Servicio de Ejecucion Automatica de los procesos");

            lock (_lockObject)
            {
                if (_backgroundJobServer != null)
                {
                    _backgroundJobServer.Dispose();
                }

                HostingEnvironment.UnregisterObject(this);
            }
        }

        void IRegisteredObject.Stop(bool immediate)
        {
            Stop();
        }
    }
}