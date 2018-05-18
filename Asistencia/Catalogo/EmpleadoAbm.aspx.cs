using System;
using Asistencia.Clases;


namespace Asistencia.Catalogo
{
    public partial class EmpleadoAbm : PaginaExtensible
    {
        Asistencia.DbInterfase.IEmpleadoDao empleadoDao;

        protected void Page_Load(object sender, EventArgs e)
        {
            empleadoDao = daoFactory.GetEmpleadoDao();


            if (!IsPostBack)
            {

                txtPlaza.Items.Clear();
                txtPlaza.Items.Add(NuevoListItem(Constantes.TEXTO_SELECCION, Constantes.TEXTO_BLANCO));
                foreach (DbDominio.Plaza obj in listaDePlaza())
                {
                    txtPlaza.Items.Add(NuevoListItem(obj.Nombre, obj.Id.ToString()));
                }

                if (Session[Constantes.WEB_VARIABLE_SESSION_ID] == null)
                {
                    Title = "Alta de Registro";
                    txtId.Value = null;
                    txtNombre.Text = null;
                    txtNumeroNomina = null;

                }
                else
                {
                    try
                    {
                        Asistencia.DbDominio.Empleado obj = empleadoDao.GetById(Int32.Parse(Session[Constantes.WEB_VARIABLE_SESSION_ID].ToString()));
                        Title = "Edición de Registro";
                        txtId.Value = obj.CveEmpleado.ToString();
                        txtNombre.Text = obj.Nombre;
                        txtNumeroNomina.Text = obj.NoEmpleado.ToString();


                        if (obj.Plaza != null)
                        {
                            txtPlaza.SelectedValue = obj.Plaza.Id.ToString();
                        }

                    }
                    catch (Exception ex)
                    {
                        log.Error(String.Format("Error al intentar Obtener los datos Del Empleado a Editar. Mensaje: [{0}] ", ex.Message));
                        ManejarExcepcion(ex);
                    }

                }
            }

        }


        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {

                if (!ValidarObjeto(txtNumeroNomina.Text))
                {
                    MostrarExcepcion(Constantes.MENSAJE_CAMPO_REQUERIDO_TITULO, "Es requerido seleccionar el campo Numero Nomina");
                    return;
                }

                if (!ValidarObjeto(txtNombre.Text))
                {
                    MostrarExcepcion(Constantes.MENSAJE_CAMPO_REQUERIDO_TITULO, "Es requerido seleccionar el campo Codigo Nombre");
                    return;
                }


                if (!ValidarObjeto(txtPlaza.SelectedValue))
                {
                    MostrarExcepcion(Constantes.MENSAJE_CAMPO_REQUERIDO_TITULO, "Es requerido seleccionar el campo plaza");
                    return;
                }


                Asistencia.DbDominio.Empleado obj = ValidarObjeto(txtId.Value) ? empleadoDao.GetById(Int32.Parse(txtId.Value)) : null;

                if (obj == null)
                {
                    log.Info(String.Format("Se intenta insertar un registro nuevo de registro de Empleado por el usuario [{0}]", UsuarioActual.Nombre));

                    obj = new DbDominio.Empleado();
                    obj.Usuario_creado_por = UsuarioActual;
                    obj.Usuario_cve_usuario_alta = UsuarioActual;
                }
                else
                {
                    log.Info(String.Format("Se intenta actualizar el registro de Empleado con ID [{0}] por el usuario [{1}]", txtId.Value, UsuarioActual.Nombre));
                }

                obj.Usuario_modificado_por = UsuarioActual;
                obj.FechaModificacion = DateTime.Now;
                obj.Nombre = txtNombre.Text;
                obj.NoEmpleado = Int32.Parse(txtNumeroNomina.Text);

                obj.Plaza = daoFactory.GetPlazaDao().GetById(Int32.Parse(txtPlaza.SelectedValue));

                empleadoDao.SaveOrUpdate(obj);
                MostrarExito("Proceso Correcto", "El proceso de guardado se completo con exito");
                log.Info("El registro se ha guardado exitosamente en la base de datos");

                if (!ValidarObjeto(txtId.Value))
                {
                    Redirigir(Constantes.WEB_PAGINA_CATALOGO_EMPLEADO);
                }

            }
            catch (Exception ex)
            {
                log.Error(String.Format("Error al intentar Guardar los datos Del Empleado. Mensaje: [{0}] ", ex.Message));
                ManejarExcepcion(ex);
            }
        }


        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Redirigir(Constantes.WEB_PAGINA_CATALOGO_EMPLEADO);
        }
    }
}