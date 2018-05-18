using Asistencia.Clases;
using System;
using System.Collections.Generic;
using zkemkeeper;


namespace Asistencia.Proceso
{
    public partial class CargaPlantillaEnControl : PaginaExtensible
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
                log.Info(String.Format("El usuario: [{0}] ha cargado la pagina de Proceso Carga Plantilla en Control", UsuarioActual.Nombre));


                // Limpiamos los itemos de la plaza fuente ya la de destino
                txtPlaza.Items.Clear();
                txtPlaza.Items.Add(NuevoListItem(Constantes.TEXTO_SELECCION, Constantes.TEXTO_BLANCO));

                txtPlazaDestino.Items.Clear();
                txtPlazaDestino.Items.Add(NuevoListItem(Constantes.TEXTO_SELECCION, Constantes.TEXTO_BLANCO));

                foreach (DbDominio.Plaza obj in listaDePlaza())
                {
                    txtPlaza.Items.Add(NuevoListItem(obj.Nombre, obj.Id.ToString()));
                    txtPlazaDestino.Items.Add(NuevoListItem(obj.Nombre, obj.Id.ToString()));
                }

                MostrarJavascriptTabla();

            }

        }


        protected void txtPlaza_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Limpiamos la lista de plantilla
            Repeticion.DataSource = new List<Plantilla>();
            Repeticion.DataBind();


            if (ValidarObjeto(txtPlaza.SelectedValue))
            {

                // Limpiamos el dropdown de plantillas
                txtPlantilla.Items.Clear();
                txtPlantilla.Items.Add(NuevoListItem(Constantes.TEXTO_SELECCION, Constantes.TEXTO_BLANCO));
                try
                {
                    // Obtenemos la lista de controles de acceso de esta plaza
                    List<ControlAcceso> listaControles = controlAccesoDao.GetListado(true, plazaDao.GetById(Int32.Parse(txtPlaza.SelectedValue)), DbDao.ControlAccesoDao.Ordenamiento.Nombre);

                    // Obtenemos el grupo de plantillas que corresponden al grupo de controles
                    List<GrupoPlantilla> plantillas = plantillaDao.GetGrupo(listaControles);

                    if (plantillas != null && plantillas.Count > 0)
                    {
                        foreach (GrupoPlantilla obj in plantillas)
                        {
                            txtPlantilla.Items.Add(NuevoListItem(String.Format("Control: [{0}], Ip: [{1}], Empleados: [{2} ]", obj.Control.Nombre, obj.Control.DireccionIp, obj.Conteo), obj.Control.CveControlAcceso.ToString()));
                        }

                    }

                }
                catch (Exception ex)
                {
                    log.Error(String.Format("Error en el Proceso Obtener Plantillas para la Plaza [{0}]. Mensaje: [{1}]", txtPlaza.SelectedValue, ex.Message));
                    ManejarExcepcion(ex);
                }
            }
        }

        protected void txtPlantilla_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Limpiamos la lista de plantillas
            Repeticion.DataSource = new List<Plantilla>();
            Repeticion.DataBind();


            if (ValidarObjeto(txtPlantilla.SelectedValue))
            {
                try
                {
                    // Obtenemos el control seleccionado
                    ControlAcceso ctrl = controlAccesoDao.GetById(Int32.Parse(txtPlantilla.SelectedValue));
                    if (ctrl != null)
                    {

                        // Obtenemos la lista de plantillas para el control seleccionado
                        List<DbDominio.Plantilla> listaPlantilla = plantillaDao.GetListado(null, ctrl);
                        Repeticion.DataSource = listaPlantilla;
                        Repeticion.DataBind();
                    }

                }
                catch (Exception ex)
                {
                    log.Error(String.Format("Error en el Proceso Obtener Plantillas para la Plaza [{0}], y el Control [{1}]. Mensaje: [{2}]", txtPlaza.SelectedValue, txtPlantilla.SelectedValue, ex.Message));
                    ManejarExcepcion(ex);
                }
            }

        }

        protected void txtPlazaDestino_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ValidarObjeto(txtPlazaDestino.SelectedValue))
            {
                // Limpiamos la lista de Controles de Acceso
                txtControlDestino.Items.Clear();
                txtControlDestino.Items.Add(NuevoListItem(Constantes.TEXTO_SELECCION, Constantes.TEXTO_BLANCO));

                try
                {
                    // Obtememos el listado de controles para la plaza seleccionada
                    List<ControlAcceso> controles = controlAccesoDao.GetListado(true, plazaDao.GetById(Int32.Parse(txtPlaza.SelectedValue)), DbDao.ControlAccesoDao.Ordenamiento.Nombre);
                    if (controles != null && controles.Count > 0)
                    {
                        foreach (ControlAcceso ctrl in controles)
                        {
                            txtControlDestino.Items.Add(NuevoListItem(ctrl.Nombre, ctrl.CveControlAcceso.ToString()));
                        }

                    }

                }
                catch (Exception ex)
                {
                    log.Error(String.Format("Error en el Proceso Obtener Lista de Controles de Acceso para la Plaza [{0}]. Mensaje: [{1}]", txtPlazaDestino.SelectedValue, ex.Message));
                    ManejarExcepcion(ex);
                }
            }
        }

        protected void BtnAceptar_Click(object sender, EventArgs e)
        {
            // Validamos que se haya seleccionado un control fuente
            if (!ValidarObjeto(txtPlantilla.SelectedValue))
            {
                MostrarExcepcion(Constantes.MENSAJE_CAMPO_REQUERIDO_TITULO, "Es requerido Seleccionar la plantilla fuente");
                return;
            }

            // Validamos que se haya seleccionado un control de destino
            if (!ValidarObjeto(txtControlDestino.SelectedValue))
            {
                MostrarExcepcion(Constantes.MENSAJE_CAMPO_REQUERIDO_TITULO, "Es requerido Seleccionar el Control de Acceso de destino");
                return;
            }

            // Validamos que el control de destino no sea el mismo que el seleccionado en la fuente
            if (txtControlDestino.SelectedValue.Equals(txtPlantilla.SelectedValue))
            {
                MostrarExcepcion(Constantes.MENSAJE_CAMPO_CON_ERROR_TITULO, "El Control de Acceso de Destino es el Mismo que el de la fuente de Datos");
            }

            // Obtenemos y validamos el control fuente
            ControlAcceso controlFuente = null;
            try
            {
                controlFuente = controlAccesoDao.GetById(Int32.Parse(txtPlantilla.SelectedValue));
            }
            catch (Exception ex)
            {
                log.Error(String.Format("Error en el Proceso Obtener Control Fuente. Mensaje: [{0}]", ex.Message));
                ManejarExcepcion(ex);
            }

            if (controlFuente == null)
            {
                MostrarExcepcion(Constantes.MENSAJE_CAMPO_CON_ERROR_TITULO, "El control fuente es nulo");
                return;
            }

            // Obtenemos y validamos el control de destino
            ControlAcceso controlDestino = null;
            try
            {
                controlDestino = controlAccesoDao.GetById(Int32.Parse(txtControlDestino.SelectedValue));
            }
            catch (Exception ex)
            {

                log.Error(String.Format("Error en el Proceso Obtener Control Destino. Mensaje: [{0}]", ex.Message));
                ManejarExcepcion(ex);

            }

            if (controlDestino == null)
            {
                MostrarExcepcion(Constantes.MENSAJE_CAMPO_CON_ERROR_TITULO, "El control de destino es nulo");
                return;
            }

            // Obtenemos y validamos la lista de plantillas a cargar
            List<DbDominio.Plantilla> listaPlantilla = null;
            try
            {
                listaPlantilla = plantillaDao.GetListado(null, controlFuente);

            }
            catch (Exception ex)
            {
                log.Error(String.Format("Error en el Proceso Obtener La plantilla desde el Control Fuente. Mensaje: [{0}]", ex.Message));
                ManejarExcepcion(ex);
            }

            if (listaPlantilla == null || listaPlantilla.Count == 0)
            {
                MostrarExcepcion(Constantes.MENSAJE_CAMPO_CON_ERROR_TITULO, "La plantilla seleccionada es nula o vacia");
                return;
            }


            // Al parecer todo va bien, iniciarmos el proceso
            int idwErrorCode = 0;
            string sdwEnrollNumber = Constantes.TEXTO_BLANCO;
            string sName = Constantes.TEXTO_BLANCO;
            int idwFingerIndex = 0;
            string sTmpData = Constantes.TEXTO_BLANCO;
            int iPrivilege = 0;
            string sPassword = Constantes.TEXTO_BLANCO;
            bool bEnabled = false;
            int iFlag = 1;
            int iUpdateFlag = 1;
            int iMachineNumber = 1;

            try
            {
                log.Info(String.Format("Se Intentará conectar con el control de acceso con ID: [{0}]. El control tiene la direccion ip: [{1}] y el puerto: [{2}]", txtControlDestino.SelectedValue, controlDestino.DireccionIp, controlDestino.Puerto));

                CZKEMClass _control = new CZKEMClass();
                // _control.PullMode = 1;

                if (_control.Connect_Net(controlDestino.DireccionIp, Int32.Parse(controlDestino.Puerto.Value.ToString())))
                {
                    log.Info(String.Format("Se logro correctamente la conexion con el control con direccion ip: [{0}] y el puerto: [{1}]", controlDestino.DireccionIp, controlDestino.Puerto));

                    _control.RegEvent(iMachineNumber, 65535);
                    _control.EnableDevice(iMachineNumber, false);

                    if (_control.BeginBatchUpdate(iMachineNumber, iUpdateFlag))//create memory space for batching data
                    {
                        log.Info(String.Format("Inicia el Proceso BeginBatchUpdate Con ImachineNumber : [{0}] iUpdateFlag: [{1}]", iMachineNumber, iUpdateFlag));

                        string sLastEnrollNumber = "";//the former enrollnumber you have upload(define original value as 0)

                        foreach (DbDominio.Plantilla planti in listaPlantilla)
                        {
                            sdwEnrollNumber = planti.Enrollnumber;
                            sName = planti.Nombre;
                            idwFingerIndex = planti.Fingerindex != null ? planti.Fingerindex.Value : 0;
                            sTmpData = planti.Tmpdata;
                            iPrivilege = planti.Privilege != null ? planti.Privilege.Value : 0;
                            sPassword = planti.Password;
                            bEnabled = planti.Enabled != null ? planti.Enabled.Value : false;
                            iFlag = planti.Flag != null ? planti.Flag.Value : 1;

                            log.Info(String.Format("Se Carga la Siguiente sdwEnrollNumber: [{0}] sName: [{1}], idwFingerIndex: [{2}], sTmpData: [{3}], iPrivilege : [{4}], sPassword : [{5}], bEnabled : [{6}], iFlag: [{7}]", sdwEnrollNumber, sName, idwFingerIndex, sTmpData, iPrivilege, sPassword, bEnabled, iFlag));

                            if (sdwEnrollNumber != sLastEnrollNumber)//identify whether the user information(except fingerprint templates) has been uploaded
                            {
                                if (_control.SSR_SetUserInfo(iMachineNumber, sdwEnrollNumber, sName, sPassword, iPrivilege, bEnabled))//upload user information to the memory
                                {
                                    log.Info(String.Format("Se Completa El ProcesoS SR_SetUserInfo con la siguiente Informacion_ iMachineNumber [{0}] sdwEnrollNumber: [{1}] sName: [{2}], sPassword: [{3}], iPrivilege : [{4}], bEnabled: [{5}]", iMachineNumber, sdwEnrollNumber, sName, sPassword, iPrivilege, bEnabled));

                                    _control.SetUserTmpExStr(iMachineNumber, sdwEnrollNumber, idwFingerIndex, iFlag, sTmpData);//upload templates information to the memory

                                    log.Info(String.Format("Se Completa El Proceso SetUserTmpExStr con la siguiente Informacion_ iMachineNumber [{0}] sdwEnrollNumber: [{1}] idwFingerIndex: [{2}], iFlag: [{3}], sTmpData : [{4}]", iMachineNumber, sdwEnrollNumber, idwFingerIndex, iFlag, sTmpData));

                                }
                                else
                                {
                                    _control.GetLastError(ref idwErrorCode);
                                    _control.EnableDevice(iMachineNumber, true);

                                    log.Error(String.Format("Error en el Proceso Distribuir  La plantilla Al Control de Destino. Codigo de Error: [{0}]", idwErrorCode));
                                    MostrarExcepcion("Error en el proceso", String.Format("Error en el Proceso Distribuir  La plantilla Al Control de Destino. Codigo de Error: [{0}]", idwErrorCode));
                                    return;
                                }
                            }
                            else
                            {
                                _control.SetUserTmpExStr(iMachineNumber, sdwEnrollNumber, idwFingerIndex, iFlag, sTmpData);
                                log.Info(String.Format("Se Completa El Proceso SetUserTmpExStr (sdwEnrollNumber != sLastEnrollNumber) con la siguiente Informacion_ iMachineNumber [{0}] sdwEnrollNumber: [{1}] idwFingerIndex: [{2}], iFlag: [{3}], sTmpData : [{4}]", iMachineNumber, sdwEnrollNumber, idwFingerIndex, iFlag, sTmpData));
                            }
                            sLastEnrollNumber = sdwEnrollNumber;

                            try
                            {
                                Empleado empleado = empleadoDao.GetByNumeroEmpleado(Int32.Parse(sdwEnrollNumber));
                                if (empleado != null && controlDestino != null)
                                {
                                    EmpleadoControlAcceso eca = empleadoControlAccesoDao.GetByControlAcceso(controlDestino, empleado);
                                    if (eca == null)
                                    {
                                        eca = new EmpleadoControlAcceso();
                                    }
                                    eca.ControlAcceso = controlDestino;
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

                    }
                    _control.BatchUpdate(iMachineNumber);//upload all the information in the memory
                    _control.RefreshData(iMachineNumber);//the data in the device should be refreshed
                    _control.EnableDevice(iMachineNumber, true);
                    _control.Disconnect();
                    MostrarExito("Proceso Completo", "Se completo el proceso exitosamente");
                }
                else
                {
                    _control.GetLastError(ref idwErrorCode);
                    log.Error(String.Format("Error en el Proceso de Conexion al control con ID [{0}], Direccion IP [{1}] y Puerto [{2}]. Codigo de Error [{3}]", controlDestino.IdControl, controlDestino.DireccionIp, controlDestino.Puerto, idwErrorCode));
                    MostrarExcepcion("Conexion Invalida", String.Format("Error en el Proceso de Conexion con el Control con Direccion IP: {0} y Puerto {1}", controlDestino.DireccionIp, controlDestino.Puerto));
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