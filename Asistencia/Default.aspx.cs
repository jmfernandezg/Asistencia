using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Asistencia.Clases;
using log4net;

namespace Asistencia
{

    public partial class _Default : PaginaExtensible
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            /**
            IEmpleadoDao empleadoDao;
            IControlAccesoDao controlAccesoDao;
            IEmpleadoControlAccesoDao empleadoControlAccesoDao;
            IColectorMovimientoIncidenciaDao colectorMovimientoIncidenciaDao;

            controlAccesoDao = daoFactory.GetControlAccesoDao();
            colectorMovimientoIncidenciaDao = daoFactory.GetColectorMovimientosIncidenciaDao();

            ControlAcceso control = controlAccesoDao.GetById(16);

            ColectorMovimientoIncidencia coleMovIncidencia = new ColectorMovimientoIncidencia();
            coleMovIncidencia.ControlAcceso = control;
            coleMovIncidencia.Detalles = "TEST";
            colectorMovimientoIncidenciaDao.Save(coleMovIncidencia);

            try
            {
                empleadoDao = daoFactory.GetEmpleadoDao();
                empleadoControlAccesoDao = daoFactory.GetEmpleadoControlAccesoDao();

                ControlAcceso control = controlAccesoDao.GetById(16);
                Empleado empleado = empleadoDao.GetByNumeroEmpleado(100100);

                log.Error(String.Format("Control Acceso: [{0}], Empleado [{1}]", control.CveControlAcceso,empleado.CveEmpleado));

                if (empleado != null && control != null)
                {
                    EmpleadoControlAcceso eca = empleadoControlAccesoDao.GetByControlAcceso(control, empleado);
                    if (eca == null)
                    {
                        eca = new EmpleadoControlAcceso();
                    }
                    eca.CveEmpleado = empleado.CveEmpleado;
                    eca.CveControlAcceso= control.CveControlAcceso;
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
            **/

        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {

            if (!ValidarObjeto(txtUsername.Text))
            {
                MostrarAdvertencia(Constantes.MENSAJE_CAMPO_REQUERIDO_TITULO, String.Format(Constantes.MENSAJE_CAMPO_REQUERIDO_TEXTO, "Usuario"));
                return;
            }
            if (!ValidarObjeto(txtUsername.Text))
            {
                MostrarAdvertencia(Constantes.MENSAJE_CAMPO_REQUERIDO_TITULO, String.Format(Constantes.MENSAJE_CAMPO_REQUERIDO_TEXTO, "Contraseña"));
                return;
            }

            try
            {
                log.Info(String.Format("Iniciando sesion con usuario [{0}] y contraseña [{1}] y direccion IP [{2}]", txtUsername.Text, txtPasswd.Text, Request.UserHostAddress));
                Usuario usuario = daoFactory.GetUsuarioDao().GetByUsuarioContrasena(txtUsername.Text, txtPasswd.Text);

                if (usuario == null)
                {
                    MostrarAdvertencia("Usuario No Valido", "Usuario o contraseña invalidos");
                    return;
                }
                Session[Constantes.WEB_VARIABLE_SESSION_USUARIO] = usuario;
                Redirigir(Constantes.WEB_PAGINA_SISTEMA);

            }
            catch (Exception ex)
            {
                log.Error(String.Format("Error al intentar iniciar sesión con el usuario. Mensaje: [{0}] ", ex.Message));
                ManejarExcepcion(ex);
            }


        }
    }
}