using Asistencia.Clases;
using PasswordHash;
using System;

namespace Asistencia.Catalogo
{
    public partial class MiPerfil : PaginaExtensible
    {
        IUsuarioDao usuarioDao;

        protected void Page_Load(object sender, EventArgs e)

        {
            usuarioDao = daoFactory.GetUsuarioDao();

            if (!IsPostBack)
            {
                txtNombre.Text = UsuarioActual.Nombre;
                txtUsuario.Text = UsuarioActual.Username;
                txtCorreoElectronico.Text = UsuarioActual.CorreoElectronico;

                log.Info(String.Format("El usuario: [{0}] ha visitado la pagina de Mi Perfil", UsuarioActual.Nombre));

            }

        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {

            log.Info(String.Format("El usuario: [{0}] ha presionado el boton de guardado para modificar su perfil", UsuarioActual.Nombre));

            if (ValidarObjeto(txtCorreoElectronico.Text))
            {
                if (!isValidEmail(txtCorreoElectronico.Text))
                {
                    MostrarAdvertencia(Constantes.MENSAJE_CAMPO_CON_ERROR_TITULO, "La dirección de correo electronico no es valida");
                    return;
                }
            }

            if (ValidarObjeto(txtContrasena1.Text))
            {
                log.Info(String.Format("Se intenta cambiar la contraseña del usuario [{0}] a la siguiente: [{1}] ", UsuarioActual.Nombre, txtContrasena1.Text));

                if (txtContrasena1.Text.Length < 4)
                {
                    MostrarAdvertencia(Constantes.MENSAJE_CAMPO_CON_ERROR_TITULO, "La contraseña debe de ser de por lo menos 4 caracteres");
                    return;
                }

                if (!txtContrasena1.Text.Equals(txtContrasena2.Text))
                {
                    MostrarAdvertencia(Constantes.MENSAJE_CAMPO_CON_ERROR_TITULO, "La contraseña y su confirmación no coinciden");
                    return;
                }
            }

            try
            {
                Asistencia.DbDominio.Usuario usr = usuarioDao.GetByUsuario(UsuarioActual.Username);
                usr.CorreoElectronico = txtCorreoElectronico.Text;

                if (ValidarObjeto(txtContrasena1.Text))
                {
                    usr.Password = Encrypt.MD5(txtContrasena1.Text);
                }


                usr.FechaModificacion = DateTime.Now;
                usuarioDao.SaveOrUpdate(usr);

                MostrarExito("Proceso Correcto", "El proceso de guardado se completo con exito");
                Session[Constantes.WEB_VARIABLE_SESSION_USUARIO] = usr;
            }
            catch (Exception ex)
            {
                log.Error(String.Format("Error al intentar guardar el registro de 'Mi Perfil'. Mensaje: [{0}] ", ex.Message));
                ManejarExcepcion(ex);
            }
        }
    }
}