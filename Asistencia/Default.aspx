<%@ Page Title="Inicio de Sesión" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Asistencia._Default" %>

<!DOCTYPE html>

<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <meta content='width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no' name='viewport'>
    <title><%: Page.Title %> - Sistema de Control de Asistencia (CASI)</title>

    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />

    <!-- BootStrap! -->
    <link href="~/Content/bootstrap.min.css" rel="stylesheet" />
    <!-- Font Awesome! -->
    <link href="~/Content/font-awesome.min.css" rel="stylesheet" />
    <!-- Dashboard! -->
    <link href="~/Diseno/AdminLTE/css/AdminLTE.min.css" rel="stylesheet" />
    <!-- Toast! -->
    <link href="~/Content/toastr.min.css" rel="stylesheet" />
</head>

<body class="login-page">
    <div class="login-box">
        <div class="login-logo">
           <b>Control</b>Asistencia
        </div>
        <div class="login-box-body">
            <p class="login-box-msg">Inicio de Sesión en el Sistema</p>
            <form runat="server">
                <div class="form-group has-feedback">
                    <asp:TextBox runat="server" ID="txtUsername" CssClass="form-control" placeholder="Usuario" />
                    <span class="glyphicon glyphicon-user form-control-feedback"></span>
                </div>
                <div class="form-group has-feedback">
                    <asp:TextBox runat="server" ID="txtPasswd" TextMode="Password" type="password" CssClass="form-control" placeholder="Contraseña" />
                    <span class="glyphicon glyphicon-lock form-control-feedback"></span>
                </div>
                <div class="row">
                    <div class="col-xs-4">
                        <asp:Button runat="server" ID="btnAceptar" CssClass="btn btn-primary btn-block btn-flat" Text="Aceptar" OnClick="btnAceptar_Click" />
                    </div>
                </div>


                <asp:ScriptManager runat="server">
                    <Scripts>
                        <asp:ScriptReference Name="MsAjaxBundle" />
                        <asp:ScriptReference Name="WebForms.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebForms.js" />
                        <asp:ScriptReference Name="WebUIValidation.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebUIValidation.js" />
                        <asp:ScriptReference Name="MenuStandards.js" Assembly="System.Web" Path="~/Scripts/WebForms/MenuStandards.js" />
                        <asp:ScriptReference Name="GridView.js" Assembly="System.Web" Path="~/Scripts/WebForms/GridView.js" />
                        <asp:ScriptReference Name="DetailsView.js" Assembly="System.Web" Path="~/Scripts/WebForms/DetailsView.js" />
                        <asp:ScriptReference Name="TreeView.js" Assembly="System.Web" Path="~/Scripts/WebForms/TreeView.js" />
                        <asp:ScriptReference Name="WebParts.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebParts.js" />
                        <asp:ScriptReference Name="Focus.js" Assembly="System.Web" Path="~/Scripts/WebForms/Focus.js" />
                        <asp:ScriptReference Name="WebFormsBundle" />

                        <asp:ScriptReference Path="~/Scripts/jquery-3.1.1.min.js" />
                        <asp:ScriptReference Path="~/Scripts/jquery-ui-1.12.1.min.js" />
                        <asp:ScriptReference Path="~/Scripts/bootstrap.min.js" />
                        <asp:ScriptReference Path="~/Scripts/toastr.min.js" />
                        <asp:ScriptReference Path="~/Diseno/Javascript/comun.js" />
                        <asp:ScriptReference Path="~/Diseno/Javascript/spin.min.js" />
                    </Scripts>
                </asp:ScriptManager>
            </form>
        </div>
    </div>

</body>
</html>
