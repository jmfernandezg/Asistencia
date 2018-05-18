using NPOI.HSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;

namespace Asistencia.Reporte
{
    public partial class ReporteEmpleadoNoRegistrado : Asistencia.Clases.PaginaExtensible
    {
        IColectorMovimientoIncidenciaDao incidenciaDao;
        protected void Page_Load(object sender, EventArgs e)
        {
            incidenciaDao = daoFactory.GetColectorMovimientosIncidenciaDao();

            if (!IsPostBack)
            {
                log.Info(String.Format("El usuario: [{0}] ha cargado la pagina de Reporte de Empleado No Registrado", UsuarioActual.Nombre));

                // Cargamos la fecha 
                txtFecha.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtFechaFin.Text = DateTime.Now.ToString("yyyy-MM-dd");

            }
        }


        protected void GenerarExcel(List<Asistencia.DbDominio.ColectorMovimientoIncidencia> lista)
        {

            var workbook = new HSSFWorkbook();
            var sheet = workbook.CreateSheet("Empleado No Registrado");

            var rowIndex = 0;
            var row = sheet.CreateRow(rowIndex);
            row.CreateCell(0).SetCellValue("Id");
            row.CreateCell(1).SetCellValue("Fecha");
            row.CreateCell(2).SetCellValue("Empleado");
            row.CreateCell(3).SetCellValue("Control de Acceso");
            row.CreateCell(4).SetCellValue("Detalle");
            rowIndex++;

            foreach (DbDominio.ColectorMovimientoIncidencia inci in lista)
            {
                row = sheet.CreateRow(rowIndex);
                row.CreateCell(0).SetCellValue(inci.Id);
                row.CreateCell(1).SetCellValue(inci.Fecha != null ? inci.Fecha.ToString("yyyy-MM-dd HH:mm:ss") : "");
                row.CreateCell(2).SetCellValue(inci.CveEmpleado);
                row.CreateCell(3).SetCellValue(inci.ControlAcceso != null ? inci.ControlAcceso.Nombre : "");
                row.CreateCell(4).SetCellValue(inci.Detalles);
                rowIndex++;
            }

            using (var exportData = new MemoryStream())
            {
                workbook.Write(exportData);
                string saveAsFileName = string.Format("ReporteEmpleadoNoRegistrado-{0:d}.xls", DateTime.Now.ToString("yyyy-MM-dd"));
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", saveAsFileName));
                Response.Clear();
                Response.BinaryWrite(exportData.GetBuffer());
                Response.End();
            }


        }

        protected void BtnActualizar_Click(object sender, EventArgs e)
        {
            if (!ValidarObjeto(txtFecha.Text) || !ValidarObjeto(txtFechaFin.Text))
            {
                MostrarExcepcion("Campo Requerido", "Es requerido seleccionar las dos fechas del reporte");
            }
            log.Info(String.Format("El usuario: [{0}] ha generado el reporte de Usuario No Registrado con Fecha Inicial: [{1}] y Fecha Final: [{2}]", UsuarioActual.Nombre, txtFecha.Text, txtFechaFin.Text));



            List<ColectorMovimientoIncidencia> l = incidenciaDao.GetListado(DateTime.Parse(txtFecha.Text), DateTime.Parse(txtFechaFin.Text).AddDays(1), true);

            if (l == null || l.Count == 0)
            {
                MostrarNotificacion("Lista Vacia", "El reporte no contiene datos en las fechas solicitadas");
            }
            GenerarExcel(l);

        }
    }


}