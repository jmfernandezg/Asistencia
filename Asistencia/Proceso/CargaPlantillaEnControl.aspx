<%@ Page Title="Carga Plantilla en Control" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CargaPlantillaEnControl.aspx.cs" Inherits="Asistencia.Proceso.CargaPlantillaEnControl" %>

<asp:Content ID="ContenidoPagina" ContentPlaceHolderID="Contenido" runat="server">
    <asp:UpdatePanel ID="PanelFiltro" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row">

                <div class="col-md-6">
                    <div class="box box-default">
                        <div class="box-header with-border">
                            <div class="user-block">
                                <span class="username">Seleccionar Plantilla Fuente</span>
                            </div>
                        </div>
                        <div class="box-body">
                            <div class="col-xs-6">
                                <asp:Label runat="server" for="txtPlaza" Text="Plaza:" />
                                <asp:DropDownList runat="server" CssClass="form-control" ID="txtPlaza" AutoPostBack="true" OnSelectedIndexChanged="txtPlaza_SelectedIndexChanged" />
                            </div>
                            <div class="col-xs-6">
                                <asp:Label runat="server" for="txtPlantilla" Text="Plantilla:" />
                                <asp:DropDownList runat="server" CssClass="form-control" ID="txtPlantilla" AutoPostBack="true" OnSelectedIndexChanged="txtPlantilla_SelectedIndexChanged" />
                            </div>
                        </div>
                        <div class="box-footer">
                            .
                        </div>
                    </div>
                </div>


                <div class="col-md-6">
                    <div class="box box-default">
                        <div class="box-header with-border">
                            <div class="user-block">
                                <span class="username">Seleccionar Control Destino</span>
                            </div>
                        </div>
                        <div class="box-body">
                            <div class="col-xs-6">
                                <asp:Label runat="server" for="txtPlazaDestino" Text="Plaza:" />
                                <asp:DropDownList runat="server" CssClass="form-control" ID="txtPlazaDestino" AutoPostBack="true" OnSelectedIndexChanged="txtPlazaDestino_SelectedIndexChanged" />
                            </div>
                            <div class="col-xs-6">
                                <asp:Label runat="server" for="txtControlDestino" Text="Control:" />
                                <asp:DropDownList runat="server" CssClass="form-control" ID="txtControlDestino" />
                            </div>
                        </div>

                        <div class="box-footer">
                            <asp:Button runat="server" Text="Aceptar" CssClass="btn btn-warning" ID="BtnCargarPlantilla" OnClick="BtnAceptar_Click" OnClientClick="return Confirm('Por favor confirme que desea cargar la plantilla en el control seleccionado');"/>
                        </div>
                    </div>
                </div>

            </div>

            <div class="row">
                <div class="col-xs-12">
                    <div class="box">
                        <div class="box-header with-border">
                            <div class="user-block">
                                <span class="username">Plantilla Seleccionada</span>
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
