using System;
using Asistencia.Clases;
using Excel;
using System.Data;

namespace Asistencia.Proceso
{
    public partial class CargaAsistencia : PaginaExtensible
    {
        IIncidenciaDao incidenciaDao;
        IEmpleadoDao empleadoDao;
        IControlAccesoDao controlAccesoDao;

        protected void Page_Load(object sender, EventArgs e)
        {
            incidenciaDao = daoFactory.GetIncidenciaDao();
            controlAccesoDao = daoFactory.GetControlAccesoDao();
            empleadoDao = daoFactory.GetEmpleadoDao();


            if (!IsPostBack)
            {

                CajaRegistro.Items.Clear();

                log.Info(String.Format("El usuario: [{0}] ha cargado la pagina de Carga Masiva de Incidencia", UsuarioActual.Nombre));
            }

        }

        protected void btnSubir_Click(object sender, EventArgs e)
        {
            if (!CargaArchivo.HasFile)
            {
                MostrarExcepcion(Constantes.MENSAJE_CAMPO_REQUERIDO_TITULO, "Es requerido subir un archivo");
                return;
            }

            String ext = System.IO.Path.GetExtension(CargaArchivo.PostedFile.FileName);
            log.Info(String.Format("El usuario: [{0}] ha cargado el archivo de excel [{1}] para su insercion masiva", UsuarioActual.Nombre, CargaArchivo.PostedFile.FileName));

            if (!ext.ToLower().Equals(Constantes.EXTENSION_EXCEL_97) && !ext.ToLower().Equals(Constantes.EXTENSION_EXCEL_2007))
            {
                MostrarExcepcion(Constantes.MENSAJE_CAMPO_REQUERIDO_TITULO, "Solo son permitidos los archivos de Excel");
                return;

            }

            CajaRegistro.Items.Clear();
            CajaRegistro.Items.Add(NuevoListItem("Iniciando Proceso", Constantes.TEXTO_BLANCO));

            try
            {
                IExcelDataReader excelReader = null;


                if (ext.ToLower().Equals(Constantes.EXTENSION_EXCEL_2007))
                {
                    CajaRegistro.Items.Add(NuevoListItem("Leyendo Archivo de Excel XLSX 2007", Constantes.TEXTO_BLANCO));
                    excelReader = ExcelReaderFactory.CreateOpenXmlReader(CargaArchivo.PostedFile.InputStream);
                }
                else
                {
                    if (ext.ToLower().Equals(Constantes.EXTENSION_EXCEL_97))
                    {
                        CajaRegistro.Items.Add(NuevoListItem("Leyendo Archivo de Excel XLS 97-2003", Constantes.TEXTO_BLANCO));
                        excelReader = ExcelReaderFactory.CreateBinaryReader(CargaArchivo.PostedFile.InputStream);
                    }
                }


                excelReader.IsFirstRowAsColumnNames = true;
                DataSet result = excelReader.AsDataSet();

                int i = 0;
                while (excelReader.Read())
                {
                    i++;
                    String nomina = excelReader.GetString(0);
                    String time = excelReader.GetString(1);
                    String deviceid = excelReader.GetString(2);
                    String status = excelReader.GetString(3);

                    int numeroNumina = 0;
                    Boolean encontroError = false;

                    DateTime fechaInicidencia;

                    if (i == 1)
                    {
                        encontroError = true;
                    }

                    if (!Int32.TryParse(nomina, out numeroNumina) && !encontroError)
                    {
                        numeroNumina = 0;
                        CajaRegistro.Items.Add(NuevoListItem(String.Format("ERROR FILA: [{0}], la columna de Nomina no es numerica", i), Constantes.TEXTO_BLANCO));
                        encontroError = true;
                    }

                    if (numeroNumina <= 0 && !encontroError)
                    {
                        CajaRegistro.Items.Add(NuevoListItem(String.Format("ERROR FILA: [{0}], la columna carece de numero nomina", i), Constantes.TEXTO_BLANCO));
                        encontroError = true;
                    }


                    Empleado emp = encontroError ? null : empleadoDao.GetByNumeroEmpleado(Int32.Parse(nomina));
                    if (emp == null && !encontroError)
                    {
                        CajaRegistro.Items.Add(NuevoListItem(String.Format("ERROR FILA: [{0}], este empleado no existe en la base de datos", i), Constantes.TEXTO_BLANCO));
                        encontroError = true;
                    }

                    ControlAcceso ctrl = encontroError ? null : controlAccesoDao.GetByIdControl(Int32.Parse(deviceid));
                    if (ctrl == null && !encontroError)
                    {
                        CajaRegistro.Items.Add(NuevoListItem(String.Format("ERROR FILA: [{0}], este control de acceso no existe en la base de datos", i), Constantes.TEXTO_BLANCO));
                        encontroError = true;
                    }

                    if (!DateTime.TryParse(time, out fechaInicidencia) && !encontroError)
                    {
                        CajaRegistro.Items.Add(NuevoListItem(String.Format("ERROR FILA: [{0}], esta fecha no puede ser interpretada correctamente", i), Constantes.TEXTO_BLANCO));
                        encontroError = true;

                    }


                    if (!encontroError)
                    {
                        try
                        {
                            Incidencia obj = incidenciaDao.GetByEmpleadoFecha(emp, fechaInicidencia);

                            if (obj != null)
                            {
                                CajaRegistro.Items.Add(NuevoListItem(String.Format("ADV FILA: [{0}], esta incidencia ya existe, se actualizara el registro", i), Constantes.TEXTO_BLANCO));
                            }
                            if (obj == null)
                            {
                                obj = new Incidencia();
                            }

                            obj.ControlAcceso = ctrl;
                            obj.Empleado = emp;
                            obj.EnviadoWs = 0;
                            obj.FechaAlta = DateTime.Now;
                            obj.FechaHoraIncidencia = fechaInicidencia;
                            obj.InOutMode = Int32.Parse(status);


                            incidenciaDao.SaveOrUpdate(obj);
                            CajaRegistro.Items.Add(NuevoListItem(String.Format("OK FILA: [{0}], registro guardado correctamente", i), Constantes.TEXTO_BLANCO));
                        }
                        catch (Exception ex)
                        {
                            log.Error(String.Format("Error al momento de intentar insertar una incidencia importada de Excel. Mensaje: [{0}]", ex.Message));
                            CajaRegistro.Items.Add(NuevoListItem(String.Format("ERROR FILA: [{0}],  error en el proceso. Mensaje: [{1}]", i, ex.Message), Constantes.TEXTO_BLANCO));
                        }

                    }
                }

                CajaRegistro.Items.Add(NuevoListItem(String.Format("PROCESO OK: Se completaron: [{0}] registros", i), Constantes.TEXTO_BLANCO));
                excelReader.Close();

            }
            catch (Exception ex)
            {
                CajaRegistro.Items.Add(NuevoListItem(String.Format("Error en el proceso. Detalles: [{0}] ", ex.Message), Constantes.TEXTO_BLANCO));

                log.Error(String.Format("Error al momento de intentar importar la hoja de excel de Asistencia. Mensaje [{0}]", ex.Message));
                ManejarExcepcion(ex);
            }
        }
    }
}