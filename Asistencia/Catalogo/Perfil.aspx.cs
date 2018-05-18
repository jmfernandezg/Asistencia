using Asistencia.Clases;
using System;
using System.Web.UI.WebControls;

namespace Asistencia.Catalogo
{

    public partial class Perfil : PaginaExtensible
    {
        IPerfilDao perfilDao;

        protected void Page_Load(object sender, EventArgs e)
        {
            perfilDao = daoFactory.GetPerfilDao();

            if (!IsPostBack)
            {
                log.Info(String.Format("El usuario: [{0}] ha visitado la pagina de Listado de Perfiles", UsuarioActual.Nombre));

                MostrarJavascriptTabla();
                CargarListado();

            }
        }

        public void CargarListado()
        {
            try
            {
                Repeticion.DataSource = perfilDao.GetListado();
                Repeticion.DataBind();

            }
            catch (Exception ex)
            {
                log.Error(String.Format("Error al intentar obtener el listado de Perfiles. Mensaje: [{0}] ", ex.Message));
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
                    log.Info(String.Format("El usuario: [{0}] ha presionado el boton para editar el Perfil con Id: [{1}]", UsuarioActual.Nombre, b.CommandArgument));

                    Session[Constantes.WEB_VARIABLE_SESSION_ID] = b.CommandArgument;
                    Redirigir(Constantes.WEB_PAGINA_CATALOGO_PERFIL_ABM);
                }

            }
            catch (Exception ex)
            {
                log.Error(String.Format("Error al intentar editar el Registro de Perfil. Mensaje: [{0}] ", ex.Message));
                ManejarExcepcion(ex);
            }
        }
        protected void BtnNuevo_Click(object sender, EventArgs e)
        {
            log.Info(String.Format("El usuario: [{0}] ha presionado el boton de Nuevo Perfil", UsuarioActual.Nombre));

            Session[Constantes.WEB_VARIABLE_SESSION_ID] = null;
            Redirigir(Constantes.WEB_PAGINA_CATALOGO_PERFIL_ABM);
        }

    }
}