using System;
using Asistencia.Clases;
using Excel;
using System.Data;

namespace Asistencia.Proceso
{
    public partial class CargaMasivaEmpleado : PaginaExtensible
    {
        IEmpleadoDao empleadoDao;
        IPlazaDao plazaDao;

        protected void Page_Load(object sender, EventArgs e)
        {
            empleadoDao = daoFactory.GetEmpleadoDao();
            plazaDao = daoFactory.GetPlazaDao();

            if (!IsPostBack)
            {
                CajaRegistro.Items.Clear();

                log.Info(String.Format("El usuario: [{0}] ha cargado la pagina de Carga Masiva de Empleado", UsuarioActual.Nombre));
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
                    String nombre = excelReader.GetString(1);
                    String plaza = excelReader.GetString(2);

                    int numeroNumina = 0;
                    Boolean encontroError = false;

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

                    if (!ValidarObjeto(nombre) && !encontroError)
                    {
                        CajaRegistro.Items.Add(NuevoListItem(String.Format("ERROR FILA: [{0}], la columna carece de nombre ", i), Constantes.TEXTO_BLANCO));
                        encontroError = true;
                    }
                    if (!ValidarObjeto(plaza) && !encontroError)
                    {
                        CajaRegistro.Items.Add(NuevoListItem(String.Format("ERROR FILA: [{0}], la columna carece de plaza", i), Constantes.TEXTO_BLANCO));
                        encontroError = true;
                    }
                    if (numeroNumina <= 0 && !encontroError)
                    {
                        CajaRegistro.Items.Add(NuevoListItem(String.Format("ERROR FILA: [{0}], la columna carece de numero nomina", i), Constantes.TEXTO_BLANCO));
                        encontroError = true;
                    }

                    if (!encontroError)
                    {
                        try
                        {
                            Empleado obj = empleadoDao.GetByNumeroEmpleado(numeroNumina);
                            Plaza objPlaza = plazaDao.GetByNombre(plaza);

                            if (obj != null)
                            {
                                CajaRegistro.Items.Add(NuevoListItem(String.Format("ADV FILA: [{0}], este numero de nomina ya existe, se actualizara el registro", i), Constantes.TEXTO_BLANCO));
                            }
                            if (objPlaza == null)
                            {
                                CajaRegistro.Items.Add(NuevoListItem(String.Format("ADV FILA: [{0}], la plaza [{1}] NO existe en la base de datos.", i, plaza), Constantes.TEXTO_BLANCO));
                            }
                            if (obj == null)
                            {
                                obj = new Empleado();
                                obj.Usuario_creado_por = UsuarioActual;
                                obj.Usuario_cve_usuario_alta = UsuarioActual;
                            }
                            obj.Usuario_modificado_por = UsuarioActual;
                            obj.FechaModificacion = DateTime.Now;
                            obj.Nombre = nombre;
                            obj.NoEmpleado = numeroNumina;

                            if (objPlaza != null)
                            {
                                obj.Plaza = objPlaza;
                            }
                            empleadoDao.SaveOrUpdate(obj);
                            CajaRegistro.Items.Add(NuevoListItem(String.Format("OK FILA:  [{0}], registro guardado correctamente", i), Constantes.TEXTO_BLANCO));
                        }
                        catch (Exception ex)
                        {
                            log.Error(String.Format("Error al momento de intentar insertar un empleado importado de Excel. Mensaje: [{0}]", ex.Message));
                            CajaRegistro.Items.Add(NuevoListItem(String.Format("ERROR FILA: [{0}]. Error en el proceso. Detalles: [{1}]", i, ex.Message), Constantes.TEXTO_BLANCO));
                        }

                    }
                }

                CajaRegistro.Items.Add(NuevoListItem(String.Format("PROCESO OK: Se completaron: [{0}] registros", i), Constantes.TEXTO_BLANCO));
                excelReader.Close();

            }
            catch (Exception ex)
            {
                CajaRegistro.Items.Add(NuevoListItem(String.Format("ERROR: Error en el proceso. Detalles:  [{0}] ", ex.Message), Constantes.TEXTO_BLANCO));

                log.Error(String.Format("Error al momento de intentar importar la hoja de excel de Empleados. Mensaje  [{0}]", ex.Message));
                ManejarExcepcion(ex);
            }
        }

    }
}