using Asistencia.Clases;
using log4net;
using System;
using System.Collections.Generic;
using zkemkeeper;

namespace Asistencia.Tareas
{
    public class ProcesoColector : IDisposable
    {
        // Logger
        public static readonly ILog logger = LogManager.GetLogger(Constantes.LOG_ASISTENCIA_TAREA_COLECTOR);

        // Fabrica de Objetos de Acceso a Datos (DAO)
        public static readonly IDaoFactory daoFactory = new NHibernateDaoFactory();

        public static void iniciarColeccion()
        {
            // Variables de Cajon
            int iMachineNumber = 1;
            int idwErrorCode = 0;
            string sdwEnrollNumber = Constantes.TEXTO_BLANCO;
            int idwVerifyMode = 0;
            int idwInOutMode = 0;
            int idwYear = 0;
            int idwMonth = 0;
            int idwDay = 0;
            int idwHour = 0;
            int idwMinute = 0;
            int idwSecond = 0;
            int idwWorkcode = 0;

            // Variables de acceso a la base de datos
            IControlAccesoDao controlAccesoDao = daoFactory.GetControlAccesoDao();
            IColectorMovimientoDao colectorMovimientoDao = daoFactory.GetColectorMovimientosDao();
            IColectorMovimientoIncidenciaDao colectorMovimientoIncidenciaDao = daoFactory.GetColectorMovimientosIncidenciaDao();
            IEmpleadoDao empleadoDao = daoFactory.GetEmpleadoDao();
            IIncidenciaDao incidenciaDao = daoFactory.GetIncidenciaDao();

            logger.Info("PROCESO COLECTOR: Ejecucion automatica de la tarea de coleccion");

            List<ControlAcceso> lista = null;
            try
            {
                lista = controlAccesoDao.GetListado(true, null, DbDao.ControlAccesoDao.Ordenamiento.FechaUltimaConexion);

            }
            catch (Exception ex)
            {

                logger.Error(String.Format("Error al intentar obtener la lista de Controles de Acceso. Detalles: [{0}]", ex.Message), ex);
            }



            if (lista != null && lista.Count > 0)
            {
                foreach (ControlAcceso control in lista)
                {
                    logger.Info(String.Format("Procesando control con Nombre: [{0}], Direccion IP: [{1}] y Puerto: [{2}]", control.Nombre, control.DireccionIp, control.Puerto));

                    try
                    {

                        CZKEMClass _control = new CZKEMClass();
                        if (_control.Connect_Net(control.DireccionIp, Int32.Parse(control.Puerto.Value.ToString())))
                        {
                            // Deshabilitanos el control
                            _control.EnableDevice(iMachineNumber, false);
                            logger.Info(String.Format("Conectado al control con Nombre: [{0}], Direccion IP: [{1}] y Puerto: [{2}]", control.Nombre, control.DireccionIp, control.Puerto));

                            try
                            {
                                control.FechaUltimaConexion = DateTime.Now;
                                controlAccesoDao.SaveOrUpdate(control);
                            }
                            catch (Exception ex)
                            {
                                logger.Warn(String.Format("Error al actualizar la Fecha de Ultima Conexionc del Control con Nombre: [{0}], Direccion IP: [{1}] y Puerto: [{2}]. Codigo de Error: [{3}]", control.Nombre, control.DireccionIp, control.Puerto, idwErrorCode));
                                logger.Warn(ex);

                            }


                            if (_control.ReadGeneralLogData(iMachineNumber))
                            {
                                logger.Info(String.Format("Lectura de Datos Generales con EXITO control con Nombre: [{0}], Direccion IP: [{1}] y Puerto: [{2}]", control.Nombre, control.DireccionIp, control.Puerto));
                                while (_control.SSR_GetGeneralLogData(iMachineNumber, out sdwEnrollNumber, out idwVerifyMode,
                                            out idwInOutMode, out idwYear, out idwMonth, out idwDay, out idwHour, out idwMinute,
                                            out idwSecond, ref idwWorkcode))
                                {
                                    logger.Info(String.Format("Datos Obtenidos: CveControlAcceso: [{0}], sdwEnrollNumber: [{1}], idwVerifyMode: [{2}], idwInOutMode: [{3}], idwYear: [{4}], idwMonth: [{5}], idwDay: [{6}], idwHour: [{7}], idwMinute: [{8}], idwSecond: [{9}], idwWorkcode: [{10}]", control.CveControlAcceso, sdwEnrollNumber, idwVerifyMode, idwInOutMode, idwYear, idwMonth, idwDay, idwHour, idwMinute, idwSecond, idwWorkcode));
                                    ColectorMovimiento colector = new ColectorMovimiento(control.IdControl, control.CveControlAcceso, sdwEnrollNumber, idwVerifyMode, idwInOutMode, idwYear, idwMonth, idwDay, idwHour, idwMinute, idwSecond, idwWorkcode);
                                    ColectorMovimiento up = colectorMovimientoDao.SaveOrUpdate(colector);
                                    colector = up;
                                }
                            }
                            else
                            {
                                _control.GetLastError(ref idwErrorCode);
                                if (idwErrorCode != 0)
                                {
                                    String strError = String.Format("ERROR de conexion al control con Nombre: [{0}], Direccion IP: [{1}] y Puerto: [{2}]. Codigo de Error [{3}]", control.Nombre, control.DireccionIp, control.Puerto, idwErrorCode);
                                    logger.Error(strError);

                                    try
                                    {
                                        ColectorMovimientoIncidencia coleMovIncidencia = new ColectorMovimientoIncidencia();
                                        coleMovIncidencia.ControlAcceso = control;
                                        coleMovIncidencia.Detalles = strError;
                                        colectorMovimientoIncidenciaDao.Save(coleMovIncidencia);
                                    }
                                    catch (Exception ex)
                                    {
                                        logger.Warn(String.Format("No se pudo guardar la incidencia de coleccion de movimientos. Mensaje: [{0}]", ex.Message));
                                    }
                                }
                                else
                                {
                                    logger.Info(String.Format("OK. El control Reporta que NO existen movimientos. Datos de conexion son Nombre: [{0}], Direccion IP: [{1}] y Puerto: [{2}]. Codigo de Mensaje: [{3}]", control.Nombre, control.DireccionIp, control.Puerto, idwErrorCode));
                                }

                            }

                            // Limpiamos el log
                            if (_control.ClearGLog(iMachineNumber))
                            {
                                _control.RefreshData(iMachineNumber);
                            }
                            else
                            {
                                _control.GetLastError(ref idwErrorCode);
                                logger.Error(String.Format("Error en el Proceso de Limpiar el LOG del Control con Nombre: [{0}], Direccion IP: [{1}] y Puerto: [{2}]. Codigo de Error: [{3}]", control.Nombre, control.DireccionIp, control.Puerto, idwErrorCode));
                            }


                            _control.EnableDevice(iMachineNumber, true);
                            _control.Disconnect();

                        }
                        else
                        {
                            _control.GetLastError(ref idwErrorCode);

                            String strError = String.Format("Error en el Proceso de Conexion al control con Nombre: [{0}], Direccion IP: [{1}] y Puerto: [{2}]. Codigo de Error: [{3}]", control.Nombre, control.DireccionIp, control.Puerto, idwErrorCode);
                            logger.Error(strError);

                            try
                            {
                                ColectorMovimientoIncidencia coleMovIncidencia = new ColectorMovimientoIncidencia();
                                coleMovIncidencia.ControlAcceso = control;
                                coleMovIncidencia.Detalles = strError;
                                colectorMovimientoIncidenciaDao.Save(coleMovIncidencia);
                            }
                            catch (Exception ex)
                            {
                                logger.Warn(String.Format("No se pudo guardar la incidencia de coleccion de movimientos. Mensaje: [{0}]", ex.Message));
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        String strError = String.Format("Error en el Proceso de Procesar Control con Nombre: [{0}], Direccion IP: [{1}] y Puerto: [{2}]. Codigo de Error: [{3}]", control.Nombre, control.DireccionIp, control.Puerto, idwErrorCode);
                        logger.Error(strError);
                        logger.Error(ex);

                        try
                        {
                            ColectorMovimientoIncidencia coleMovIncidencia = new ColectorMovimientoIncidencia();
                            coleMovIncidencia.ControlAcceso = control;
                            coleMovIncidencia.Detalles = strError;
                            colectorMovimientoIncidenciaDao.Save(coleMovIncidencia);
                        }
                        catch (Exception except)
                        {
                            logger.Warn(String.Format("No se pudo guardar la incidencia de coleccion de movimientos. Mensaje: [{0}]", except.Message));
                        }

                    }
                }

                // Se inicia el purrun de la tabla de inicidencias
                List<ColectorMovimiento> listaColector = colectorMovimientoDao.GetListado();
                if (listaColector != null && listaColector.Count > 0)
                {
                    foreach (ColectorMovimiento movimiento in listaColector)
                    {
                        try
                        {
                            Empleado empleado = empleadoDao.GetByNumeroEmpleado(Int32.Parse(movimiento.EnrollNumber));
                            ControlAcceso control = controlAccesoDao.GetById(movimiento.CveControlAcceso);

                            DateTime fechaAlta = DateTime.Now;
                            DateTime fechaIncidencia = new DateTime(movimiento.Year != null ? movimiento.Year.Value : fechaAlta.Year,
                                movimiento.Month != null ? movimiento.Month.Value : fechaAlta.Month, movimiento.Day != null ? movimiento.Day.Value : fechaAlta.Day,
                                movimiento.Hour != null ? movimiento.Hour.Value : fechaAlta.Hour, movimiento.Minute != null ? movimiento.Minute.Value : fechaAlta.Minute,
                                movimiento.Second != null ? movimiento.Second.Value : fechaAlta.Second);


                            if (empleado == null)
                            {
                                String strError = String.Format("ADVERTENCIA: El Empleado con clave [{0}] no fue encontrado en la base de datos. Esta incidencia no se guardara", movimiento.EnrollNumber);
                                logger.Warn(strError);

                                try
                                {
                                    ColectorMovimientoIncidencia coleMovIncidencia = new ColectorMovimientoIncidencia();
                                    coleMovIncidencia.ControlAcceso = control;
                                    coleMovIncidencia.Detalles = strError;
                                    coleMovIncidencia.CveEmpleado = movimiento.EnrollNumber;
                                    colectorMovimientoIncidenciaDao.Save(coleMovIncidencia);
                                }
                                catch (Exception except)
                                {
                                    logger.Warn(String.Format("No se pudo guardar la incidencia de coleccion de movimientos. Mensaje: [{0}]", except.Message));
                                }

                            }
                            if (control == null)
                            {
                                logger.Warn(String.Format("ADVERTENCIA: El Control de Acceso con Clave: [{0}] no fue encontrado en la base de datos. Esta incidencia no se guardara", movimiento.CveControlAcceso));

                            }

                            if (empleado != null && control != null)
                            {
                                try
                                {
                                    Incidencia validacion = incidenciaDao.GetByEmpleadoControlFechaInOutMode(empleado, control, fechaIncidencia, movimiento.InOutMode.HasValue ? movimiento.InOutMode.Value : 0);

                                    if (empleado != null)
                                    {
                                        try
                                        {
                                            empleado.UltimaColeccion = String.Format("Fecha: [{0}] en el Control: [{1}]", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), control.Nombre);
                                            empleadoDao.SaveOrUpdate(empleado);
                                        }
                                        catch
                                        {
                                        }

                                    }

                                    if (validacion == null)
                                    {
                                        Incidencia incidencia = new Incidencia();
                                        incidencia.ControlAcceso = control;
                                        incidencia.Empleado = empleado;
                                        incidencia.EnviadoWs = 0;
                                        incidencia.FechaAlta = fechaAlta;
                                        incidencia.FechaHoraIncidencia = fechaIncidencia;
                                        incidencia.InOutMode = movimiento.InOutMode;
                                        Incidencia up = incidenciaDao.SaveOrUpdate(incidencia);
                                        incidencia = up;
                                    }
                                    else
                                    {
                                        logger.Warn(String.Format("ADVERTENCIA: Esta incidencia ya estaba dada de alta con la clave [{0}]", validacion.CveIncidencia));
                                    }

                                }
                                catch (Exception ex)
                                {
                                    logger.Error(String.Format("Error: Al intentar guardar la incidencia con Control: [{0}], Empleado: [{1}], Fecha Hora [{2}]. Mensaje: [{3}]", control.Nombre, empleado.Nombre, fechaIncidencia, ex.Message));
                                    logger.Error(ex);

                                }
                            }

                        }
                        catch (Exception ex)
                        {
                            logger.Error(String.Format("Error al momento de intentar procesar los colectores de movimiento. Mensaje: [{0}] ", ex.Message));
                            logger.Error(ex);
                        }
                    }
                }

                // Se borra la tabla del colector de movimientos
                if (listaColector != null && listaColector.Count > 0)
                {
                    foreach (ColectorMovimiento movimiento in listaColector)
                    {
                        try
                        {
                            colectorMovimientoDao.Delete(movimiento);
                        }
                        catch (Exception ex)
                        {
                            logger.Error(String.Format("Error al momento de intentar limpiar los colectores de movimiento. Mensaje: [{0}] ", ex.Message));
                            logger.Error(ex);
                        }
                    }
                }
            }
            else
            {
                logger.Info("La lista de controles de acceso a procesar está vacia o nula de controles de acceso activos en la base de datos.");
            }
        }

        public void Dispose()
        {
            logger.Info("Se termina la ejecucion de la tarea de coleccion.");
        }

    }
}