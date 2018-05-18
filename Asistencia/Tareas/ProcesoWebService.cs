using Asistencia.HuellaDactilar;
using Asistencia.Clases;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Xml;
using System.Xml.Serialization;

namespace Asistencia.Tareas
{
    public class ProcesoWebService : IDisposable
    {
        // Logger
        public static readonly ILog logger = LogManager.GetLogger(Constantes.LOG_ASISTENCIA_TAREA_SERVICIO_WEB);

        // Fabrica de Objetos de Acceso a Datos (DAO)
        public static readonly IDaoFactory daoFactory = new NHibernateDaoFactory();
        public static readonly System.Xml.Serialization.XmlSerializer xsSubmit = new XmlSerializer(typeof(DT_RegistroHuellaData[]));
        public static void iniciarEjecuciondeServicioWeb()
        {
            IIncidenciaDao incidenciaDao = daoFactory.GetIncidenciaDao();

            logger.Info("Ejecucion automatica de la Tarea de Proceso Servicio Web");
            List<Incidencia> lista = null;


            try
            {
                lista = incidenciaDao.GetListado(0);
            }
            catch (Exception ex)
            {
                logger.Error(String.Format("Error al intentar obtener la lista de incidencias. Mensaje [{0}]", ex.Message));
                logger.Error(ex);
            }

            /**
            if (lista != null)
            {
                logger.Info(String.Format("Conteo de Lista de Incidencias [{0}]", lista.Count));
            }
            else {
                logger.Info("Lista de Incidencias Nula");
            }**/


            if (lista != null && lista.Count > 0)
            {
                try
                {
                    ServicePointManager.ServerCertificateValidationCallback = (obj, certificate, chain, errors) => true;
                    SI_OA_RegistroHuellaDactilarClient cliente = new SI_OA_RegistroHuellaDactilarClient();

                    cliente.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings[Constantes.CONFIG_PROCESO_SERVICIO_WEB_USUARIO];
                    cliente.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings[Constantes.CONFIG_PROCESO_SERVICIO_WEB_PASSWD];
                    logger.Info(String.Format("Se han encontrado [{0}] incidencias nuevas, ejecutando el servicio web por lista", lista.Count));

                    foreach (Incidencia incidencia in lista)
                    {

                        String eventTypeID = "E";
                        switch (incidencia.InOutMode)
                        {
                            case 0: eventTypeID = "E"; break;
                            case 1: eventTypeID = "S"; break;
                            case 2: eventTypeID = "S"; break;
                            case 3: eventTypeID = "E"; break;
                            case 4: eventTypeID = "E"; break;
                            case 5: eventTypeID = "S"; break;
                        }

                        DT_RegistroHuellaData datos = new DT_RegistroHuellaData();
                        datos.DAY_EVENT = "1";
                        datos.DRIVE_ID = incidencia.Empleado.NoEmpleado.ToString();
                        datos.EVENT_ID = incidencia.CveIncidencia.ToString();
                        datos.EVENT_TYPE_ID = "HD";
                        datos.MESSGE_TYPE = eventTypeID;
                        datos.ORIG_TIME = incidencia.FechaHoraIncidencia.ToString("yyyy-MM-dd HH:mm:ss");
                        datos.PLANT_ID = incidencia.ControlAcceso.Oficina.CodigoPlanta;
                        datos.RECEIVER_ID = "RMS";
                        datos.VENDOR_ID = "1";

                        try
                        {
                            DT_RegistroHuellaData[] wsin = new DT_RegistroHuellaData[] { datos };

                            String xml;
                            using (StringWriter sww = new StringWriter())
                            using (XmlWriter writer = XmlWriter.Create(sww))
                            {
                                xsSubmit.Serialize(writer, wsin);
                                xml = sww.ToString(); // Your XML
                            }

                            logger.Info(String.Format("Enviando al Servicio Web la Incidencia  [{0}]", xml));
                            // Enviamos los datos al servicio Web
                            cliente.SI_OA_RegistroHuellaDactilar(wsin);


                            // Guardamos la Incidencia Como enviada al servicio Web
                            incidencia.EnviadoWs = 1;
                            Incidencia up = incidenciaDao.SaveOrUpdate(incidencia);

                        }
                        catch (Exception ex)
                        {

                            logger.Info(String.Format("Error al Intentar Enviar la Incidencia Con ID [{0}]. Mensaje: [{1}]", incidencia.CveIncidencia, ex.Message));
                            logger.Error(ex);
                        }

                    }
                }
                catch (Exception ex)
                {
                    logger.Info(String.Format("Error al Intentar Crear el Cliente de Servicio Web. Mensaje: [{0}]", ex.Message));
                    logger.Error(ex);

                }
            }
            else
            {
                logger.Info("La lista de incidencias esta vacia");
            }
        }
        public void Dispose()
        {
            logger.Info("Se termina la ejecucion de la tarea de servicio web");
        }
    }
}