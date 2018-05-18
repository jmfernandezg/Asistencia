using System;
using Asistencia.Clases;
using Excel;
using System.Data;


namespace Asistencia.Proceso
{
    public partial class CargaMasivaOficina : PaginaExtensible
    {
        IPlazaDao plazaDao;
        IOficinaDao oficinaDao;
        IZonaDao zonaDao;
        IRegionDao regionDao;

        protected void Page_Load(object sender, EventArgs e)
        {
            oficinaDao = daoFactory.GetOficinaDao();
            plazaDao = daoFactory.GetPlazaDao();
            zonaDao = daoFactory.GetZonaDao();
            regionDao = daoFactory.GetRegionDao();

            if (!IsPostBack)
            {

                CajaRegistro.Items.Clear();
                log.Info(String.Format("El usuario: [{0}] ha cargado la pagina de Carga Masiva de Oficina", UsuarioActual.Nombre));
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
                    String codigo = excelReader.GetString(0);
                    String nombre = excelReader.GetString(1);
                    String zona = excelReader.GetString(2);
                    String region = excelReader.GetString(3);
                    String plaza = excelReader.GetString(4);

                    Boolean encontroError = false;

                    if (i == 1)
                    {
                        encontroError = true;
                    }

                    if (!ValidarObjeto(codigo) && !encontroError)
                    {
                        CajaRegistro.Items.Add(NuevoListItem(String.Format("ERROR FILA: [{0}], la columna carece de codigo", i), Constantes.TEXTO_BLANCO));
                        encontroError = true;
                    }
                    if (!ValidarObjeto(nombre) && !encontroError)
                    {
                        CajaRegistro.Items.Add(NuevoListItem(String.Format("ERROR FILA: [{0}], la columna carece de nombre", i), Constantes.TEXTO_BLANCO));
                        encontroError = true;
                    }
                    if (!ValidarObjeto(zona) && !encontroError)
                    {
                        CajaRegistro.Items.Add(NuevoListItem(String.Format("ERROR FILA: [{0}], la columna carece de zona", i), Constantes.TEXTO_BLANCO));
                        encontroError = true;
                    }
                    if (!ValidarObjeto(region) && !encontroError)
                    {
                        CajaRegistro.Items.Add(NuevoListItem(String.Format("ERROR FILA: [{0}], la columna carece de region", i), Constantes.TEXTO_BLANCO));
                        encontroError = true;
                    }
                    if (!ValidarObjeto(plaza) && !encontroError)
                    {
                        CajaRegistro.Items.Add(NuevoListItem(String.Format("ERROR FILA: [{0}], la columna carece de plaza", i), Constantes.TEXTO_BLANCO));
                        encontroError = true;
                    }

                    if (!encontroError)
                    {
                        try
                        {
                            Oficina obj = oficinaDao.GetByCodigoPlanta(codigo);
                            Plaza objPlaza = plazaDao.GetByNombre(plaza);
                            Zona objZona = zonaDao.GetByNombre(zona);
                            Region objRegion = regionDao.GetByNombre(region);

                            if (obj != null)
                            {
                                CajaRegistro.Items.Add(NuevoListItem(String.Format("ADV FILA: [{0}], este codigo de oficina ya existe, se actualizara el registro", i), Constantes.TEXTO_BLANCO));
                            }
                            if (objPlaza == null)
                            {
                                CajaRegistro.Items.Add(NuevoListItem(String.Format("ADV FILA: [{0}], la plaza: [{1}]  NO existe en la base de datos.", i, plaza), Constantes.TEXTO_BLANCO));
                            }
                            if (objRegion == null)
                            {
                                CajaRegistro.Items.Add(NuevoListItem(String.Format("ADV FILA: [{0}], la region: [{1}], NO existe en la base de datos.", i, region), Constantes.TEXTO_BLANCO));
                            }
                            if (objZona == null)
                            {
                                CajaRegistro.Items.Add(NuevoListItem(String.Format("ADV FILA: [{0}], la zona: [{1}], NO existe en la base de datos.", i, zona), Constantes.TEXTO_BLANCO));
                            }

                            if (obj == null)
                            {
                                obj = new Oficina();
                                obj.Usuario_creado_por = UsuarioActual;
                                obj.DetalleCatalogo = daoFactory.GetDetalleCatalogoDao().GetById(Constantes.DETALLE_CATALOGO_OFICINA);
                            }


                            obj.Usuario_modificado_por = UsuarioActual;
                            obj.FechaModificacion = DateTime.Now;
                            obj.Nombre = nombre;
                            obj.CodigoPlanta = codigo;

                            if (objPlaza != null)
                            {
                                obj.Plaza = objPlaza;
                            }
                            if (objRegion != null)
                            {
                                obj.Region = objRegion;
                            }

                            if (objZona != null)
                            {
                                obj.Zona = objZona;
                            }

                            oficinaDao.SaveOrUpdate(obj);
                            CajaRegistro.Items.Add(NuevoListItem(String.Format("OK FILA: [{0}], registro guardado correctamente", i), Constantes.TEXTO_BLANCO));
                        }
                        catch (Exception ex)
                        {
                            log.Info(String.Format("Error en el proceso de importar la oficina. Mensaje [{0}]", ex.Message));
                            CajaRegistro.Items.Add(NuevoListItem(String.Format("ERROR FILA: [{0}], error en el proceso. Detalles: [{1}] ", i, ex.Message), Constantes.TEXTO_BLANCO));
                        }

                    }
                }

                CajaRegistro.Items.Add(NuevoListItem(String.Format("PROCESO OK: Se completaron: [{0}] registros", i), Constantes.TEXTO_BLANCO));
                excelReader.Close();

            }
            catch (Exception ex)
            {
                log.Info(String.Format("Error en el proceso de importar la Hoja de Excel. Mensaje [{0}]", ex.Message));
                CajaRegistro.Items.Add(NuevoListItem(String.Format("ERROR: Error en el proceso. Detalles: [{0}]", ex.Message), Constantes.TEXTO_BLANCO));
                ManejarExcepcion(ex);
            }
        }
    }
}