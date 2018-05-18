using Asistencia.Clases;
using System;
using System.Web.UI.WebControls;


namespace Asistencia.Catalogo
{
    public partial class Oficina : PaginaExtensible
    {
        Asistencia.DbInterfase.IOficinaDao oficinaDao;

        protected void Page_Load(object sender, EventArgs e)
        {
            oficinaDao = daoFactory.GetOficinaDao();

            log.Info(String.Format("El usuario: [{0}] ha visitado la pagina de Listado de Oficinas", UsuarioActual.Nombre));

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
                Repeticion.DataSource = oficinaDao.GetListado();
                Repeticion.DataBind();

            }
            catch (Exception ex)
            {
                log.Error(String.Format("Error al intentar Obtener El Listado de Oficinas. Mensaje: [{0}] ", ex.Message));
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
                    log.Info(String.Format("El usuario: [{0}] ha presionado el boton para editar la Oficina con Id: [{1}]", UsuarioActual.Nombre, b.CommandArgument));

                    Session[Constantes.WEB_VARIABLE_SESSION_ID] = b.CommandArgument;
                    Redirigir(Constantes.WEB_PAGINA_CATALOGO_OFICINA_ABM);
                }

            }
            catch (Exception ex)
            {
                log.Error(String.Format("Error al intentar Obtener los datos de la Oficina a Editar. Mensaje: [{0}] ", ex.Message));
                ManejarExcepcion(ex);
            }
        }
        protected void BtnNuevo_Click(object sender, EventArgs e)
        {
            log.Info(String.Format("El usuario: [{0}] ha presionado el boton de Nueva Oficina", UsuarioActual.Nombre));

            Session[Constantes.WEB_VARIABLE_SESSION_ID] = null;
            Redirigir(Constantes.WEB_PAGINA_CATALOGO_OFICINA_ABM);
        }

        protected void BtnBloquear_Click(object sender, EventArgs e)
        {
            try
            {

                LinkButton b = (LinkButton)sender;

                // validamos el argumento de comando
                if (b != null && b.CommandArgument != null && b.CommandArgument.Length > 0)
                {
                    log.Info(String.Format("El usuario: [{0}] ha presionado el boton para Bloquear la Oficina con Id: [{1}]", UsuarioActual.Nombre, b.CommandArgument));

                    Asistencia.DbDominio.Oficina obj = oficinaDao.GetById(Int32.Parse(b.CommandArgument));
                    if (obj != null)
                    {
                        obj.Activo = false;
                        oficinaDao.SaveOrUpdate(obj);
                        CargarListado();

                    }
                }

            }
            catch (Exception ex)
            {
                log.Error(String.Format("Error al intentar Bloquear la Oficina. Mensaje: [{0}] ", ex.Message));
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
                    log.Info(String.Format("El usuario: [{0}] ha presionado el boton para Desbloquear la Oficina con Id: [{1}]", UsuarioActual.Nombre, b.CommandArgument));

                    Asistencia.DbDominio.Oficina obj = oficinaDao.GetById(Int32.Parse(b.CommandArgument));
                    if (obj != null)
                    {
                        obj.Activo = true;
                        oficinaDao.SaveOrUpdate(obj);
                        CargarListado();

                    }
                }

            }
            catch (Exception ex)
            {
                log.Error(String.Format("Error al intentar desbloquear la Oficina. Mensaje: [{0}] ", ex.Message));
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
                    log.Info(String.Format("El usuario: [{0}] ha presionado el boton para Eliminar la Oficina con Id: [{1}]", UsuarioActual.Nombre, b.CommandArgument));

                    Asistencia.DbDominio.Oficina obj = oficinaDao.GetById(Int32.Parse(b.CommandArgument));
                    if (obj != null)
                    {
                        obj.Habilitado = false;
                        oficinaDao.SaveOrUpdate(obj);
                        CargarListado();

                    }
                }

            }
            catch (Exception ex)
            {
                log.Error(String.Format("Error al intentar Eliminar la Oficina. Mensaje: [{0}] ", ex.Message));
                ManejarExcepcion(ex);
            }
        }
    }
}