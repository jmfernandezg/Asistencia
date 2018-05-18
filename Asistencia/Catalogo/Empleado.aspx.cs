using Asistencia.Clases;
using System;
using System.Web.UI.WebControls;
using NPOI.HSSF.UserModel;
using System.IO;
using System.Collections.Generic;

namespace Asistencia.Catalogo
{
    public partial class Empleado : PaginaExtensible
    {
        Asistencia.DbInterfase.IEmpleadoDao empleadoDao;
        Asistencia.DbInterfase.IEmpleadoControlAccesoDao empleadoControlAccesoDao;

        protected void Page_Load(object sender, EventArgs e)
        {
            empleadoDao = daoFactory.GetEmpleadoDao();
            empleadoControlAccesoDao = daoFactory.GetEmpleadoControlAccesoDao();

            log.Info(String.Format("El usuario: [{0}] ha visitado la pagina de Listado de Empleados", UsuarioActual.Nombre));

            if (!IsPostBack)
            {
                MostrarJavascriptTabla();
                CargarListado();
            }
        }

        public void CargarListado()
        {
            try
            {
                Repeticion.DataSource = empleadoDao.GetListado();
                Repeticion.DataBind();

            }
            catch (Exception ex)
            {
                log.Error(String.Format("Error al intentar Obtener El Listado de Empleados. Mensaje: [{0}] ", ex.Message));
                ManejarExcepcion(ex);
            }
        }


        protected void GenerarExcel(List<Asistencia.DbDominio.Empleado> lista)
        {

            var workbook = new HSSFWorkbook();
            var sheet = workbook.CreateSheet("Empleados");

            var rowIndex = 0;
            var row = sheet.CreateRow(rowIndex);
            row.CreateCell(0).SetCellValue("Id Empleado");
            row.CreateCell(1).SetCellValue("No Empleado");
            row.CreateCell(2).SetCellValue("Nombre");
            row.CreateCell(3).SetCellValue("Tarjeta");
            row.CreateCell(4).SetCellValue("Plaza");
            row.CreateCell(5).SetCellValue("Fecha Alta");
            row.CreateCell(6).SetCellValue("Activo");
            row.CreateCell(7).SetCellValue("Ultima Huella");
            row.CreateCell(8).SetCellValue("Controles");
            rowIndex++;

            foreach (Asistencia.DbDominio.Empleado inci in lista)
            {
                row = sheet.CreateRow(rowIndex);
                row.CreateCell(0).SetCellValue(inci.CveEmpleado);
                row.CreateCell(1).SetCellValue(inci.NoEmpleado);
                row.CreateCell(2).SetCellValue(inci.Nombre);
                row.CreateCell(3).SetCellValue(inci.Tarjeta);
                row.CreateCell(4).SetCellValue(inci.NombrePlaza);
                row.CreateCell(5).SetCellValue(inci.FechaAlta != null ? inci.FechaAlta.ToString("yyyy-MM-dd HH:mm:ss") : "");
                row.CreateCell(6).SetCellValue(inci.Activo.HasValue ? inci.Activo.Value == true ? "Si" : "No" : "");
                row.CreateCell(7).SetCellValue(inci.UltimaColeccion);

                String controles = "";

                /*
                ISet<EmpleadoControlAcceso> listaControles = inci.EmpleadoControlAccesos;
                if (listaControles != null && listaControles.Count > 0)
                {
                    foreach (EmpleadoControlAcceso ectrlA in listaControles)
                    {
                        if (ectrlA.ControlAcceso != null)
                        {
                            controles = "\n" + ectrlA.ControlAcceso.Nombre;
                        }
                    }
                }*/

                row.CreateCell(8).SetCellValue(controles);

                rowIndex++;
            }

            using (var exportData = new MemoryStream())
            {
                workbook.Write(exportData);
                string saveAsFileName = string.Format("ReporteEmpleado-{0:d}.xls", DateTime.Now.ToString("yyyy-MM-dd"));
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", saveAsFileName));
                Response.Clear();
                Response.BinaryWrite(exportData.GetBuffer());
                Response.End();
            }


        }


        protected void BtnEditar_Click(object sender, EventArgs e)
        {
            try
            {

                LinkButton b = (LinkButton)sender;

                // validamos el argumento de comando
                if (b != null && b.CommandArgument != null && b.CommandArgument.Length > 0)
                {
                    log.Info(String.Format("El usuario: [{0}] ha presionado el boton para editar el Empleado con Id: [{1}]", UsuarioActual.Nombre, b.CommandArgument));

                    Session[Constantes.WEB_VARIABLE_SESSION_ID] = b.CommandArgument;
                    Redirigir(Constantes.WEB_PAGINA_CATALOGO_EMPLEADO_ABM);
                }

            }
            catch (Exception ex)
            {
                log.Error(String.Format("Error al intentar Obtener los datos Del Empleado a Editar. Mensaje: [{0}] ", ex.Message));
                ManejarExcepcion(ex);
            }
        }
        protected void BtnNuevo_Click(object sender, EventArgs e)
        {
            log.Info(String.Format("El usuario: [{0}] ha presionado el boton de Nuevo Empleado", UsuarioActual.Nombre));

            Session[Constantes.WEB_VARIABLE_SESSION_ID] = null;
            Redirigir(Constantes.WEB_PAGINA_CATALOGO_EMPLEADO_ABM);
        }

        protected void BtnBloquear_Click(object sender, EventArgs e)
        {
            try
            {

                LinkButton b = (LinkButton)sender;

                // validamos el argumento de comando
                if (b != null && b.CommandArgument != null && b.CommandArgument.Length > 0)
                {
                    log.Info(String.Format("El usuario: [{0}] ha presionado el boton para Bloquear el Empleado con Id: [{1}]", UsuarioActual.Nombre, b.CommandArgument));

                    Asistencia.DbDominio.Empleado obj = empleadoDao.GetById(Int32.Parse(b.CommandArgument));
                    if (obj != null)
                    {
                        obj.Activo = false;
                        empleadoDao.SaveOrUpdate(obj);
                        CargarListado();

                    }
                }

            }
            catch (Exception ex)
            {
                log.Error(String.Format("Error al intentar Bloquear el Empleado. Mensaje: [{0}] ", ex.Message));
                ManejarExcepcion(ex);
            }
        }

        protected void BtnDesbloquear_Click(object sender, EventArgs e)
        {
            try
            {

                LinkButton b = (LinkButton)sender;

                // validamos el argumento de comando
                if (b != null && b.CommandArgument != null && b.CommandArgument.Length > 0)
                {
                    log.Info(String.Format("El usuario: [{0}] ha presionado el boton para Desbloquear el Empleado con Id: [{1}]", UsuarioActual.Nombre, b.CommandArgument));

                    Asistencia.DbDominio.Empleado obj = empleadoDao.GetById(Int32.Parse(b.CommandArgument));
                    if (obj != null)
                    {
                        obj.Activo = true;
                        empleadoDao.SaveOrUpdate(obj);
                        CargarListado();

                    }
                }

            }
            catch (Exception ex)
            {
                log.Error(String.Format("Error al intentar Desbloquear el Empleado. Mensaje: [{0}] ", ex.Message));
                ManejarExcepcion(ex);
            }
        }



        protected void BtnEliminar_Click(object sender, EventArgs e)
        {
            try
            {

                LinkButton b = (LinkButton)sender;

                // validamos el argumento de comando
                if (b != null && b.CommandArgument != null && b.CommandArgument.Length > 0)
                {
                    log.Info(String.Format("El usuario: [{0}] ha presionado el boton para Eliminar el Empleado con Id: [{1}]", UsuarioActual.Nombre, b.CommandArgument));

                    Asistencia.DbDominio.Empleado obj = empleadoDao.GetById(Int32.Parse(b.CommandArgument));
                    if (obj != null)
                    {
                        obj.Habilitado = false;
                        empleadoDao.SaveOrUpdate(obj);
                        CargarListado();

                    }
                }

            }
            catch (Exception ex)
            {
                log.Error(String.Format("Error al intentar Eliminar el Empleado. Mensaje: [{0}] ", ex.Message));
                ManejarExcepcion(ex);
            }
        }

        protected void BtnExcel_Click(object sender, EventArgs e)
        {

            GenerarExcel(empleadoDao.GetListado());
        }
    }
}