<%@ Page Title="Perfiles" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Perfil.aspx.cs" Inherits="Asistencia.Catalogo.Perfil" %>

<asp:Content ID="ContenidoPagina" ContentPlaceHolderID="Contenido" runat="server">

    <div class="row">
        <div class="col-xs-12">
            <div class="box">
                <div class="box-header">
                </div>
                <div class="box-body">

                    <asp:Repeater ID="Repeticion" runat="server">
                        <HeaderTemplate>
                            <table id="tablita" class="table table-bordered table-striped">
                                <caption>
                                    <asp:Button runat="server" ID="BtnNuevo" CssClass="btn btn-primary" Text="Nuevo" OnClick="BtnNuevo_Click" />
                                </caption>
                                <thead>
                                    <tr>
                                        <th>Nombre</th>
                                        <th>Descripción</th>
                                        <th>Acciones</th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("Nombre")%></td>
                                <td><%# Eval("Descripcion")%></td>
                                <td>

                                    <div class="btn-group">
                                        <button type="button" class="btn btn-info">Acciones</button>
                                        <button aria-expanded="false" type="button" class="btn btn-info dropdown-toggle" data-toggle="dropdown">
                                            <span class="caret"></span>
                                            <span class="sr-only">Toggle Dropdown</span>
                                        </button>
                                        <ul class="dropdown-menu" role="menu">
                                            <li>
                                                <asp:LinkButton runat="server" ID="BtnEditar" CommandArgument='<%# Eval("Id")%>' OnClick="BtnEditar_Click"><i class="fa fa-edit"></i>Editar</asp:LinkButton>
                                            </li>
                                        </ul>
                                    </div>
                                </td>
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

</asp:Content>
