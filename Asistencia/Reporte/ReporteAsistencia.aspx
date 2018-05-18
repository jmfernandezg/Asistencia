<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReporteASistencia.aspx.cs" Inherits="Asistencia.Reporte.ReporteAsistencia" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="box box-default">
                <div class="box-header with-border">
                    <div class="user-block">
                        <span class="username">Reporte de Asistencia</span>
                    </div>
                </div>
                <div class="box-body">
                    <div class="clearfix"></div>
                    <div class="col-xs-8">
                        <asp:Label runat="server" for="txtPlaza" Text="Plaza:" />
                        <asp:DropDownList runat="server" CssClass="form-control" ID="txtPlaza" />
                    </div>
                    <div class="col-xs-5">
                        <asp:Label runat="server" for="txtFecha" Text="Fecha Inicial:" />
                        <asp:TextBox runat="server" CssClass="form-control datepicker" ID="txtFecha" />
                    </div>
                    <div class="col-xs-5">
                        <asp:Label runat="server" for="txtFecha" Text="Fecha Final:" />
                        <asp:TextBox runat="server" CssClass="form-control datepicker" ID="txtFechaFin" />
                    </div>
                </div>
                <div class="box-footer">
                    <asp:Button runat="server" Text="Aceptar" ID="BtnActualizar" CssClass="btn btn-primary" OnClick="BtnActualizar_Click" />

                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        $('#<%=txtFecha.ClientID%>').datepicker({
            format: 'yyyy-mm-dd',
        })
        $('#<%=txtFechaFin.ClientID%>').datepicker({
            format: 'yyyy-mm-dd',
        })
    </script>
</asp:Content>
