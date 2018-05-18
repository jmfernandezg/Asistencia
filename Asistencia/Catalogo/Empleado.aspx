<%@ Page Title="Empleados" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Empleado.aspx.cs" Inherits="Asistencia.Catalogo.Empleado" %>

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
                                    <asp:Button runat="server" ID="BtnExcel" CssClass="btn btn-success" Text="Excel" OnClick="BtnExcel_Click" />
                                </caption>
                                <thead>
                                    <tr>
                                        <th>No. Empleado</th>
                                        <th>Nombre</th>
                                        <th>Plaza</th>
                                        <th>Acciones</th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class='<%# Eval("NombreClaseCSS")%>'>
                                <td><%# Eval("NoEmpleado")%></td>
                                <td><%# Eval("Nombre")%></td>
                                <td><%# Eval("NombrePlaza")%></td>
                                <td>
                                    <div class="btn-group">
                                        <button type="button" class="btn btn-info">Acciones</button>
                                        <button aria-expanded="false" type="button" class="btn btn-info dropdown-toggle" data-toggle="dropdown">
                                            <span class="caret"></span>
                                            <span class="sr-only">Toggle Dropdown</span>
                                        </button>
                                        <ul class="dropdown-menu" role="menu">
                                            <li>
                                                <asp:LinkButton runat="server" ID="BtnEditar" CommandArgument='<%# Eval("CveEmpleado")%>' OnClick="BtnEditar_Click"><i class="fa fa-edit"></i>Editar</asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:LinkButton runat="server" ID="BtnBloquear" Visible='<%#Convert.ToBoolean(Eval("Activo"))%>' CommandArgument='<%# Eval("CveEmpleado")%>' OnClick="BtnBloquear_Click" OnClientClick="return confirm('Por favor confirme que desea bloquear el registro')"><i class="fa fa-lock"></i>Bloquear</asp:LinkButton>
                                                <asp:LinkButton runat="server" ID="BtnDesbloquear" Visible='<%#Convert.ToBoolean(Eval("Activo")) == false%>' CommandArgument='<%# Eval("CveEmpleado")%>' OnClick="BtnDesbloquear_Click" OnClientClick="return confirm('Por favor confirme que desea desbloquear el registro')"><i class="fa fa-unlock"></i>Desbloquear</asp:LinkButton>

                                            </li>
                                            <li>
                                                <asp:LinkButton runat="server" ID="BtnEliminar" CommandArgument='<%# Eval("CveEmpleado")%>' OnClick="BtnEliminar_Click" OnClientClick="return confirm('Por favor confirme que desea eliminar el registro')"><i class="fa fa-trash"></i>Eliminar</asp:LinkButton>
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
