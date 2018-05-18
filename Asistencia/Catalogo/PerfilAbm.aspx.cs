using System;
using Asistencia.Clases;

namespace Asistencia.Catalogo
{
    public partial class PerfilAbm : PaginaExtensible
    {
        Asistencia.DbInterfase.IPerfilDao perfilDao;

        protected void Page_Load(object sender, EventArgs e)
        {
            perfilDao = daoFactory.GetPerfilDao();

            if (!IsPostBack)
            {
                if (Session[Constantes.WEB_VARIABLE_SESSION_ID] == null)
                {
                    Title = "Alta de Registro";
                    txtId.Value = null;
                    txtNombre.Text = null;
                    txtDescripcion.Text = null;

                    CheckCatalogoPerfiles.Checked = false;
                    CheckCatalogoEmpleados.Checked = false;
                    CheckCatalogoOficinas.Checked = false;
                    CheckCatalogoControlAcceso.Checked = false;
                    CheckCatalogoUsuarios.Checked = false;

                    CheckProcesoActualizarFechaEnControl.Checked = false;
                    CheckProcesoCargaMasivaEmpleado.Checked = false;
                    CheckProcesoCargaMasivaPlanta.Checked = false;
                    CheckProcesoCargaPlantillaEnControles.Checked = false;
                    CheckProcesoEmpleadoEnControles.Checked = false;
                    CheckProcesoPlantilla.Checked = false;
                    CheckProcesoCargaAsistencia.Checked = false;

                    CheckPermisoReporteAsistencia.Checked = false;
                    CheckPermisoReporteEmpleadoNoRegistrado.Checked = false;
                }
                else
                {
                    try
                    {
                        Asistencia.DbDominio.Perfil perf = perfilDao.GetById(Int32.Parse(Session[Constantes.WEB_VARIABLE_SESSION_ID].ToString()));
                        Title = "Edición de Registro";
                        txtId.Value = perf.Id.ToString();
                        txtNombre.Text = perf.Nombre;
                        txtDescripcion.Text = perf.Descripcion;

                        CheckCatalogoPerfiles.Checked = perf.PermisoCatalogoPerfil != null ? perf.PermisoCatalogoPerfil.Value : false;
                        CheckCatalogoEmpleados.Checked = perf.PermisoCatalogoEmpleado != null ? perf.PermisoCatalogoEmpleado.Value : false;
                        CheckCatalogoOficinas.Checked = perf.PermisoCatalogoOficina != null ? perf.PermisoCatalogoOficina.Value : false;
                        CheckCatalogoControlAcceso.Checked = perf.PermisoCatalogoControlAcceso != null ? perf.PermisoCatalogoControlAcceso.Value : false;
                        CheckCatalogoUsuarios.Checked = perf.PermisoCatalogoUsuario != null ? perf.PermisoCatalogoUsuario.Value : false;

                        CheckProcesoCargaMasivaEmpleado.Checked = perf.PermisoProcesoCargaMasivaEmpleado != null ? perf.PermisoProcesoCargaMasivaEmpleado.Value : false;
                        CheckProcesoCargaMasivaPlanta.Checked = perf.PermisoProcesoCargaMasivaPlantilla != null ? perf.PermisoProcesoCargaMasivaPlantilla.Value : false;
                        CheckProcesoPlantilla.Checked = perf.PermisoProcesoPlantilla != null ? perf.PermisoProcesoPlantilla.Value : false;
                        CheckProcesoEmpleadoEnControles.Checked = perf.PermisoProcesoEmpleadoEnControl != null ? perf.PermisoProcesoEmpleadoEnControl.Value : false;
                        CheckProcesoCargaPlantillaEnControles.Checked = perf.PermisoProcesoCargaPlantillaEnControl != null ? perf.PermisoProcesoCargaPlantillaEnControl.Value : false;
                        CheckProcesoActualizarFechaEnControl.Checked = perf.PermisoProcesoActualizarFechaEnControl != null ? perf.PermisoProcesoActualizarFechaEnControl.Value : false;
                        CheckProcesoCargaAsistencia.Checked = perf.PermisoProcesoCargaAsistencia != null ? perf.PermisoProcesoCargaAsistencia.Value : false;

                        CheckPermisoReporteAsistencia.Checked = perf.PermisoReporteAsistencia != null ? perf.PermisoReporteAsistencia.Value : false;
                        CheckPermisoReporteEmpleadoNoRegistrado.Checked = perf.PermisoReporteEmpleadoNoRegistrado != null ? perf.PermisoReporteEmpleadoNoRegistrado.Value : false;




                    }
                    catch (Exception ex)
                    {
                        log.Error(String.Format("Error al intentar Obtener los datos Del Perfil a Editar. Mensaje: [{0}] ", ex.Message));
                        ManejarExcepcion(ex);
                    }

                }
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarObjeto(txtNombre.Text))
                {
                    MostrarExcepcion(Constantes.MENSAJE_CAMPO_REQUERIDO_TITULO, "Es requerido capturar el campo Nombre");
                    return;
                }

                Asistencia.DbDominio.Perfil perf = ValidarObjeto(txtId.Value) ? perfilDao.GetById(Int32.Parse(txtId.Value)) : null;
                if (perf == null)
                {
                    log.Info(String.Format("Se intenta insertar un registro nuevo de registro de Perfil por el usuario [{0}]", UsuarioActual.Nombre));

                    perf = new DbDominio.Perfil();
                    perf.Usuario_creado_por = UsuarioActual;
                }
                else
                {
                    log.Info(String.Format("Se intenta actualizar el registro de Perfil con ID [{0}] por el usuario [{1}]", txtId.Value, UsuarioActual.Nombre));
                }

                perf.Nombre = txtNombre.Text;
                perf.Descripcion = txtDescripcion.Text;
                perf.Usuario_modificado_por = UsuarioActual;
                perf.FechaModificacion = DateTime.Now;

                perf.PermisoCatalogoPerfil = CheckCatalogoPerfiles.Checked;
                perf.PermisoCatalogoEmpleado = CheckCatalogoEmpleados.Checked;
                perf.PermisoCatalogoOficina = CheckCatalogoOficinas.Checked;
                perf.PermisoCatalogoControlAcceso = CheckCatalogoControlAcceso.Checked;
                perf.PermisoCatalogoUsuario = CheckCatalogoUsuarios.Checked;

                perf.PermisoProcesoActualizarFechaEnControl = CheckProcesoActualizarFechaEnControl.Checked;
                perf.PermisoProcesoCargaMasivaEmpleado = CheckProcesoCargaMasivaEmpleado.Checked;
                perf.PermisoProcesoCargaMasivaPlantilla = CheckProcesoCargaMasivaPlanta.Checked;
                perf.PermisoProcesoCargaPlantillaEnControl = CheckProcesoCargaPlantillaEnControles.Checked;
                perf.PermisoProcesoEmpleadoEnControl = CheckProcesoEmpleadoEnControles.Checked;
                perf.PermisoProcesoPlantilla = CheckProcesoPlantilla.Checked;
                perf.PermisoProcesoCargaAsistencia = CheckProcesoCargaAsistencia.Checked;

                perf.PermisoReporteAsistencia = CheckPermisoReporteAsistencia.Checked;
                perf.PermisoReporteEmpleadoNoRegistrado = CheckPermisoReporteEmpleadoNoRegistrado.Checked;

                perfilDao.SaveOrUpdate(perf);
                MostrarExito("Proceso Correcto", "El proceso de guardado se completo con exito");

                if (!ValidarObjeto(txtId.Value))
                {
                    Redirigir(Constantes.WEB_PAGINA_CATALOGO_PERFIL);
                }
            }
            catch (Exception ex)
            {
                log.Error(String.Format("Error al intentar Guardar los datos Del Perfil. Mensaje: [{0}] ", ex.Message));
                ManejarExcepcion(ex);
            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Redirigir(Constantes.WEB_PAGINA_CATALOGO_PERFIL);
        }
    }
}
