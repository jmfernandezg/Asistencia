<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ActualizaFechaEnControl.aspx.cs" Inherits="Asistencia.Proceso.ActualizaFechaEnControl" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <asp:UpdatePanel ID="PanelFiltro" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-default">
                        <div class="box-header with-border">
                            <div class="user-block">
                                <span class="username">Actualizar Fecha en Control </span>
                            </div>
                        </div>
                        <div class="box-body">
                            <div class="form-group">
                                <asp:Label runat="server" for="txtPlaza" Text="Plaza:" />
                                <asp:DropDownList runat="server" CssClass="form-control" ID="txtPlaza" AutoPostBack="true" OnSelectedIndexChanged="txtPlaza_SelectedIndexChanged" />
                            </div>
                            <div class="clearfix"></div>
                            <div class="col-xs-5">
                                <asp:Label runat="server" for="txtFecha" Text="Fecha:" />
                                <asp:TextBox runat="server" CssClass="form-control datepicker" ID="txtFecha" />
                            </div>
                            <div class="col-xs-5 input-group bootstrap-timepicker timepicker">
                                <asp:Label runat="server" for="txtHora" Text="Hora:" />
                                <asp:TextBox runat="server" CssClass="form-control timepicker" ID="txtHora" />
                            </div>
                            <div class="clearfix"></div>
                            <div class="col-xs-3">
                                <asp:Button runat="server" Text="Actualizar" ID="BtnActualizar" OnClick="BtnActualizar_Click" CssClass="btn btn-primary" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <div class="row">
                <div class="col-xs-12">
                    <div class="box">
                        <div class="box-header">
                        </div>
                        <div class="box-body">
                            <asp:Repeater ID="Repeticion" runat="server">
                                <HeaderTemplate>
                                    <table id="tablita" class="table table-bordered table-striped">
                                        <thead>
                                            <tr>
                                                <th>
                                                    <input type="checkbox" /></th>
                                                <th>Nombre</th>
                                                <th>IP</th>
                                                <th>Oficina</th>
                                                <th>Plaza</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:CheckBox runat="server" ID="CheckControlAcceso" />
                                            <asp:HiddenField runat="server" ID="identificador" Value='<%#Eval("CveControlAcceso")%>' />
                                        </td>
                                        <td><%# Eval("Nombre")%></td>
                                        <td><%# Eval("DireccionIp")%></td>
                                        <td><%# Eval("NombreOficina")%></td>
                                        <td><%# Eval("NombrePlaza")%></td>
                                    </tr>

                                </ItemTemplate>
                                <FooterTemplate>
                                    </tbody>
                <tfoot>
                </tfoot>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        $('#<%=txtFecha.ClientID%>').datepicker({
            format: 'yyyy/mm/dd',
        })
        $('#<%=txtHora.ClientID%>').timepicker({
            showSeconds: true,
            showMeridian: false
        });
    </script>
</asp:Content>
