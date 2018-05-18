<%@ Page Title="Edicion de Perfil" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PerfilAbm.aspx.cs" Inherits="Asistencia.Catalogo.PerfilAbm" %>

<asp:Content ID="Liga" ContentPlaceHolderID="PlaceNivel1" runat="server">
    <li>
        <asp:HyperLink runat="server" Text="Perfiles" NavigateUrl="~/Catalogo/Perfil.aspx" /></li>
</asp:Content>
<asp:Content ID="ContenidoPagina" ContentPlaceHolderID="Contenido" runat="server">
    <div class="row">
        <div class="col-xs-12">
            <div class="box">
                <div class="box-body">

                    <div class="form-group">
                        <asp:Label runat="server" for="txtNombre" Text="Nombre:" />
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtNombre" />
                        <asp:RequiredFieldValidator runat="server"
                            ControlToValidate="txtNombre"
                            ErrorMessage="Es requerido capturar el campo Nombre."
                            ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" for="txtDescripcion" Text="Descripción:" />
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtDescripcion" />
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" for="CheckCatalogoPerfiles" Text="Permiso Catálogo Perfiles:" />
                        <asp:CheckBox runat="server" CssClass="form-control" ID="CheckCatalogoPerfiles" />
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" for="CheckCatalogoUsuarios" Text="Permiso Catálogo Usuarios:" />
                        <asp:CheckBox runat="server" CssClass="form-control" ID="CheckCatalogoUsuarios" />
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" for="CheckCatalogoOficinas" Text="Permiso Catalogo Plantas:" />
                        <asp:CheckBox runat="server" CssClass="form-control" ID="CheckCatalogoOficinas" />
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" for="CheckCatalogoEmpleados" Text="Permiso Catálogo Empleados:" />
                        <asp:CheckBox runat="server" CssClass="form-control" ID="CheckCatalogoEmpleados" />
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" for="CheckCatalogoControlAcceso" Text="Permiso Catálogo Controles de Acceso:" />
                        <asp:CheckBox runat="server" CssClass="form-control" ID="CheckCatalogoControlAcceso" />
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" for="CheckProcesoCargaMasivaEmpleado" Text="Permiso Proceso Carga Masiva Empleado:" />
                        <asp:CheckBox runat="server" CssClass="form-control" ID="CheckProcesoCargaMasivaEmpleado" />
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" for="CheckProcesoCargaMasivaPlanta" Text="Permiso Proceso Carga Masiva Planta:" />
                        <asp:CheckBox runat="server" CssClass="form-control" ID="CheckProcesoCargaMasivaPlanta" />
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" for="CheckProcesoPlantilla" Text="Permiso Proceso Plantilla:" />
                        <asp:CheckBox runat="server" CssClass="form-control" ID="CheckProcesoPlantilla" />
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" for="CheckProcesoEmpleadoEnControles" Text="Permiso Proceso Empleado en Controles:" />
                        <asp:CheckBox runat="server" CssClass="form-control" ID="CheckProcesoEmpleadoEnControles" />
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" for="CheckProcesoCargaPlantillaEnControles" Text="Permiso Proceso Carga Plantilla En Controles:" />
                        <asp:CheckBox runat="server" CssClass="form-control" ID="CheckProcesoCargaPlantillaEnControles" />
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" for="CheckProcesoCargaAsistencia" Text="Permiso Proceso Carga Asistencia:" />
                        <asp:CheckBox runat="server" CssClass="form-control" ID="CheckProcesoCargaAsistencia" />
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" for="CheckProcesoActualizarFechaEnControl" Text="Permiso Proceso Actualizar Fecha En Control:" />
                        <asp:CheckBox runat="server" CssClass="form-control" ID="CheckProcesoActualizarFechaEnControl" />
                    </div>

                    <div class="form-group">
                        <asp:Label runat="server" for="CheckPermisoReporteAsistencia" Text="Permiso Reporte Asistencia:" />
                        <asp:CheckBox runat="server" CssClass="form-control" ID="CheckPermisoReporteAsistencia" />
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" for="CheckPermisoReporteEmpleadoNoRegistrado" Text="Permiso Reporte Empleado No Registrado:" />
                        <asp:CheckBox runat="server" CssClass="form-control" ID="CheckPermisoReporteEmpleadoNoRegistrado" />
                    </div>

                </div>
                <!-- /.box-body -->
                <div class="box-footer">
                    <asp:HiddenField ID="txtId" runat="server" />
                    <asp:Button runat="server" ID="btnAceptar" OnClick="btnAceptar_Click" Text="Aceptar" CssClass="btn btn-primary" />
                    <asp:Button runat="server" ID="btnRegresar" OnClick="btnRegresar_Click" Text="Regresar" CssClass="btn btn-warning" />

                </div>
                <!-- /.box-footer-->
            </div>
            <!-- /.box -->
        </div>
    </div>
</asp:Content>
