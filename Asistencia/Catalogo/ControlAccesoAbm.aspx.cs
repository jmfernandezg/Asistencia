using System;
using Asistencia.Clases;



namespace Asistencia.Catalogo
{
    public partial class ControlAccesoAbm : PaginaExtensible
    {
        Asistencia.DbInterfase.IControlAccesoDao controlAccesoDao;

        protected void Page_Load(object sender, EventArgs e)
        {
            controlAccesoDao = daoFactory.GetControlAccesoDao();

            if (!IsPostBack)
            {

                txtOficina.Items.Clear();
                txtOficina.Items.Add(NuevoListItem(Constantes.TEXTO_SELECCION, Constantes.TEXTO_BLANCO));
                foreach (DbDominio.Oficina of in listaDeOficina())
                {
                    txtOficina.Items.Add(NuevoListItem(of.Nombre, of.CveOficina.ToString()));
                }

                if (Session[Constantes.WEB_VARIABLE_SESSION_ID] == null)
                {
                    Title = "Alta de Registro";
                    txtId.Value = null;
                    txtIdControl.Text = (controlAccesoDao.GetMaxIdControl() + 1).ToString();
                    txtNombre.Text = null;
                    txtMarca.Text = null;
                    txtModelo.Text = null;
                    txtVersionFirmware.Text = null;
                    txtCapacidadEnHuellas.Text = null;
                    txtDireccionIP.Text = null;
                    txtPuerto.Text = null;
                    txtOficina.SelectedValue = null;

                }
                else
                {
                    try
                    {
                        Asistencia.DbDominio.ControlAcceso obj = controlAccesoDao.GetById(Int32.Parse(Session[Constantes.WEB_VARIABLE_SESSION_ID].ToString()));
                        Title = "Edición de Registro";
                        txtId.Value = obj.CveControlAcceso.ToString();
                        txtNombre.Text = obj.Nombre;
                        txtIdControl.Text = obj.IdControl.ToString();
                        txtIdControl.ReadOnly = true;
                        txtMarca.Text = obj.Marca;
                        txtModelo.Text = obj.Modelo;
                        txtVersionFirmware.Text = obj.VersionFirmware;
                        txtCapacidadEnHuellas.Text = obj.CapacidadHuellas.ToString();
                        txtDireccionIP.Text = obj.DireccionIp;
                        txtPuerto.Text = obj.Puerto.ToString();
                        txtOficina.SelectedValue = null;


                        if (obj.Oficina != null)
                        {
                            txtOficina.SelectedValue = obj.Oficina.CveOficina.ToString();
                        }

                    }
                    catch (Exception ex)
                    {
                        log.Error(String.Format("Error al intentar Obtener los datos Del Control de Acceso a Editar. Mensaje: [{0}] ", ex.Message));
                        ManejarExcepcion(ex);
                    }

                }
            }

        }


        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {

                if (!ValidarObjeto(txtIdControl.Text))
                {
                    MostrarExcepcion(Constantes.MENSAJE_CAMPO_REQUERIDO_TITULO, "Es requerido seleccionar el campo ID Control");
                    return;
                }

                if (!ValidarObjeto(txtNombre.Text))
                {
                    MostrarExcepcion(Constantes.MENSAJE_CAMPO_REQUERIDO_TITULO, "Es requerido seleccionar el campo Nombre");
                    return;
                }


                if (!ValidarObjeto(txtCapacidadEnHuellas.Text))
                {
                    txtCapacidadEnHuellas.Text = "10000";
                }


                if (!ValidarObjeto(txtOficina.SelectedValue))
                {
                    MostrarExcepcion(Constantes.MENSAJE_CAMPO_REQUERIDO_TITULO, "Es requerido seleccionar el campo Planta");
                    return;
                }

                if (!ValidarObjeto(txtDireccionIP.Text))
                {
                    MostrarExcepcion(Constantes.MENSAJE_CAMPO_REQUERIDO_TITULO, "Es requerido seleccionar el campo Direccion IP");
                    return;
                }

                if (!ValidarObjeto(txtPuerto.Text))
                {
                    txtPuerto.Text = "4370";
                }



                Asistencia.DbDominio.ControlAcceso obj = ValidarObjeto(txtId.Value) ? controlAccesoDao.GetById(Int32.Parse(txtId.Value)) : null;

                if (obj == null)
                {
                    log.Info(String.Format("Se intenta insertar un registro nuevo de registro de Control de Acceso por el usuario [{0}]", UsuarioActual.Nombre));
                    obj = new DbDominio.ControlAcceso();
                    obj.Usuario_cve_usuario_alta = UsuarioActual;
                }
                else
                {
                    log.Info(String.Format("Se intenta actualizar el registro de Control de Acceso con ID [{0}] por el usuario [{1}]", txtId.Value, UsuarioActual.Nombre));
                }

                obj.IdControl = Int32.Parse(txtIdControl.Text);
                obj.Nombre = txtNombre.Text;

                obj.Oficina = daoFactory.GetOficinaDao().GetById(Int32.Parse(txtOficina.SelectedValue));

                obj.DireccionIp = txtDireccionIP.Text;
                obj.Puerto = short.Parse(txtPuerto.Text);
                obj.Marca = txtMarca.Text;
                obj.Modelo = txtModelo.Text;
                obj.VersionFirmware = txtVersionFirmware.Text;
                obj.CapacidadHuellas = Int32.Parse(txtCapacidadEnHuellas.Text);


                obj.Usuario_modificado_por = UsuarioActual;
                obj.FechaModificacion = DateTime.Now;


                controlAccesoDao.SaveOrUpdate(obj);
                MostrarExito("Proceso Correcto", "El proceso de guardado se completo con exito");
                log.Info("El registro se ha guardado exitosamente en la base de datos");

                if (!ValidarObjeto(txtId.Value))
                {
                    Redirigir(Constantes.WEB_PAGINA_CATALOGO_CONTROL_ACCESO);
                }



            }
            catch (Exception ex)
            {
                log.Error(String.Format("Error al intentar Guardar los datos Del Control de Acceso. Mensaje: [{0}] ", ex.Message));
                ManejarExcepcion(ex);
            }
        }


        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Redirigir(Constantes.WEB_PAGINA_CATALOGO_CONTROL_ACCESO);
        }
    }
}