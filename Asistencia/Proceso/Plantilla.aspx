<%@ Page Title="Plantilla" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Plantilla.aspx.cs" Inherits="Asistencia.Proceso.Plantilla" %>

<asp:Content ID="ContenidoPagina" ContentPlaceHolderID="Contenido" runat="server">
    <asp:UpdatePanel ID="PanelFiltro" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-default">
                        <div class="box-header with-border">
                            <div class="user-block">
                                <span class="username">Descargar Usuarios del Control de Acceso</span>
                            </div>
                        </div>
                        <div class="box-body">
                            <div class="col-xs-5">
                                <asp:Label runat="server"  AssociatedControlID="txtPlaza" Text="Plaza:" />
                                <asp:DropDownList runat="server" CssClass="form-control select2" ID="txtPlaza" AutoPostBack="true" OnSelectedIndexChanged="txtPlaza_SelectedIndexChanged" />
                            </div>
                            <div class="col-xs-5">
                                <asp:Label runat="server" AssociatedControlID="txtControlAcceso" Text="Control:" />
                                <asp:DropDownList runat="server" CssClass="form-control select2" ID="txtControlAcceso" AutoPostBack="true" OnSelectedIndexChanged="txtControlAcceso_SelectedIndexChanged" />
                            </div>
                            <div class="col-xs-3">
                                <asp:Button runat="server" Text="Descargar" ID="BtnDescargar" OnClick="BtnDescargar_Click" CssClass="btn btn-primary" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <div class="row">
                <div class="col-xs-12">
                    <div class="box">
                        <div class="box-header with-border">
                            <div class="user-block">
                                <span class="username">Plantillas en Plaza / Control</span>
                            </div>
                        </div>
                        <div class="box-body">
                            <asp:Repeater ID="Repeticion" runat="server">
                                <HeaderTemplate>
                                    <table id="tablita" class="table table-bordered table-striped">
                                        <thead>
                                            <tr>
                                                <th>Control</th>
                                                <th>IP</th>
                                                <th>Enrollnumber</th>
                                                <th>Nombre</th>
                                                <th>Privilege</th>
                                                <th>Password</th>
                                                <th>Flag</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td><%# Eval("NombreControl")%></td>
                                        <td><%# Eval("IpControlTemplate")%></td>
                                        <td><%# Eval("Enrollnumber")%></td>
                                        <td><%# Eval("Nombre")%></td>
                                        <td><%# Eval("Privilege")%></td>
                                        <td><%# Eval("Password")%></td>
                                        <td><%# Eval("Flag")%></td>
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

</asp:Content>
