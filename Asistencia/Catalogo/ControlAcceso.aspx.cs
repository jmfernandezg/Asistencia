using Asistencia.Clases;
using System;
using System.Web.UI.WebControls;

namespace Asistencia.Catalogo
{
    public partial class ControlAcceso : PaginaExtensible
    {
        Asistencia.DbInterfase.IControlAccesoDao controlAccesoDao;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Cargamos los datos
            controlAccesoDao = daoFactory.GetControlAccesoDao();

            log.Info(String.Format("El usuario: [{0}] ha visitado la pagina de Listado de Controles de Acceso", UsuarioActual.Nombre));

            if (!IsPostBack)
            {
                // Se muestra el javascript para el datatable y se carga el listado
                MostrarJavascriptTabla();
                CargarListado();
            }
        }

        public void CargarListado()
        {
            try
            {
                Repeticion.DataSource = controlAccesoDao.GetListado();
                Repeticion.DataBind();

            }
            catch (Exception ex)
            {
                log.Error(String.Format("Error al intentar Obtener El Listado de Controles de Acceso. Mensaje: [{0}] ", ex.Message));
                ManejarExcepcion(ex);

            }
        }

        protected void BtnEditar_Click(object sender, EventArgs e)
        {
            try
            {

                LinkButton b = (LinkButton)sender;

                // validamos el argumento de comando
                if (b != null && b.CommandArgument != null && b.CommandArgument.Length > 0)
                {
                    log.Info(String.Format("El usuario: [{0}] ha presionado el boton para editar el Control de Acceso con Id: [{1}]", UsuarioActual.Nombre, b.CommandArgument));

                    Session[Constantes.WEB_VARIABLE_SESSION_ID] = b.CommandArgument;
                    Redirigir(Constantes.WEB_PAGINA_CATALOGO_CONTROL_ACCESO_ABM);
                }

            }
            catch (Exception ex)
            {
                log.Error(String.Format("Error al intentar Obtener los datos Del Control de Acceso a Editar. Mensaje: [{0}] ", ex.Message));
                ManejarExcepcion(ex);
            }
        }
        protected void BtnNuevo_Click(object sender, EventArgs e)
        {
            log.Info(String.Format("El usuario: [{0}] ha presionado el boton de Nuevo Control de Acceso", UsuarioActual.Nombre));

            Session[Constantes.WEB_VARIABLE_SESSION_ID] = null;
            Redirigir(Constantes.WEB_PAGINA_CATALOGO_CONTROL_ACCESO_ABM);
        }

        protected void BtnBloquear_Click(object sender, EventArgs e)
        {
            try
            {

                LinkButton b = (LinkButton)sender;

                // validamos el argumento de comando
                if (b != null && b.CommandArgument != null && b.CommandArgument.Length > 0)
                {
                    log.Info(String.Format("El usuario: [{0}] ha presionado el boton para Bloquear el Control de Acceso con Id: [{1}]", UsuarioActual.Nombre, b.CommandArgument));

                    Asistencia.DbDominio.ControlAcceso obj = controlAccesoDao.GetById(Int32.Parse(b.CommandArgument));
                    if (obj != null)
                    {
                        obj.Activo = false;
                        controlAccesoDao.SaveOrUpdate(obj);
                        CargarListado();

                    }
                }

            }
            catch (Exception ex)
            {
                log.Error(String.Format("Error al intentar Bloquear el Control de Acceso. Mensaje: [{0}] ", ex.Message));
                ManejarExcepcion(ex);
            }
        }

        protected void BtnDesbloquear_Click(object sender, EventArgs e)
        {
            try
            {

                LinkButton b = (LinkButton)sender;

                // validamos el argumento de comando
                if (b != null && b.CommandArgument != null && b.CommandArgument.Length > 0)
                {
                    log.Info(String.Format("El usuario: [{0}] ha presionado el boton para Desbloquear el Control de Acceso con Id: [{1}]", UsuarioActual.Nombre, b.CommandArgument));

                    Asistencia.DbDominio.ControlAcceso obj = controlAccesoDao.GetById(Int32.Parse(b.CommandArgument));
                    if (obj != null)
                    {
                        obj.Activo = true;
                        controlAccesoDao.SaveOrUpdate(obj);
                        CargarListado();

                    }
                }

            }
            catch (Exception ex)
            {
                log.Error(String.Format("Error al intentar Desbloquear el Control de Acceso. Mensaje: [{0}] ", ex.Message));
                ManejarExcepcion(ex);
            }
        }



        protected void BtnEliminar_Click(object sender, EventArgs e)
        {
            try
            {

                LinkButton b = (LinkButton)sender;

                // validamos el argumento de comando
                if (b != null && b.CommandArgument != null && b.CommandArgument.Length > 0)
                {
                    log.Info(String.Format("El usuario: [{0}] ha presionado el boton para Eliminar el Control de Acceso con Id: [{1}]", UsuarioActual.Nombre, b.CommandArgument));

                    Asistencia.DbDominio.ControlAcceso obj = controlAccesoDao.GetById(Int32.Parse(b.CommandArgument));

                    if (obj != null)
                    {
                        obj.Habilitado = false;
                        controlAccesoDao.SaveOrUpdate(obj);
                        CargarListado();

                    }
                }

            }
            catch (Exception ex)
            {
                log.Error(String.Format("Error al intentar Eliminar el Control de Acceso. Mensaje: [{0}] ", ex.Message));
                ManejarExcepcion(ex);
            }
        }
    }
}