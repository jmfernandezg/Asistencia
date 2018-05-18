<%@ Page Title="Mi Perfil" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MiPerfil.aspx.cs" Inherits="Asistencia.Catalogo.MiPerfil" %>

<asp:Content ID="Princ" ContentPlaceHolderID="Contenido" runat="server">
    <div class="row">
        <div class="col-xs-12">
            <div class="box">
                <div class="box-header with-border">
                    <h3 class="box-title">Editar Datos</h3>
                </div>
                <div class="box-body">

                    <div class="form-group">
                        <asp:Label runat="server" for="txtNombre" Text="Nombre:" />
                        <asp:TextBox runat="server" ReadOnly="true" CssClass="form-control" ID="txtNombre" />
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" for="txtUsuario" Text="Usuario:" />
                        <asp:TextBox runat="server" ReadOnly="true" CssClass="form-control" ID="txtUsuario" />
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" for="txtCorreoElectronico" Text="Correo Electrónico:" />
                        <asp:TextBox runat="server" TextMode="Email" CssClass="form-control" ID="txtCorreoElectronico" />
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" for="txtContrasena1" Text="Cambiar Contraseña:" />
                        <asp:TextBox runat="server" TextMode="Password" CssClass="form-control" ID="txtContrasena1" />
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" for="txtContrasena2" Text="Confirmar Contraseña a Cambiar:" />
                        <asp:TextBox runat="server" TextMode="Password" CssClass="form-control" ID="txtContrasena2" />
                    </div>
                </div>
                <!-- /.box-body -->
                <div class="box-footer">
                    <asp:Button runat="server" ID="btnAceptar" OnClick="btnAceptar_Click" Text="Aceptar" CssClass="btn btn-primary" />
                </div>
                <!-- /.box-footer-->
            </div>
            <!-- /.box -->
        </div>
    </div>

</asp:Content>
