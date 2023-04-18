<%@ Page Title="" Language="C#" MasterPageFile="~/Plantilla.Master" AutoEventWireup="true" CodeBehind="ventas.aspx.cs" Inherits="VentasWeb.ventas" %>
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
            <div class="col-6 mx-auto">
                <h1>Mantenimiento de Ventas</h1>
                <hr/>
                <asp:TextBox ID="txtID" runat="server" Visible="False"></asp:TextBox>
                <asp:Label ID="Label1" runat="server" Text="Tipo:" CssClass="form-label"></asp:Label>
                <asp:RequiredFieldValidator ID="rfvTipo" runat="server" ErrorMessage="El tipo es necesario" ControlToValidate="drlstTipo" Display="None" ValidationGroup="grupo1"></asp:RequiredFieldValidator>
                <asp:DropDownList ID="drlstTipo" runat="server">
                    <asp:ListItem Selected="True">Seleccione una opci&#243;n</asp:ListItem>
                    <asp:ListItem Value="Credito"></asp:ListItem>
                    <asp:ListItem Value="Contado"></asp:ListItem>
                </asp:DropDownList>
                <br/>
                <br />
                <asp:Label ID="Label3" runat="server" Text="Estado:" CssClass="form-label"></asp:Label>
                <asp:RequiredFieldValidator ID="rfvEstado" runat="server" ErrorMessage="El estado es requerido" ControlToValidate="drlstEstado" Display="None" ValidationGroup="grupo1"></asp:RequiredFieldValidator>
                <asp:DropDownList ID="drlstEstado" runat="server">
                    <asp:ListItem Selected="True">Seleccione una opci&#243;n</asp:ListItem>
                    <asp:ListItem>Cancelado</asp:ListItem>
                    <asp:ListItem>Pendiente</asp:ListItem>
                </asp:DropDownList>
                <asp:Label ID="Label2" runat="server" Text="Cantidad:"></asp:Label>
                <asp:TextBox ID="txtCantidad" runat="server"></asp:TextBox>
                <hr/>
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success mt-2" OnClick="btnGuardar_Click" ValidationGroup="grupo1"/>
                <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="btn btn-secondary mt-2" OnClick="btnRegresar_Click"/>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="grupo1" ForeColor="#990000" CssClass="alert alert-danger mt-3"/>
            </div>
        </div>
    </div>
</asp:Content>
