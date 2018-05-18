using System;
using Asistencia.Clases;

namespace Asistencia
{
    public partial class Sistema : PaginaExtensible
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (UsuarioActual != null && UsuarioActual.Perfil_id_perfil != null)
            {
                PanelOficina.Visible = UsuarioActual.Perfil_id_perfil.PermisoCatalogoOficina != null ? UsuarioActual.Perfil_id_perfil.PermisoCatalogoOficina.Value : false;
                PanelEmpleado.Visible = UsuarioActual.Perfil_id_perfil.PermisoCatalogoEmpleado != null ? UsuarioActual.Perfil_id_perfil.PermisoCatalogoEmpleado.Value : false;
                PanelControlAcceso.Visible = UsuarioActual.Perfil_id_perfil.PermisoCatalogoControlAcceso != null ? UsuarioActual.Perfil_id_perfil.PermisoCatalogoControlAcceso.Value : false;
                PanelUsuario.Visible = UsuarioActual.Perfil_id_perfil.PermisoCatalogoUsuario != null ? UsuarioActual.Perfil_id_perfil.PermisoCatalogoUsuario.Value : false;

                try
                {
                    LigaOficina.Text = listaDeOficina().Count.ToString();
                    LigaUsuario.Text = listaDeUsuario().Count.ToString();
                    LigaEmpleado.Text = daoFactory.GetEmpleadoDao().GetConteo().ToString();
                    LigaControlAcceso.Text = daoFactory.GetControlAccesoDao().GetConteo().ToString();
                }
                catch (Exception ex)
                {
                    log.Error(String.Format("Error al intentar obtener los contadores de oficinas y usuarios. Mensaje: [{0}] ", ex.Message));
                    ManejarExcepcion(ex);
                }
            }
        }


    }
}