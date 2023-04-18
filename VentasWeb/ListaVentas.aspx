<%@ Page Title="" Language="C#" MasterPageFile="~/Plantilla.Master" AutoEventWireup="true" CodeBehind="ListaVentas.aspx.cs" Inherits="VentasWeb.ListaVentas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function MostrarMensaje(mensaje) {
            alert(mensaje);
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-8 mx-auto">
                <h1>Listado de Ventas</h1>
                <hr/>
                <asp:Label ID="Label1" runat="server" Text="N° Factura:"></asp:Label>
                <asp:TextBox ID="txtIDFactura" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-success mt-2" OnClick="btnBuscar_Click"/>
                <asp:Button ID="btnNuevaFactura" runat="server" Text="Agregar nueva factura" CssClass="btn btn-primary mt-2" OnClick="btnNuevaFactura_Click"/>
                <hr/>
                <asp:GridView ID="grdLista" runat="server" AllowPaging="True" EmptyDataText="No hay registros disponibles" AutoGenerateColumns="False" CellPadding="2" Width="100%" ForeColor="Black" GridLines="None" OnPageIndexChanging="grdLista_PageIndexChanging" PageSize="5" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px">
                    <AlternatingRowStyle BackColor="PaleGoldenrod"></AlternatingRowStyle>
                    <Columns>
                        <asp:BoundField DataField="Fecha" HeaderText="Fecha:"></asp:BoundField>
                        <asp:BoundField DataField="Tipo" HeaderText="Tipo:"></asp:BoundField>
                        <asp:BoundField DataField="NombreCliente" HeaderText="Cliente:"></asp:BoundField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkModificar" runat="server" CommandName="Modificar" CommandArgument='<%# Eval("ID").ToString()  %>' OnCommand="lnkModificar_Command">Modificar</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("ID").ToString() %>' OnCommand="lnkEliminar_Command">Eliminar</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="Tan"></FooterStyle>

                    <HeaderStyle BackColor="Tan" Font-Bold="True"></HeaderStyle>

                    <PagerStyle HorizontalAlign="Center" BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue"></PagerStyle>

                    <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite"></SelectedRowStyle>

                    <SortedAscendingCellStyle BackColor="#FAFAE7"></SortedAscendingCellStyle>

                    <SortedAscendingHeaderStyle BackColor="#DAC09E"></SortedAscendingHeaderStyle>

                    <SortedDescendingCellStyle BackColor="#E1DB9C"></SortedDescendingCellStyle>

                    <SortedDescendingHeaderStyle BackColor="#C2A47B"></SortedDescendingHeaderStyle>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
