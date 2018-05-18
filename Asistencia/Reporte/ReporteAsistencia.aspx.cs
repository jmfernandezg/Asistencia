using System;
using Asistencia.Clases;
using System.Collections.Generic;
using NPOI.HSSF.UserModel;
using System.IO;

namespace Asistencia.Reporte
{
    public partial class ReporteAsistencia : PaginaExtensible
    {
        IIncidenciaDao incidenciaDao;
        IPlazaDao plazaDao;

        protected void Page_Load(object sender, EventArgs e)
        {
            incidenciaDao = daoFactory.GetIncidenciaDao();
            plazaDao = daoFactory.GetPlazaDao();

            if (!IsPostBack)
            {
                log.Info(String.Format("El usuario: [{0}] ha cargado la pagina de Reporte de Asistencia", UsuarioActual.Nombre));

                // Cargamos la fecha 
                txtFecha.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtFechaFin.Text = DateTime.Now.ToString("yyyy-MM-dd");


                // Limpiamos los itemos de la plaza fuente ya la de destino
                txtPlaza.Items.Clear();
                txtPlaza.Items.Add(NuevoListItem(Constantes.TEXTO_SELECCION, Constantes.TEXTO_BLANCO));
                foreach (DbDominio.Plaza obj in listaDePlaza())
                {
                    txtPlaza.Items.Add(NuevoListItem(obj.Nombre, obj.Id.ToString()));
                }

            }

        }

        protected void GenerarExcel(List<Asistencia.DbDominio.Incidencia> lista)
        {

            var workbook = new HSSFWorkbook();
            var sheet = workbook.CreateSheet("Asistencia");

            var rowIndex = 0;
            var row = sheet.CreateRow(rowIndex);
            row.CreateCell(0).SetCellValue("Id Asistencia");
            row.CreateCell(1).SetCellValue("Empleado");
            row.CreateCell(2).SetCellValue("Fecha Asistencia");
            row.CreateCell(3).SetCellValue("Control de Acceso");
            row.CreateCell(4).SetCellValue("Codigo Planta");
            row.CreateCell(5).SetCellValue("Oficina");
            row.CreateCell(6).SetCellValue("Plaza");
            row.CreateCell(7).SetCellValue("Region");
            row.CreateCell(8).SetCellValue("Zona");
            row.CreateCell(9).SetCellValue("Fecha Alta");
            row.CreateCell(10).SetCellValue("Enviado a Servicio Web");
            rowIndex++;

            foreach (Incidencia inci in lista)
            {
                row = sheet.CreateRow(rowIndex);
                row.CreateCell(0).SetCellValue(inci.CveIncidencia);
                row.CreateCell(1).SetCellValue(inci.Empleado != null ? ("(" + inci.Empleado.CveEmpleado + ")" + inci.Empleado.Nombre) : "");
                row.CreateCell(2).SetCellValue(inci.FechaHoraIncidencia != null ? inci.FechaHoraIncidencia.ToString() : "");
                row.CreateCell(3).SetCellValue(inci.ControlAcceso != null ? inci.ControlAcceso.Nombre : "");
                row.CreateCell(4).SetCellValue(inci.ControlAcceso != null && inci.ControlAcceso.Oficina != null ? inci.ControlAcceso.Oficina.CodigoPlanta : "");
                row.CreateCell(5).SetCellValue(inci.ControlAcceso != null && inci.ControlAcceso.Oficina != null ? inci.ControlAcceso.Oficina.Nombre : "");
                row.CreateCell(6).SetCellValue(inci.ControlAcceso != null && inci.ControlAcceso.Oficina != null ? inci.ControlAcceso.Oficina.NombrePlaza : "");
                row.CreateCell(7).SetCellValue(inci.ControlAcceso != null && inci.ControlAcceso.Oficina != null ? inci.ControlAcceso.Oficina.NombreRegion : "");
                row.CreateCell(8).SetCellValue(inci.ControlAcceso != null && inci.ControlAcceso.Oficina != null ? inci.ControlAcceso.Oficina.NombreZona : "");
                row.CreateCell(9).SetCellValue(inci.FechaAlta != null ? inci.FechaAlta.ToString() : "");
                row.CreateCell(10).SetCellValue(inci.EnviadoWs.HasValue ? inci.EnviadoWs.Value == 1 ? "Si" : "No" : "");
                rowIndex++;
            }

            using (var exportData = new MemoryStream())
            {
                workbook.Write(exportData);
                string saveAsFileName = string.Format("ReporteAsistencia-{0:d}.xls", DateTime.Now.ToString("yyyy-MM-dd"));
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
            log.Info(String.Format("El usuario: [{0}] ha generado el reporte de Asistencia con Fecha Inicial: [{1}] y Fecha Final: [{2}]", UsuarioActual.Nombre, txtFecha.Text, txtFechaFin.Text));

            Plaza plaza = null;

            if (ValidarObjeto(txtPlaza.SelectedValue))
            {
                plaza = plazaDao.GetById(Int32.Parse(txtPlaza.SelectedValue));
            }


            List<Incidencia> l = incidenciaDao.GetListado(plaza, DateTime.Parse(txtFecha.Text), DateTime.Parse(txtFechaFin.Text).AddDays(1));

            if (l == null || l.Count == 0)
            {
                MostrarNotificacion("Lista Vacia", "El reporte no contiene datos en las fechas solicitadas");
            }
            GenerarExcel(l);

        }
    }
}