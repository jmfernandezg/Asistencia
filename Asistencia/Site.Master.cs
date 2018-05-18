using System;
using System.Web.UI;
using Asistencia.Clases;
using System.Configuration;

namespace Asistencia
{
    public partial class SiteMaster : MasterPage
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            if (UsuarioActual != null && UsuarioActual.Perfil_id_perfil != null)
            {
                linkCatalogoControlACceso.Visible = UsuarioActual.Perfil_id_perfil.PermisoCatalogoControlAcceso != null ? UsuarioActual.Perfil_id_perfil.PermisoCatalogoControlAcceso.Value : false;
                linkCatalogoEmpleado.Visible = UsuarioActual.Perfil_id_perfil.PermisoCatalogoEmpleado != null ? UsuarioActual.Perfil_id_perfil.PermisoCatalogoEmpleado.Value : false;
                linkCatalogoPerfil.Visible = UsuarioActual.Perfil_id_perfil.PermisoCatalogoPerfil != null ? UsuarioActual.Perfil_id_perfil.PermisoCatalogoPerfil.Value : false;
                linkCatalogoPlanta.Visible = UsuarioActual.Perfil_id_perfil.PermisoCatalogoOficina != null ? UsuarioActual.Perfil_id_perfil.PermisoCatalogoOficina.Value : false;
                linkCatalogoUsuario.Visible = UsuarioActual.Perfil_id_perfil.PermisoCatalogoUsuario != null ? UsuarioActual.Perfil_id_perfil.PermisoCatalogoUsuario.Value : false;

                linkProcActualizarFecha.Visible = UsuarioActual.Perfil_id_perfil.PermisoProcesoActualizarFechaEnControl != null ? UsuarioActual.Perfil_id_perfil.PermisoProcesoActualizarFechaEnControl.Value : false;
                //                linkProcEmpleadoEnControl.Visible = UsuarioActual.Perfil_id_perfil.PermisoProcesoEmpleadoEnControl != null ? UsuarioActual.Perfil_id_perfil.PermisoProcesoEmpleadoEnControl.Value : false;
                linkProcCargaMasivaEmpleado.Visible = UsuarioActual.Perfil_id_perfil.PermisoProcesoCargaMasivaEmpleado != null ? UsuarioActual.Perfil_id_perfil.PermisoProcesoCargaMasivaEmpleado.Value : false;
                linkProcCargaMasivaOficina.Visible = UsuarioActual.Perfil_id_perfil.PermisoProcesoCargaMasivaPlantilla != null ? UsuarioActual.Perfil_id_perfil.PermisoProcesoCargaMasivaPlantilla.Value : false;
                linkProcPlantilla.Visible = UsuarioActual.Perfil_id_perfil.PermisoProcesoPlantilla != null ? UsuarioActual.Perfil_id_perfil.PermisoProcesoPlantilla.Value : false;
                linkProcCargaPlantillaEnControl.Visible = UsuarioActual.Perfil_id_perfil.PermisoProcesoCargaPlantillaEnControl != null ? UsuarioActual.Perfil_id_perfil.PermisoProcesoCargaPlantillaEnControl.Value : false;
                linkProcCargaAsistencia.Visible = UsuarioActual.Perfil_id_perfil.PermisoProcesoCargaAsistencia != null ? UsuarioActual.Perfil_id_perfil.PermisoProcesoCargaAsistencia.Value : false;

                linkProcEmpleadoEnControl.Visible = false;

                linkReporteTareas.NavigateUrl = "~" + ConfigurationManager.AppSettings[Constantes.CONFIG_WEB_TABLERO_PROCESOS];
                linkReporteTareas.Visible = UsuarioActual.EsAdmin != null ? UsuarioActual.EsAdmin.Value : false;

                linkReporteAsistencia.Visible = UsuarioActual.Perfil_id_perfil.PermisoReporteAsistencia != null ? UsuarioActual.Perfil_id_perfil.PermisoReporteAsistencia.Value : false;

                linkReporteEmpleadoNoRegistrado.Visible = UsuarioActual.Perfil_id_perfil.PermisoReporteEmpleadoNoRegistrado != null ? UsuarioActual.Perfil_id_perfil.PermisoReporteEmpleadoNoRegistrado.Value : false;

            }

        }


        public Usuario UsuarioActual
        {
            get
            {
                return (Usuario)Session[Constantes.WEB_VARIABLE_SESSION_USUARIO];
            }
        }


        protected void BntMiPerfil_Click(object sender, EventArgs e)
        {
            Response.Redirect(Constantes.WEB_PAGINA_CATALOGO_MI_PERFIL);

        }

        protected void BtnCerrarSession_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect(Constantes.WEB_PAGINA_INICIO_SESION);

        }
    }

}