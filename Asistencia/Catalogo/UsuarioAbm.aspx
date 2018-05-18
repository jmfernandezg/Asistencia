<%@ Page Title="Edicion de Usuario" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UsuarioAbm.aspx.cs" Inherits="Asistencia.Catalogo.UsuarioAbm" %>

<asp:Content ID="Liga" ContentPlaceHolderID="PlaceNivel1" runat="server">
    <li>
        <asp:HyperLink runat="server" Text="Usuarios" NavigateUrl="~/Catalogo/Usuario.aspx" />

    </li>
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
                        <asp:Label runat="server" for="txtUsername" Text="Usuario:" />
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtUsername" />
                        <asp:RequiredFieldValidator runat="server"
                            ControlToValidate="txtUsername"
                            ErrorMessage="Es requerido capturar el campo Usuario."
                            ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" for="txtEmail" Text="Correo Electrónico:" />
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtEmail" />
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" for="txtPerfil" Text="Perfil:" />
                        <asp:DropDownList runat="server" CssClass="form-control" ID="txtPerfil" />
                        <asp:RequiredFieldValidator runat="server"
                            ControlToValidate="txtPerfil"
                            ErrorMessage="Es requerido capturar el campo Perfil."
                            ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" for="txtOficina" Text="Planta/Oficina:" />
                        <asp:DropDownList runat="server" CssClass="form-control select2" ID="txtOficina" />
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" ID="lbPasswd" for="txtPasswd" Text="Contraseña:" />
                        <asp:TextBox runat="server" TextMode="Password" CssClass="form-control" ID="txtPasswd" />
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
