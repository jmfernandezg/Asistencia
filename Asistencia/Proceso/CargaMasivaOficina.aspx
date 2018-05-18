<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CargaMasivaOficina.aspx.cs" Inherits="Asistencia.Proceso.CargaMasivaOficina" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
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
                        <li>Los encabezados de las columnas del documento son:no nomina,nombre,plaza</li>
                        <li>Todo el Contenido debe estar alineado a la izquierda</li>
                    </ol>
                    <table class="table table-bordered">
                        <tr>
                            <th>CODIGO_PLANTA</th>
                            <th>NOMBRE</th>
                            <th>ZONA</th>
                            <th>REGION</th>
                            <th>PLAZA</th>
                        </tr>
                        <tr>
                            <td>DE25</td>
                            <td>PLANTA DE25</td>
                            <td>PAC</td>
                            <td>BAJ</td>
                            <td>CELAYA</td>
                        </tr>
                        <tr>
                            <td>DE26</td>
                            <td>PLANTA DE26</td>
                            <td>PAC</td>
                            <td>BCN</td>
                            <td>ENSENADA</td>
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