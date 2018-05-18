using Asistencia.Clases;
using System;
using System.Web.UI.WebControls;

namespace Asistencia.Catalogo
{
    public partial class Usuario : PaginaExtensible
    {
        Asistencia.DbInterfase.IUsuarioDao usuarioDao;

        protected void Page_Load(object sender, EventArgs e)
        {
            log.Info(String.Format("El usuario: [{0}] ha visitado la pagina de Listado de Usuarios", UsuarioActual.Nombre));

            usuarioDao = daoFactory.GetUsuarioDao();

            if (!IsPostBack)
            {
                MostrarJavascriptTabla();
                CargarListado();
            }
        }

        public void CargarListado()
        {
            try
            {
                Repeticion.DataSource = usuarioDao.GetListado();
                Repeticion.DataBind();

            }
            catch (Exception ex)
            {
                log.Error(String.Format("Error al intentar Obtener El Listado de Usuarios. Mensaje: [{0}] ", ex.Message));
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
                    log.Info(String.Format("El usuario: [{0}] ha presionado el boton para editar el Usuario con Id: [{1}]", UsuarioActual.Nombre, b.CommandArgument));

                    Session[Constantes.WEB_VARIABLE_SESSION_ID] = b.CommandArgument;
                    Redirigir(Constantes.WEB_PAGINA_CATALOGO_USUARIO_ABM);
                }

            }
            catch (Exception ex)
            {
                log.Error(String.Format("Error al intentar Obtener los datos Del Usuario a Editar. Mensaje: [{0}] ", ex.Message));
                ManejarExcepcion(ex);
            }
        }
        protected void BtnNuevo_Click(object sender, EventArgs e)
        {
            log.Info(String.Format("El usuario: [{0}] ha presionado el boton de Nuevo Usuario", UsuarioActual.Nombre));

            Session[Constantes.WEB_VARIABLE_SESSION_ID] = null;
            Redirigir(Constantes.WEB_PAGINA_CATALOGO_USUARIO_ABM);
        }

        protected void BtnBloquear_Click(object sender, EventArgs e)
        {
            try
            {

                LinkButton b = (LinkButton)sender;

                // validamos el argumento de comando
                if (b != null && b.CommandArgument != null && b.CommandArgument.Length > 0)
                {
                    log.Info(String.Format("El usuario: [{0}] ha presionado el boton para Bloquear el Usuario con Id: [{1}]", UsuarioActual.Nombre, b.CommandArgument));

                    Asistencia.DbDominio.Usuario usr = usuarioDao.GetById(Int32.Parse(b.CommandArgument));
                    if (usr != null)
                    {
                        usr.Activo = false;
                        usuarioDao.SaveOrUpdate(usr);
                        CargarListado();

                    }
                }

            }
            catch (Exception ex)
            {
                log.Error(String.Format("Error al intentar Bloquear el Usuario. Mensaje: [{0}] ", ex.Message));
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
                    log.Info(String.Format("El usuario: [{0}] ha presionado el boton para desbloquear el Usuario con Id: [{1}]", UsuarioActual.Nombre, b.CommandArgument));

                    Asistencia.DbDominio.Usuario usr = usuarioDao.GetById(Int32.Parse(b.CommandArgument));
                    if (usr != null)
                    {
                        usr.Activo = true;
                        usuarioDao.SaveOrUpdate(usr);
                        CargarListado();

                    }
                }

            }
            catch (Exception ex)
            {
                log.Error(String.Format("Error al intentar Desbloquear el Usuario. Mensaje: [{0}] ", ex.Message));
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
                    log.Info(String.Format("El usuario: [{0}] ha presionado el boton para Eliminar el Usuario con Id: [{1}]", UsuarioActual.Nombre, b.CommandArgument));

                    Asistencia.DbDominio.Usuario usr = usuarioDao.GetById(Int32.Parse(b.CommandArgument));
                    if (usr != null)
                    {
                        usr.Habilitado = false;
                        usuarioDao.SaveOrUpdate(usr);
                        CargarListado();

                    }
                }

            }
            catch (Exception ex)
            {
                log.Error(String.Format("Error al intentar Eliminar el Usuario. Mensaje: [{0}] ", ex.Message));
                ManejarExcepcion(ex);
            }
        }


    }
}