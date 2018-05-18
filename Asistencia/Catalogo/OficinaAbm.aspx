<%@ Page Title="Planta / Oficina" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OficinaAbm.aspx.cs" Inherits="Asistencia.Catalogo.OficinaAbm" %>

<asp:Content ID="Liga" ContentPlaceHolderID="PlaceNivel1" runat="server">
    <li>
        <asp:HyperLink runat="server" Text="Planta / Oficina" NavigateUrl="~/Catalogo/Oficina.aspx" />

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

                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" for="txtCodigoPlanta" Text="Codigo Planta:" />
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtCodigoPlanta" />

                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" for="txtZona" Text="Zona:" />
                        <div class="input-group">
                            <div class="input-group-btn">
                                <button type="button" class="btn btn-info" data-toggle="modal" data-target="#modalZona">+</button>
                            </div>
                            <asp:DropDownList runat="server" CssClass="form-control" ID="txtZona" />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" for="txtRegion" Text="Región:" />

                        <div class="input-group">
                            <div class="input-group-btn">
                                <button type="button" class="btn btn-info" data-toggle="modal" data-target="#modalRegion">+</button>
                            </div>
                            <asp:DropDownList runat="server" CssClass="form-control" ID="txtRegion" />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" for="txtPlaza" Text="Plaza:" />

                        <div class="input-group">
                            <div class="input-group-btn">
                                <button type="button" class="btn btn-info" data-toggle="modal" data-target="#modalPlaza">+</button>
                            </div>
                            <asp:DropDownList runat="server" CssClass="form-control" ID="txtPlaza" />
                        </div>

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
            <div id="modalZona" class="modal fade" role="dialog">
                <div class="modal-dialog">

                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Nueva Zona</h4>
                        </div>
                        <div class="modal-body">
                            <div class="form-group">
                                <asp:Label runat="server" for="txtNuevaZona" Text="Nombre:" />
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtNuevaZona" />
                            </div>

                        </div>
                        <div class="modal-footer">
                            <asp:Button runat="server" ID="btnNuevaZona" OnClick="btnNuevaZona_Click" Text="Aceptar" CssClass="btn btn-primary" />
                            <button type="button" class="btn btn-warning" data-dismiss="modal">Cerrar</button>
                        </div>
                    </div>

                </div>
            </div>


            <div id="modalRegion" class="modal fade" role="dialog">
                <div class="modal-dialog">

                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Nueva Region</h4>
                        </div>
                        <div class="modal-body">
                            <div class="form-group">
                                <asp:Label runat="server" for="txtNuevaRegion" Text="Nombre:" />
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtNuevaRegion" />
                            </div>

                        </div>
                        <div class="modal-footer">
                            <asp:Button runat="server" ID="btnNuevaRegion" OnClick="btnNuevaRegion_Click" Text="Aceptar" CssClass="btn btn-primary" />
                            <button type="button" class="btn btn-warning" data-dismiss="modal">Cerrar</button>
                        </div>
                    </div>

                </div>
            </div>



            <div id="modalPlaza" class="modal fade" role="dialog">
                <div class="modal-dialog">

                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Nueva Plaza</h4>
                        </div>
                        <div class="modal-body">
                            <div class="form-group">
                                <asp:Label runat="server" for="txtNuevaPlaza" Text="Nombre:" />
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtNuevaPlaza" />
                            </div>

                        </div>
                        <div class="modal-footer">
                            <asp:Button runat="server" ID="btnNuevaPlaza" OnClick="btnNuevaPlaza_Click" Text="Aceptar" CssClass="btn btn-primary" />
                            <button type="button" class="btn btn-warning" data-dismiss="modal">Cerrar</button>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>
