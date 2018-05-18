using Asistencia.Clases;
using System;
using System.Web.UI.WebControls;
using zkemkeeper;

namespace Asistencia.Proceso
{
    public partial class ActualizaFechaEnControl : PaginaExtensible
    {
        IPlazaDao plazaDao;
        IControlAccesoDao controlAccesoDao;
        IPlantillaDao plantillaDao;

        protected void Page_Load(object sender, EventArgs e)
        {
            plazaDao = daoFactory.GetPlazaDao();
            controlAccesoDao = daoFactory.GetControlAccesoDao();
            plantillaDao = daoFactory.GetPlantillaDao();

            if (!IsPostBack)
            {
                txtFecha.Text = DateTime.Now.ToString("yyyy/MM/dd");
                txtHora.Text = DateTime.Now.ToString("HH:mm:ss");

                txtPlaza.Items.Clear();
                txtPlaza.Items.Add(NuevoListItem(Constantes.TEXTO_SELECCION, Constantes.TEXTO_BLANCO));
                foreach (DbDominio.Plaza obj in listaDePlaza())
                {
                    txtPlaza.Items.Add(NuevoListItem(obj.Nombre, obj.Id.ToString()));
                }
                MostrarJavascriptTabla();

            }
        }

        protected void BtnActualizar_Click(object sender, EventArgs e)
        {
            log.Info(String.Format("El usuario [{0}] presiono el boton para actualizar la fecha en los controles. La nueva fecha es [{1}], la nueva hora es [{2}]", UsuarioActual.Nombre, txtFecha.Text, txtHora.Text));


            if (!ValidarObjeto(txtFecha.Text))
            {
                MostrarAdvertencia(Constantes.MENSAJE_CAMPO_REQUERIDO_TITULO, "Es requerido capturar una fecha");
            }
            if (!ValidarObjeto(txtHora.Text))
            {
                MostrarAdvertencia(Constantes.MENSAJE_CAMPO_REQUERIDO_TITULO, "Es requerido capturar una hora");
            }

            if (Repeticion == null || Repeticion.Items == null || Repeticion.Items.Count == 0)
            {
                MostrarAdvertencia(Constantes.MENSAJE_CAMPO_REQUERIDO_TITULO, "Es requerido seleccionar controles de acceso para actualizar las fechas");
            }

            foreach (RepeaterItem i in Repeticion.Items)
            {
                //Retrieve the state of the CheckBox
                CheckBox cb = (CheckBox)i.FindControl("CheckControlAcceso");
                if (cb.Checked)
                {
                    //Retrieve the value associated with that CheckBox
                    HiddenField identificador = (HiddenField)i.FindControl("identificador");

                    if (ValidarObjeto(identificador.Value))
                    {
                        log.Info(String.Format("El usuario: [{0}] ha seleccionado el control de acceso con identificador: [{1}] para actualizar su fecha y hora", UsuarioActual.Nombre, identificador));
                        try
                        {

                            int idwErrorCode = 0;

                            String[] DatosFecha = txtFecha.Text.Split('/');
                            String[] DatosHora = txtHora.Text.Split(':');

                            int idwYear = Convert.ToInt32(DatosFecha[0]);
                            int idwMonth = Convert.ToInt32(DatosFecha[1]);
                            int idwDay = Convert.ToInt32(DatosFecha[2]);
                            int idwHour = Convert.ToInt32(DatosHora[0]);
                            int idwMinute = Convert.ToInt32(DatosHora[1]);
                            int idwSecond = Convert.ToInt32(DatosHora[2]);

                            ControlAcceso control = controlAccesoDao.GetById(Int32.Parse(identificador.Value));
                            if (control != null)
                            {
                                log.Info(String.Format("El control de acceso tiene la direccion ip: [{0}] y el puerto [{1}]", control.DireccionIp, control.Puerto));
                                CZKEMClass _control = new CZKEMClass();
                                if (_control.Connect_Net(control.DireccionIp, Int32.Parse(control.Puerto.Value.ToString())))
                                {
                                    log.Info(String.Format("Se logro correctamente la conexion con el control de acceso con direccion ip: [{0}] y el puerto [{1}]", control.DireccionIp, control.Puerto));

                                    if (_control.SetDeviceTime2(1, idwYear, idwMonth, idwDay, idwHour, idwMinute, idwSecond))
                                    {
                                        log.Info(String.Format("Se logro correctamente la actualización de fecha y hora al control de acceso con direccion ip: [{0}] y el puerto [{1}]", control.DireccionIp, control.Puerto));
                                        _control.RefreshData(1);
                                        MostrarExito("Proceso Correcto", "Se actualizo correctamente el control " + control.Nombre);
                                    }
                                    else
                                    {
                                        _control.GetLastError(ref idwErrorCode);
                                        log.Error(String.Format("Error en el Proceso Actualizar Fecha En Control con Direccion ip: [{0}] y puerto [{1}], Codigo de Error [{2}]", control.DireccionIp, control.Puerto, idwErrorCode));
                                    }

                                    _control.Disconnect();

                                }
                                else
                                {
                                    _control.GetLastError(ref idwErrorCode);
                                    log.Error(String.Format("Error en el Proceso de Conexion al control con ID [{0}], Direccion IP [{1}] y Puerto [{2}]. Codigo de Error [{3}]", control.IdControl, control.DireccionIp, control.Puerto, idwErrorCode));
                                    MostrarExcepcion("Conexion Invalida", String.Format("Error en el Proceso de Conexion con el Control con Direccion IP: {0} y Puerto {1}", control.DireccionIp, control.Puerto));
                                }

                            }
                        }
                        catch (Exception ex)
                        {
                            log.Error(String.Format("Error en el proceso de actualizacion de fecha en el control de Acceso. Mensaje [{0}]", ex.Message));
                            ManejarExcepcion(ex);
                        }


                    }

                }
            }
        }

        protected void txtPlaza_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ValidarObjeto(txtPlaza.SelectedValue))
            {
                try
                {
                    Repeticion.DataSource = controlAccesoDao.GetListado(true, plazaDao.GetById(Int32.Parse(txtPlaza.SelectedValue)), DbDao.ControlAccesoDao.Ordenamiento.Nombre);
                    Repeticion.DataBind();
                }
                catch (Exception ex)
                {
                    log.Error(String.Format("Error al intentar obtener el listado de Controles de Acceso para la Plaza: [{0}]. Mensaje: [{1}]", txtPlaza.SelectedValue, ex.Message));
                    ManejarExcepcion(ex);
                }

            }
        }
    }
}