<%@ Page Title="Control de Acceso" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ControlAccesoAbm.aspx.cs" Inherits="Asistencia.Catalogo.ControlAccesoAbm" %>

<asp:Content ID="Liga" ContentPlaceHolderID="PlaceNivel1" runat="server">
    <li>
        <asp:HyperLink runat="server" Text="Control de Acceso" NavigateUrl="~/Catalogo/ControlAcceso.aspx" />
    </li>
</asp:Content>
<asp:Content ID="ContenidoPagina" ContentPlaceHolderID="Contenido" runat="server">
    <div class="row">
        <div class="col-xs-12">
            <div class="box">
                <div class="box-body">
                    <div class="form-group">
                        <asp:Label runat="server" for="txtIdControl" Text="ID Control:" />
                        <asp:TextBox runat="server" TextMode="Number" CssClass="form-control" ID="txtIdControl" />
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" for="txtNombre" Text="Nombre:" />
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtNombre" />
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" for="txtMarca" Text="Marca:" />
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtMarca" />

                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" for="txtModelo" Text="Modelo:" />
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtModelo" />

                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" for="txtVersionFirmware" Text="Versión Firmware:" />
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtVersionFirmware" />

                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" for="txtCapacidadEnHuellas" Text="Capacidad en Huellas:" />
                        <asp:TextBox runat="server" TextMode="Number" CssClass="form-control" ID="txtCapacidadEnHuellas" />
                    </div>

                    <div class="form-group">
                        <asp:Label runat="server" for="txtOficina" Text="Planta:" />
                        <asp:DropDownList runat="server" CssClass="form-control" ID="txtOficina" />
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" for="txtDireccionIP" Text="Dirección IP:" />
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtDireccionIP" />
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" for="txtPuerto" Text="Puerto:" />
                        <asp:TextBox runat="server" TextMode="Number" CssClass="form-control" ID="txtPuerto" />
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
