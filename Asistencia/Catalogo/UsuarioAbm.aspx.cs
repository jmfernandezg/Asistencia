using System;
using Asistencia.Clases;
using PasswordHash;

namespace Asistencia.Catalogo
{
    public partial class UsuarioAbm : PaginaExtensible
    {
        Asistencia.DbInterfase.IUsuarioDao usuarioDao;

        protected void Page_Load(object sender, EventArgs e)
        {
            usuarioDao = daoFactory.GetUsuarioDao();


            if (!IsPostBack)
            {

                txtOficina.Items.Clear();
                txtOficina.Items.Add(NuevoListItem(Constantes.TEXTO_SELECCION, Constantes.TEXTO_BLANCO));
                foreach (DbDominio.Oficina of in listaDeOficina())
                {
                    txtOficina.Items.Add(NuevoListItem(of.Nombre, of.CveOficina.ToString()));
                }

                txtPerfil.Items.Clear();
                txtPerfil.Items.Add(NuevoListItem(Constantes.TEXTO_SELECCION, Constantes.TEXTO_BLANCO));
                foreach (DbDominio.Perfil per in listaDePerfil())
                {
                    txtPerfil.Items.Add(NuevoListItem(per.Nombre, per.Id.ToString()));
                }

                if (Session[Constantes.WEB_VARIABLE_SESSION_ID] == null)
                {
                    Title = "Alta de Registro";
                    txtId.Value = null;
                    txtNombre.Text = null;
                    txtUsername.Text = null;
                    txtPasswd.Text = null;
                    txtEmail.Text = null;

                }
                else
                {
                    try
                    {
                        Asistencia.DbDominio.Usuario obj = usuarioDao.GetById(Int32.Parse(Session[Constantes.WEB_VARIABLE_SESSION_ID].ToString()));
                        Title = "Edición de Registro";
                        txtId.Value = obj.CveUsuario.ToString();
                        txtNombre.Text = obj.Nombre;
                        txtUsername.Text = obj.Username;
                        txtUsername.ReadOnly = true;
                        txtEmail.Text = obj.CorreoElectronico;
                        lbPasswd.Text = "Cambiar Contraseña:";

                        if (obj.Oficina_cve_oficina != null)
                        {
                            txtOficina.SelectedValue = obj.Oficina_cve_oficina.CveOficina.ToString();
                        }
                        if (obj.Perfil_id_perfil != null)
                        {
                            txtPerfil.SelectedValue = obj.Perfil_id_perfil.Id.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        log.Error(String.Format("Error al intentar Obtener los datos Del Usuario a Editar. Mensaje: [{0}] ", ex.Message));
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
                if (!ValidarObjeto(txtUsername.Text))
                {
                    MostrarExcepcion(Constantes.MENSAJE_CAMPO_REQUERIDO_TITULO, "Es requerido capturar el campo Nombre de Usuario");
                    return;
                }
                if (!ValidarObjeto(txtPerfil.SelectedValue))
                {
                    MostrarExcepcion(Constantes.MENSAJE_CAMPO_REQUERIDO_TITULO, "Es requerido seleccionar el campo perfil");
                    return;
                }
                if (!ValidarObjeto(txtOficina.SelectedValue))
                {
                    MostrarExcepcion(Constantes.MENSAJE_CAMPO_REQUERIDO_TITULO, "Es requerido seleccionar el campo planta");
                    return;
                }


                Asistencia.DbDominio.Usuario obj = ValidarObjeto(txtId.Value) ? usuarioDao.GetById(Int32.Parse(txtId.Value)) : null;

                // Validacion de que si es nuevo debe de tener passwd
                if (obj == null && (!ValidarObjeto(txtPasswd.Text)))
                {
                    MostrarExcepcion(Constantes.MENSAJE_CAMPO_REQUERIDO_TITULO, "El campo contraseña es requerido para un registro nuevo");
                    return;
                }

                // Validacion de que si es nuevo, el nombre de usuario no exista en la bd
                DbDominio.Usuario val = (obj == null) ? usuarioDao.GetByUsuario(txtUsername.Text) : null;
                if (val != null)
                {
                    MostrarExcepcion("Error en el proceso", "Este usuario ya se encuentra registrado en la base de datos");
                    return;
                }

                if (obj == null)
                {
                    log.Info(String.Format("Se intenta insertar un registro nuevo de registro de Usuario por el usuario [{0}]", UsuarioActual.Nombre));

                    obj = new DbDominio.Usuario();
                    obj.Usuario_creado_por = UsuarioActual;
                    obj.Loginsql = txtUsername.Text;
                }
                else
                {
                    log.Info(String.Format("Se intenta actualizar el registro de Usuario con ID [{0}] por el usuario [{1}]", txtId.Value, UsuarioActual.Nombre));

                }

                obj.Usuario_modificado_por = UsuarioActual;
                obj.FechaModificacion = DateTime.Now;
                obj.Nombre = txtNombre.Text;
                obj.Username = txtUsername.Text;
                obj.CorreoElectronico = txtEmail.Text;

                if (ValidarObjeto(txtPasswd.Text))
                {
                    obj.Password = Encrypt.MD5(txtPasswd.Text);
                }

                obj.Oficina_cve_oficina = daoFactory.GetOficinaDao().GetById(Int32.Parse(txtOficina.SelectedValue));
                obj.Perfil_id_perfil = daoFactory.GetPerfilDao().GetById(Int32.Parse(txtPerfil.SelectedValue));

                usuarioDao.SaveOrUpdate(obj);
                MostrarExito("Proceso Correcto", "El proceso de guardado se completo con exito");

                if (!ValidarObjeto(txtId.Value))
                {
                    Redirigir(Constantes.WEB_PAGINA_CATALOGO_USUARIO);
                }

            }
            catch (Exception ex)
            {
                log.Error(String.Format("Error al intentar Guardar los datos Del Usuario. Mensaje: [{0}] ", ex.Message));
                ManejarExcepcion(ex);
            }
        }


        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Redirigir(Constantes.WEB_PAGINA_CATALOGO_USUARIO);
        }

    }


}