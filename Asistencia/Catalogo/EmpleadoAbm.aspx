<%@ Page Title="Empleados" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmpleadoAbm.aspx.cs" Inherits="Asistencia.Catalogo.EmpleadoAbm" %>

<asp:Content ID="Liga" ContentPlaceHolderID="PlaceNivel1" runat="server">
    <li>
        <asp:HyperLink runat="server" Text="Empleados" NavigateUrl="~/Catalogo/Empleado.aspx" />
    </li>
</asp:Content>
<asp:Content ID="ContenidoPagina" ContentPlaceHolderID="Contenido" runat="server">
    <div class="row">
        <div class="col-xs-12">

            <div class="box">
                <div class="box-body">
                    <div class="form-group">
                        <asp:Label runat="server" for="txtNumeroNomina" Text="No. Nómina:" />
                        <asp:TextBox runat="server" TextMode="Number" CssClass="form-control" ID="txtNumeroNomina" />
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" for="txtNombre" Text="Nombre:" />
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtNombre" />

                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" for="txtPlaza" Text="Plaza:" />
                        <asp:DropDownList runat="server" CssClass="form-control" ID="txtPlaza" />
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
        </div>
    </div>
</asp:Content>
