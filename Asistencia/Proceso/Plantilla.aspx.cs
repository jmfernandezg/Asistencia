using Asistencia.Clases;
using System;
using System.Collections.Generic;
using zkemkeeper;

namespace Asistencia.Proceso
{
    public partial class Plantilla : PaginaExtensible
    {
        IPlazaDao plazaDao;
        IControlAccesoDao controlAccesoDao;
        IPlantillaDao plantillaDao;
        IEmpleadoDao empleadoDao;
        IEmpleadoControlAccesoDao empleadoControlAccesoDao;

        protected void Page_Load(object sender, EventArgs e)
        {
            plazaDao = daoFactory.GetPlazaDao();
            controlAccesoDao = daoFactory.GetControlAccesoDao();
            plantillaDao = daoFactory.GetPlantillaDao();
            empleadoControlAccesoDao = daoFactory.GetEmpleadoControlAccesoDao();
            empleadoDao = daoFactory.GetEmpleadoDao();

            if (!IsPostBack)
            {
                log.Info(String.Format("El usuario: [{0}] ha cargado la pagina de Proceso Plantilla", UsuarioActual.Nombre));


                txtPlaza.Items.Clear();
                txtPlaza.Items.Add(NuevoListItem(Constantes.TEXTO_SELECCION, Constantes.TEXTO_BLANCO));

                foreach (DbDominio.Plaza obj in listaDePlaza())
                {
                    txtPlaza.Items.Add(NuevoListItem(obj.Nombre, obj.Id.ToString()));
                }
                MostrarJavascriptTabla();

            }
        }

        protected void txtPlaza_SelectedIndexChanged(object sender, EventArgs e)
        {
            Repeticion.DataSource = new List<Plantilla>();
            Repeticion.DataBind();


            if (ValidarObjeto(txtPlaza.SelectedValue))
            {

                txtControlAcceso.Items.Clear();
                txtControlAcceso.Items.Add(NuevoListItem(Constantes.TEXTO_SELECCION, Constantes.TEXTO_BLANCO));
                try
                {
                    List<ControlAcceso> controles = controlAccesoDao.GetListado(true, plazaDao.GetById(Int32.Parse(txtPlaza.SelectedValue)), DbDao.ControlAccesoDao.Ordenamiento.Nombre);
                    if (controles != null && controles.Count > 0)
                    {
                        foreach (ControlAcceso ctrl in controles)
                        {
                            txtControlAcceso.Items.Add(NuevoListItem(ctrl.Nombre, ctrl.CveControlAcceso.ToString()));
                        }

                    }

                    CargarDatos();

                }
                catch (Exception ex)
                {
                    log.Error(String.Format("Error en el Proceso Obtener Lista de Controles de Acceso para la Plaza [{0}]. Mensaje: [{1}]", txtPlaza.SelectedValue, ex.Message));
                    ManejarExcepcion(ex);
                }
            }
        }

        protected void txtControlAcceso_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ValidarObjeto(txtControlAcceso.SelectedValue))
            {
                Repeticion.DataSource = new List<Plantilla>();
                Repeticion.DataBind();

                CargarDatos();
            }

        }

        protected void CargarDatos()
        {


            // Cargar los datos de un solo control
            if (ValidarObjeto(txtControlAcceso.SelectedValue))
            {
                try
                {
                    ControlAcceso ctrl = controlAccesoDao.GetById(Int32.Parse(txtControlAcceso.SelectedValue));

                    if (ctrl != null)
                    {
                        Repeticion.DataSource = plantillaDao.GetListado(null, ctrl);
                        Repeticion.DataBind();
                    }
                }
                catch (Exception ex)
                {
                    log.Error(String.Format("Error en el Proceso Obtener Lista de Plantillas Para el Control de Acceso [{0}]. Mensaje: [{1}]", txtControlAcceso.SelectedValue, ex.Message));
                    ManejarExcepcion(ex);
                }
            }
            else
            {
                // Cargar los datos de varios controles
                if (ValidarObjeto(txtPlaza.SelectedValue))
                {
                    try
                    {
                        List<ControlAcceso> controles = controlAccesoDao.GetListado(true, plazaDao.GetById(Int32.Parse(txtPlaza.SelectedValue)), DbDao.ControlAccesoDao.Ordenamiento.Nombre);

                        if (controles != null && controles.Count > 0)
                        {
                            Repeticion.DataSource = plantillaDao.GetListado(controles, null);
                            Repeticion.DataBind();
                        }
                    }
                    catch (Exception ex)
                    {
                        log.Error(String.Format("Error en el Proceso Obtener Lista de Plantillas Para el Control de Acceso [{0}]. Mensaje: [{1}]", txtControlAcceso.SelectedValue, ex.Message));
                        ManejarExcepcion(ex);
                    }
                }
            }

        }

        protected void BtnDescargar_Click(object sender, EventArgs e)
        {
            log.Info(String.Format("El usuario [{0}] presiono el boton para descargar la plantilla del Control de Acceso con ID: [{1}]", UsuarioActual.Nombre, txtControlAcceso.SelectedValue));

            // Validar si se selecciono un control de acceso
            if (!ValidarObjeto(txtControlAcceso.SelectedValue))
            {
                MostrarAdvertencia(Constantes.MENSAJE_CAMPO_REQUERIDO_TITULO, "Por favor seleccione un control del listado");
                return;
            }

            ControlAcceso control = null;

            try
            {
                control = controlAccesoDao.GetById(Int32.Parse(txtControlAcceso.SelectedValue));

            }
            catch (Exception ex)
            {
                log.Error(String.Format("Error en el Proceso de Conexion con el control. Mensaje: [{0}]", ex.Message));
                ManejarExcepcion(ex);
            }

            if (control == null)
            {
                MostrarAdvertencia("Control de Acceso No Encontrado", String.Format("No fue Posible encontrar el Control de Acceso con identificador [0]", txtControlAcceso.SelectedValue));
                return;
            }


            // Variables de cajon
            int iMachineNumber = 1;
            string sdwEnrollNumber = Constantes.TEXTO_BLANCO;
            string sName = Constantes.TEXTO_BLANCO;
            string sPassword = Constantes.TEXTO_BLANCO;
            int iPrivilege = 0;
            bool bEnabled = false;
            int idwErrorCode = 0;

            int idwFingerIndex;
            string sTmpData = Constantes.TEXTO_BLANCO;
            int iTmpLength = 0;
            int iFlag = 0;


            try
            {
                log.Info(String.Format("Se Intentará conectar con el control de acceso con ID: [{0}]. El control tiene la direccion ip: [{1}] y el puerto: [{2}]", txtControlAcceso.SelectedValue, control.DireccionIp, control.Puerto));

                CZKEMClass _control = new CZKEMClass();
                if (_control.Connect_Net(control.DireccionIp, Int32.Parse(control.Puerto.Value.ToString())))
                {
                    log.Info(String.Format("Se logro correctamente la conexion con el control con direccion ip: [{0}] y el puerto: [{1}]", control.DireccionIp, control.Puerto));

                    _control.EnableDevice(iMachineNumber, false);
                    _control.ReadAllUserID(iMachineNumber);//read all the user information to the memory
                    _control.ReadAllTemplate(iMachineNumber);//read all the users' fingerprint templates to the memory


                    while (_control.SSR_GetAllUserInfo(iMachineNumber, out sdwEnrollNumber, out sName, out sPassword, out iPrivilege, out bEnabled))//get all the users' information from the memory
                    {
                        log.Info(String.Format("Se obtienen los datos iMachineNumber: [{0}], sdwEnrollNumber: [{1}], sName: [{2}], sPassword: [{3}], iPrivilege: [{4}], bEnabled: [{5}]", iMachineNumber, sdwEnrollNumber, sName, sPassword, iPrivilege, bEnabled));

                        for (idwFingerIndex = 0; idwFingerIndex < 10; idwFingerIndex++)
                        {
                            if (_control.GetUserTmpExStr(iMachineNumber, sdwEnrollNumber, idwFingerIndex, out iFlag, out sTmpData, out iTmpLength))//get the corresponding templates string and length from the memory
                            {
                                log.Info(String.Format("Se obtienen los datos idwFingerIndex: [{0}], iFlag: [{1}], sTmpData: [{2}], iTmpLength: [{3}]", idwFingerIndex, iFlag, sTmpData, iTmpLength));


                                try
                                {
                                    DbDominio.Plantilla planti = plantillaDao.GetByControlAccesoEnrollNumberFingerIndex(control, sdwEnrollNumber, idwFingerIndex);

                                    if (planti == null)
                                    {
                                        planti = new DbDominio.Plantilla();
                                        planti.ControlAcceso = control;
                                        planti.Usuario_creado_por = UsuarioActual;
                                        log.Info("Se creará una plantilla nueva a partir de estos datos");
                                    }
                                    else
                                    {
                                        log.Info("Esta es una plantilla nueva que se insertara");
                                    }

                                    planti.FechaModificacion = DateTime.Now;
                                    planti.Usuario_modificado_por = UsuarioActual;
                                    planti.Enabled = bEnabled;
                                    planti.Enrollnumber = sdwEnrollNumber;
                                    planti.Fingerindex = idwFingerIndex;
                                    planti.Flag = iFlag;
                                    planti.IpControlTemplate = control.DireccionIp;
                                    planti.Nombre = sName;
                                    planti.Password = sPassword;
                                    planti.Privilege = iPrivilege;
                                    planti.Status = true;
                                    planti.Tmpdata = sTmpData;
                                    plantillaDao.SaveOrUpdate(planti);

                                    try
                                    {
                                        Empleado empleado = empleadoDao.GetByNumeroEmpleado(Int32.Parse(sdwEnrollNumber));
                                        if (empleado != null && control != null)
                                        {
                                            EmpleadoControlAcceso eca = empleadoControlAccesoDao.GetByControlAcceso(control, empleado);
                                            if (eca == null)
                                            {
                                                eca = new EmpleadoControlAcceso();
                                            }
                                            eca.CveEmpleado = empleado.CveEmpleado;
                                            eca.CveControlAcceso = control.CveControlAcceso;
                                            eca.ControlAcceso = control;
                                            eca.Empleado = empleado;

                                            empleadoControlAccesoDao.SaveOrUpdate(eca);
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        log.Error(String.Format("Error en el Proceso Guardar Empleado Control de Acceso en la base de Datos. Mensaje: [{0}]", ex.Message));
                                        log.Error(ex);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    log.Error(String.Format("Error en el Proceso Guardar Plantilla en la base de Datos. Mensaje: [{0}]", ex.Message));
                                    log.Error(ex);
                                }

                            }
                        }
                    }

                    _control.Disconnect();
                    MostrarExito("Proceso Completo", "Se completo el proceso exitosamente");
                }
                else
                {
                    _control.GetLastError(ref idwErrorCode);
                    log.Error(String.Format("Error en el Proceso de Conexion al control con ID [{0}], Direccion IP [{1}] y Puerto [{2}]. Codigo de Error [{3}]", control.IdControl, control.DireccionIp, control.Puerto, idwErrorCode));
                    MostrarExcepcion("Conexion Invalida", String.Format("Error en el Proceso de Conexion con el Control con Direccion IP: {0} y Puerto {1}", control.DireccionIp, control.Puerto));
                }


            }

            catch (Exception ex)
            {
                log.Error(String.Format("Error en el Proceso de Conexion con el control. Mensaje: [{0}]", ex.Message));
                ManejarExcepcion(ex);
            }

        }






    }
}