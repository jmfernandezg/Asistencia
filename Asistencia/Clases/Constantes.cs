using System;

namespace Asistencia.Clases
{
    public class Constantes
    {

        public static String WEB_VARIABLE_SESSION_USUARIO = "Usuario";
        public static String WEB_VARIABLE_SESSION_OBJETO = "Objeto";
        public static String WEB_VARIABLE_SESSION_ID = "Id";
        public static String WEB_PAGINA_INICIO_SESION = "~/Default.aspx";
        public static String WEB_PAGINA_SISTEMA = "~/Sistema.aspx";
        public static String WEB_PAGINA_CATALOGO_MI_PERFIL = "~/Catalogo/MiPerfil.aspx";
        public static String WEB_PAGINA_CATALOGO_PERFIL = "~/Catalogo/Perfil.aspx";
        public static String WEB_PAGINA_CATALOGO_PERFIL_ABM = "~/Catalogo/PerfilAbm.aspx";
        public static String WEB_PAGINA_CATALOGO_USUARIO = "~/Catalogo/Usuario.aspx";
        public static String WEB_PAGINA_CATALOGO_USUARIO_ABM = "~/Catalogo/UsuarioAbm.aspx";
        public static String WEB_PAGINA_CATALOGO_EMPLEADO = "~/Catalogo/Empleado.aspx";
        public static String WEB_PAGINA_CATALOGO_EMPLEADO_ABM = "~/Catalogo/EmpleadoAbm.aspx";
        public static String WEB_PAGINA_CATALOGO_OFICINA = "~/Catalogo/Oficina.aspx";
        public static String WEB_PAGINA_CATALOGO_OFICINA_ABM = "~/Catalogo/OficinaAbm.aspx";
        public static String WEB_PAGINA_CATALOGO_CONTROL_ACCESO = "~/Catalogo/ControlAcceso.aspx";
        public static String WEB_PAGINA_CATALOGO_CONTROL_ACCESO_ABM = "~/Catalogo/ControlAccesoAbm.aspx";

        public static String LOG_ASISTENCIA_APP = "AsistenciaApp";
        public static String LOG_ASISTENCIA_DB = "AsistenciaDb";
        public static String LOG_ASISTENCIA_TAREA_COLECTOR = "AsistenciaColector";
        public static String LOG_ASISTENCIA_TAREA_SERVICIO_WEB = "AsistenciaServicioWeb";

        public static String CONFIG_WEB_TABLERO_PROCESOS = "urlDescriptorProcesoTareas";
        public static String CONFIG_DATABASE_CONNECTION_NAME = "base_de_datos";
        public static String CONFIG_PROCESO_COLECTOR_INTERVALO = "procesoColectorIntervaloMinutos";
        public static String CONFIG_PROCESO_SERVICIO_WEB_INTERVALO = "procesoServicioWebIntervaloMinutos";
        public static String CONFIG_PROCESO_SERVICIO_WEB_USUARIO = "procesoServicioWebUsuario";
        public static String CONFIG_PROCESO_SERVICIO_WEB_PASSWD = "procesoServicioWebContrasena";
        public static String CONFIG_PROCESO_COLECTOR_INICIAR = "procesoColectorIniciar";
        public static String CONFIG_PROCESO_SERVICIO_WEB_INICIAR = "procesoServicioWebIniciar";

        public static String CONFIG_MAIL_FROM = "mailFrom";
        public static String CONFIG_MAIL_USERNAME = "mailUsername";
        public static String CONFIG_MAIL_PASSWD = "mailPasswd";
        public static String CONFIG_MAIL_HOST = "mailHost";
        public static String CONFIG_MAIL_PORT = "mailPort";
        public static String CONFIG_MAIL_SSL = "mailSSL";
        public static String CONFIG_MAIL_BCC = "mailBCC";

        public static String MENSAJE_CAMPO_REQUERIDO_TITULO = "Campo Requerido";
        public static String MENSAJE_CAMPO_REQUERIDO_TEXTO = "Es Requerido Capturar el Campo {0}";

        public static String MENSAJE_CAMPO_CON_ERROR_TITULO = "Campo Inválido";
        public static String MENSAJE_CAMPO_CON_ERROR_TEXTO = "El Campo {0} no contiene valores correctos para el mismo. Por favor revise su captura";

        public static String VENTANA_MODAL_ABM = "abm";
        public static String TEXTO_BLANCO = "";
        public static String TEXTO_SELECCION = "-- SELECCIONE --";
        public static String WEB_PANEL_JAVASCRIPT_TABLA = "PanelDataTable";

        public static int DETALLE_CATALOGO_OFICINA = 4915;
        public static String MIME_TIME_EXCEL_97 = "application/vnd.ms-excel";
        public static String MIME_TIME_EXCEL_2007 = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        public static String EXTENSION_EXCEL_97 = ".xls";
        public static String EXTENSION_EXCEL_2007 = ".xlsx";
    }
}
