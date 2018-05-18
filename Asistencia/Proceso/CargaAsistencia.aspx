<%@ Page Title="Carga Asistencia" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CargaAsistencia.aspx.cs" Inherits="Asistencia.Proceso.CargaAsistencia" %>

<asp:Content ID="ContenidoPagina" ContentPlaceHolderID="Contenido" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="box box-success collapsed-box">
                <div class="box-header with-border">
                    <div class="user-block">
                        <span class="username">Instrucciones</span>
                    </div>
                    <div class="box-tools">
                        <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-plus"></i></button>
                    </div>
                </div>
                <div class="box-body">
                    <ol>
                        <li>El documento debe ser un excel(xls o xlsx)</li>
                        <li>El contenido es: Numero de Nomina, Fecha y Hora, Id del Control de Acceso y Si es entrada o salida</li> 
                    </ol>
                    <table class="table table-bordered">
                        <tr>
                            <th>Numero Nomina (Empleado)</th>
                            <th>Fecha y Hora</th>
                            <th>ID Control de Acceso</th>
                            <th>Entrada/Salida</th>
                        </tr>
                        <tr>
                            <td>1</td>
                            <td>2015-09-08 01:02:03</td>
                            <td>10</td>
                            <td>0</td>
                        </tr>

                        <tr>
                            <td>2</td>
                            <td>2015-09-10 03:04:05</td>
                            <td>11</td>
                            <td>1</td>
                        </tr>
                        <tr>
                            <td>3</td>
                            <td>2015-09-11 04:05:06</td>
                            <td>12</td>
                            <td>0</td>
                        </tr>


                    </table>
                </div>
                <div class="box-footer box-comments">
                </div>
                <div class="box-footer">
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="box box-default">
                <div class="box-header with-border">
                    <div class="user-block">
                        <span class="username">Carga de Archivo</span>
                    </div>
                </div>
                <div class="box-body">
                    <div class="form-group">
                        <asp:FileUpload ID="CargaArchivo" AllowMultiple="false"
                            runat="server" />
                        <asp:Button runat="server" ID="btnSubir" Text="Iniciar Proceso" OnClick="btnSubir_Click" CssClass="btn btn-primary" />
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="CajaRegistro" Text="Resultado del Proceso"/>
                        <asp:ListBox ID="CajaRegistro" runat="server" CssClass="form-control" />
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
