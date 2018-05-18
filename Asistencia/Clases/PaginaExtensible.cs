using System;
using System.Collections.Generic;
using System.Web.UI;
using System.IO;
using log4net;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Text;
using System.Net;

namespace Asistencia.Clases
{
    public class PaginaExtensible : Page
    {

        // Logger
        public readonly ILog log = LogManager.GetLogger(Constantes.LOG_ASISTENCIA_APP);
        // Fabrica de Objetos de Acceso a Datos (DAO)
        public readonly IDaoFactory daoFactory = new NHibernateDaoFactory();


        protected override void OnInit(EventArgs e)
        {
            if (Session[Constantes.WEB_VARIABLE_SESSION_USUARIO] == null && !Request.Path.Contains("Default"))
            {
                Redirigir(Constantes.WEB_PAGINA_INICIO_SESION);
            }
        }


        public List<Plaza> listaDePlaza()
        {
            try
            {
                return daoFactory.GetPlazaDao().GetListado();

            }
            catch (Exception ex)
            {
                log.Error(String.Format("Error al intentar Obtener El Listado de Plazas. Mensaje: [{0}] ", ex.Message));
                ManejarExcepcion(ex);
                return new List<Plaza>();
            }
        }

        public List<Region> listaDeRegion()
        {
            try
            {
                return daoFactory.GetRegionDao().GetListado();
            }
            catch (Exception ex)
            {
                log.Error(String.Format("Error al intentar Obtener El Listado de Regiones. Mensaje: [{0}] ", ex.Message));
                ManejarExcepcion(ex);
                return new List<Region>();
            }
        }

        public List<Zona> listaDeZona()
        {
            try
            {
                return daoFactory.GetZonaDao().GetListado();

            }
            catch (Exception ex)
            {

                log.Error(String.Format("Error al intentar Obtener El Listado de Zonas. Mensaje: [{0}] ", ex.Message));
                ManejarExcepcion(ex);
                return new List<Zona>();
            }
        }

        public List<Usuario> listaDeUsuario()
        {
            try
            {
                return daoFactory.GetUsuarioDao().GetListado();

            }
            catch (Exception ex)
            {

                log.Error(String.Format("Error al intentar Obtener El Listado de Usuarios. Mensaje: [{0}] ", ex.Message));
                ManejarExcepcion(ex);
                return new List<Usuario>();
            }
        }

        public List<Perfil> listaDePerfil()
        {
            try
            {
                return daoFactory.GetPerfilDao().GetListado();

            }
            catch (Exception ex)
            {

                log.Error(String.Format("Error al intentar Obtener El Listado de Perfiles. Mensaje: [{0}] ", ex.Message));
                ManejarExcepcion(ex);
                return new List<Perfil>();
            }
        }

        public List<Oficina> listaDeOficina()
        {
            try
            {
                return daoFactory.GetOficinaDao().GetListado();

            }
            catch (Exception ex)
            {
                log.Error(String.Format("Error al intentar Obtener El Listado de Oficinas. Mensaje: [{0}] ", ex.Message));
                ManejarExcepcion(ex);
                return new List<Oficina>();
            }
        }

        public String NombreCompleto
        {
            get
            {
                return UsuarioActual != null ? UsuarioActual.Nombre : null;
            }
        }

        public Usuario UsuarioActual
        {
            get
            {
                return (Usuario)Session[Constantes.WEB_VARIABLE_SESSION_USUARIO];
            }
        }

        public void Redirigir(String pagina)
        {
            Response.Redirect(pagina, false);
            Context.ApplicationInstance.CompleteRequest();

        }

        public void MostrarJavascriptTabla()
        {
            Panel js = (Panel)Master.FindControl(Constantes.WEB_PANEL_JAVASCRIPT_TABLA);
            if (js != null)
            {
                js.Visible = true;
            }
        }

        protected void BntMiPerfil_Click(object sender, EventArgs e)
        {
            Redirigir(Constantes.WEB_PAGINA_CATALOGO_MI_PERFIL);

        }

        protected void BtnCerrarSession_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Redirigir(Constantes.WEB_PAGINA_INICIO_SESION);

        }

        public void ManejarExcepcion(Exception ex)
        {
            log.Error(ex);
            MostrarExcepcion("Error en el Proceso", String.Format("Se encontro un error en el proceso. Detalles: [{0}]", ex.GetBaseException().Message));

        }

        /// <summary>Método que envia un correo electronico</summary>
        ///<param name="aQuien">String que nos indica la direccion de correo electronico a quien se le enviara</param>
        ///<param name="asunto">String que nos indica el asunto del correo electronico</param>
        ///<param name="attach">Diccionario de arreglo de bytes que nos indica en modo Llave/Valor los archivos adjuntos para este correo.
        /// La llave sera usada como nombre del archivo, el arreglo binario sera el valor, es decir, el archivo adjunto</param>
        /// <param name="cc">String que nos indica si el correo sera con copia para alguien</param>
        /// <param name="mensaje">String que nos indica el mensaje</param>
        /// <remarks> Este metodo utiliza varias configuraciones del aplicativo, definidas en web.config:
        /// FROM: Si DireccionDeCorreoFrom esta presente en el NVC: se tomara esta direccion de correo de FROM. Si no, se utilizara desde Web.Config el parametro "mailFrom"
        /// PORT: Se utilizara el puerto del servidor de correos definido en  Web.Config "mailPort"
        /// HOST: Se utilizara el host o direccion del servidor de correos definido en Web.Config "mailHost"
        /// BCC: Se utilizara el BCC definido en Web.Config, parametro "mailBCC"
        /// USERNAME: Se utilizara el username definido en Web.Config, parametro "mailUsername"
        /// PASSWORD: Se utilizara el password definido en Web.Config, parametro "mailPasswd"
        /// SSL: Se utilizara el password definido en Web.Config, parametro "mailSSL"
        /// </remarks>

        public void EnviarEmail(String aQuien, String cc, String asunto, String mensaje, Dictionary<String, byte[]> attach)
        {
            log.Info("//*****************************************************//");
            log.Info("//***** INICIA ENVIO DE CORREO ELECTRONICO      ******//");
            log.Info("//*****************************************************//");

            log.Info(String.Format("//----> Correo Para: [{0}] ", aQuien));
            log.Info(String.Format("//----> Correo CC: [{0}]", cc));
            log.Info(String.Format("//----> Correo Asunto: [{0}]", asunto));
            log.Info(String.Format("//----> Correo Mensaje:  [{0}]", mensaje));


            try
            {

                // Cliente de Correo Electronico

                SmtpClient sender = new SmtpClient();
                log.Info(String.Format("//----> Envio de Correo: Server: Puerto. Se utilizara el puerto configurado: [{0}] ", System.Configuration.ConfigurationManager.AppSettings["mailPort"]));
                sender.Port = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings[Constantes.CONFIG_MAIL_PORT]);

                // Server y asunto
                log.Info(String.Format("//----> Envio de Correo: Server: Host. Se utilizara el host configurado: [{0}] ", System.Configuration.ConfigurationManager.AppSettings["mailHost"]));
                sender.Host = System.Configuration.ConfigurationManager.AppSettings[Constantes.CONFIG_MAIL_HOST];

                // SSL
                log.Info(String.Format("//----> Envio de Correo: Server: SSL. Se utilizara SSL?: [{0}]", System.Configuration.ConfigurationManager.AppSettings["mailSSL"]));
                sender.EnableSsl = Boolean.Parse(System.Configuration.ConfigurationManager.AppSettings[Constantes.CONFIG_MAIL_SSL]);


                // UserName
                if (ValidarObjeto(System.Configuration.ConfigurationManager.AppSettings[Constantes.CONFIG_MAIL_USERNAME]) && ValidarObjeto(System.Configuration.ConfigurationManager.AppSettings[Constantes.CONFIG_MAIL_PASSWD]))
                {
                    log.Info(String.Format("//----> Envio de Correo: Server: Username. Se utilizara el username configurado: [{0}] y contraseña [{1}]", System.Configuration.ConfigurationManager.AppSettings["mailUsername"], System.Configuration.ConfigurationManager.AppSettings["mailPasswd"]));
                    sender.Credentials = new NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["mailUsername"], System.Configuration.ConfigurationManager.AppSettings["mailPasswd"]);
                }


                sender.Timeout = 30000;
                sender.DeliveryMethod = SmtpDeliveryMethod.Network;

                MailMessage msg = new MailMessage();
                msg.BodyEncoding = UTF8Encoding.UTF8;
                msg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                msg.From = new MailAddress(System.Configuration.ConfigurationManager.AppSettings[Constantes.CONFIG_MAIL_FROM]);

                // To
                log.Info(String.Format("//----> Envio de Correo: TO: El correo se enviara principamente a: [{0}]", aQuien));
                msg.To.Add(new MailAddress(aQuien));


                // Subject
                log.Info(String.Format("//----> Envio de Correo: Asunto: El asunto de este correo es: [{0}]", asunto));
                msg.Subject = asunto;

                //Body 
                msg.Body = mensaje;

                // CC
                if (ValidarObjeto(cc))
                {
                    log.Info(String.Format("//----> Envio de Correo: CC: El correo se enviara con copia a: [{0}]", cc));
                    msg.CC.Add(new MailAddress(cc));
                }

                // BCC
                if (ValidarObjeto(System.Configuration.ConfigurationManager.AppSettings[Constantes.CONFIG_MAIL_BCC]))
                {
                    log.Info(String.Format("//----> Envio de Correo: BCC: El correo se enviara con copia a: [{0}]", System.Configuration.ConfigurationManager.AppSettings["mailBCC"]));
                    msg.Bcc.Add(new MailAddress(System.Configuration.ConfigurationManager.AppSettings["mailBCC"]));
                }




                // Attachments
                if (attach != null)
                {
                    foreach (KeyValuePair<String, byte[]> key in attach)
                    {
                        msg.Attachments.Add(new System.Net.Mail.Attachment(new MemoryStream(key.Value), key.Key));
                    }
                }

                sender.Send(msg);
                log.Info("//----> Envio de Correo: EXITOSO!");
                MostrarExito("Envio de Correo", "Correo Enviado Exitosamente");
            }
            catch (Exception ex)
            {
                log.Info(String.Format("//----> Error el en Proceso de Envio de Correo. Mensaje: [{0}]", ex.Message));
                ManejarExcepcion(ex);
            }

        }

        /// <summary>Método que nos regresa si un DropDown es valido</summary>
        /// <param name="obj">El dropDown a validar</param>
        /// <remarks>Toma como parametros un DropDown, retorna TRUE si el DropDown no es Nulo, 
        /// si la longitud ToString() es mayor a cero, si tiene Items y la longitud del arreglo de items es mayor a cero
        /// </remarks>
        public Boolean ValidarDropDown(DropDownList obj)
        {
            return (obj != null && obj.ToString().Length > 0 && obj.Items != null && obj.Items.Count > 1);
        }


        /// <summary>Método que nos regresa el texto en Mayusculas</summary>
        /// <param name="obj">Texto</param>
        /// <remarks> Pone un texto vacio si el texto es nulo
        /// </remarks>
        private String EnMayusculas(String obj)
        {
            return ValidarObjeto(obj) ? obj.ToUpper() : Constantes.TEXTO_BLANCO;
        }


        /// <summary>Método que nos regresa si un objeto es valido</summary>
        /// <param name="obj">El arreglo a validar</param>
        /// <remarks>Toma como parametros un objetos, retorna TRUE si el objeto no es nulo y su longitud ToString() es mayor a cero</remarks>
        public Boolean ValidarObjeto(Object obj)
        {
            return (obj != null && !string.IsNullOrEmpty(obj.ToString()));
        }

        /// <summary>Método que nos regresa si un arreglo es valido o no y si tiene datos</summary>
        /// <param name="array">El arreglo a validar</param>
        /// <remarks>Toma como parametros un arreglo de objetos, retorna TRUE si el arreglo no es nulo y su longitud es mayor a cero</remarks>
        public Boolean ValidarArray(object[] array)
        {
            return (array != null && array.Length > 0);
        }

        /// <summary>Método que nos regresa un nuevo elemento ListItem</summary>
        /// <param name="texto">El texto a desplegar en el "Value" para este ListItem</param>
        /// <param name="valor">El valor del "Title" de este ListItem. Esto aminora el bug del IE8</param>
        /// <remarks>Nos regresa un URL de ejemplo para una cotización de persona fisica. </remarks>
        public ListItem NuevoListItem(String texto, String valor)
        {
            ListItem retorno = new ListItem(texto, valor);
            retorno.Attributes.Add("title", texto);
            return retorno;
        }

        /// <summary>Método que nos regresa un nuevo elemento ListItem</summary>
        /// <param name="texto">El texto a desplegar en el "Value" para este ListItem</param>
        /// <param name="valor">El valor del "Title" de este ListItem. Esto aminora el bug del IE8</param>
        /// <remarks>Nos regresa un URL de ejemplo para una cotización de persona fisica. </remarks>
        public ListItem NuevoListItem(String texto, String valor, String valorAlternativo)
        {
            ListItem retorno = new ListItem(texto, valor);
            retorno.Attributes.Add("data-value", valorAlternativo);
            return retorno;
        }


        public void MostrarVentana(String ventana)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "$(\"#" + ventana + "\").modal();", true);

        }
        /// <summary>Metodo que muestra una excepcion</summary>
        /// <param name="titulo">El titulo de la excepcion.</param>
        /// <param name="mensaje">El mensaje de la excepcion.</param>
        /// <remarks>Este metodo muestra una excecpcion visual en la pagina web</remarks>      
        public void MostrarExcepcion(String titulo, String mensaje)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "mensajeError(\"" + mensajeSinSpecialChars(mensaje) + "\",\"" + titulo + "\");", true);

        }

        /// <summary>Metodo que muestra una advertencia</summary>
        /// <param name="titulo">El titulo de la advertencia.</param>
        /// <param name="mensaje">El mensaje de la advertencia.</param>
        /// <remarks>Este metodo muestra una advertencia visual en la pagina web</remarks>      
        public void MostrarAdvertencia(String titulo, String mensaje)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "mensajeAdvertir(\"" + mensajeSinSpecialChars(mensaje) + "\",\"" + titulo + "\");", true);
        }

        /// <summary>Metodo que muestra un mensaje de exito</summary>
        /// <param name="titulo">El titulo del mensaje.</param>
        /// <param name="mensaje">El mensaje de exito.</param>
        /// <remarks>Este metodo muestra un mensaje de exito visual en la pagina web</remarks>      
        public void MostrarExito(String titulo, String mensaje)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "mensajeExito(\"" + mensajeSinSpecialChars(mensaje) + "\",\"" + titulo + "\");", true);

        }

        /// <summary>Metodo que muestra una notificaicon</summary>
        /// <param name="titulo">El titulo de la notificacion.</param>
        /// <param name="mensaje">El mensaje de la notificacion.</param>
        /// <remarks>Este metodo muestra una notificacion visual en la pagina web</remarks>      
        public void MostrarNotificacion(String titulo, String mensaje)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "mensajeNotificar(\"" + mensajeSinSpecialChars(mensaje) + "\",\"" + titulo + "\");", true);

        }

        public String mensajeSinSpecialChars(String msg)
        {
            if (msg != null)
            {
                msg = msg.Replace("\n", " ").Trim();
                msg = msg.Replace("\r", " ").Trim();
                char[] arr = msg.ToCharArray();

                arr = Array.FindAll<char>(arr, (c => (char.IsLetterOrDigit(c) || char.IsWhiteSpace(c) || c == '-' || c == '/')));
                return new string(arr);
            }
            return null;

        }

        public static bool isValidEmail(string inputEmail)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
        }
    }
}