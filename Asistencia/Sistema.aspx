<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sistema.aspx.cs" Inherits="Asistencia.Sistema" MasterPageFile="~/Site.Master" Title="Principal" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="Contenido" runat="server">

    <div class="row">
        <asp:Panel runat="server" ID="PanelOficina" CssClass="col-md-3 col-sm-6 col-xs-12">
            <div class="info-box">
                <span class="info-box-icon bg-aqua"><i class="fa fa-tree"></i></span>
                <div class="info-box-content">
                    <span class="info-box-text">Plantas</span>
                    <span class="info-box-number">
                        <asp:HyperLink runat="server" ID="LigaOficina" NavigateUrl="~/Catalogo/Oficina.aspx" /></span>
                </div>
                <!-- /.info-box-content -->
            </div>
            <!-- /.info-box -->
        </asp:Panel>
        <!-- /.col -->
        <asp:Panel runat="server" ID="PanelUsuario" CssClass="col-md-3 col-sm-6 col-xs-12">
            <div class="info-box">
                <span class="info-box-icon bg-red"><i class="fa fa-users"></i></span>
                <div class="info-box-content">
                    <span class="info-box-text">Usuarios</span>
                    <span class="info-box-number">
                        <asp:HyperLink runat="server" ID="LigaUsuario" NavigateUrl="~/Catalogo/Usuario.aspx"/></span>
                </div>
                <!-- /.info-box-content -->
            </div>
            <!-- /.info-box -->
        </asp:Panel>
        <!-- /.col -->

        <!-- fix for small devices only -->
        <div class="clearfix visible-sm-block"></div>

        <asp:Panel runat="server" ID="PanelEmpleado" CssClass="col-md-3 col-sm-6 col-xs-12">
            <div class="info-box">
                <span class="info-box-icon bg-green"><i class="fa fa-car"></i></span>
                <div class="info-box-content">
                    <span class="info-box-text">Empleados</span>
                    <span class="info-box-number"><asp:HyperLink runat="server" ID="LigaEmpleado" NavigateUrl="~/Catalogo/Empleado.aspx" /></span>
                </div>
                <!-- /.info-box-content -->
            </div>
            <!-- /.info-box -->
        </asp:Panel>
        <!-- /.col -->
         <asp:Panel runat="server" ID="PanelControlAcceso" CssClass="col-md-3 col-sm-6 col-xs-12">
           <div class="info-box">
                <span class="info-box-icon bg-yellow"><i class="fa fa-laptop"></i></span>
                <div class="info-box-content">
                    <span class="info-box-text">Controles de Acceso</span>
                    <span class="info-box-number"><asp:HyperLink runat="server" ID="LigaControlAcceso" NavigateUrl="~/Catalogo/ControlAcceso.aspx"/></span>
                </div>
                <!-- /.info-box-content -->
            </div>
            <!-- /.info-box -->
        </asp:Panel>
        <!-- /.col -->
    </div>
    <!-- /.row -->
</asp:Content>
