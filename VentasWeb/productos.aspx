<%@ Page Title="" Language="C#" MasterPageFile="~/Plantilla.Master" AutoEventWireup="true" CodeBehind="productos.aspx.cs" Inherits="VentasWeb.productos" %>
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
                <h1>Mantenimiento de Productos</h1>
                <asp:TextBox ID="txtId" runat="server" Visible="False"></asp:TextBox>
                <asp:Label ID="Label1" runat="server" Text="Descripción" CssClass="form-label"></asp:Label>
                <asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" ErrorMessage="La descripción es requerida" ControlToValidate="txtdescripcion" Display="None" ValidationGroup="grupo1"></asp:RequiredFieldValidator>
                <asp:TextBox ID="txtdescripcion" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:Label ID="Label2" runat="server" Text="Cantidad" CssClass="form-label"></asp:Label>
                <asp:RequiredFieldValidator ID="rfvCantidad" runat="server" ErrorMessage="La cantidad es requerida" ControlToValidate="txtCantidad" Display="None" ValidationGroup="grupo1"></asp:RequiredFieldValidator>
                <asp:TextBox ID="txtCantidad" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
                <asp:Label ID="Label3" runat="server" Text="Precio" CssClass="form-label"></asp:Label>
                <asp:RequiredFieldValidator ID="rfvPrecio" runat="server" ErrorMessage="El precio es requerido" ControlToValidate="txtPrecio" Display="None" ValidationGroup="grupo1"></asp:RequiredFieldValidator>
                <asp:TextBox ID="txtPrecio" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success mt-2" OnClick="btnGuardar_Click" ValidationGroup="grupo1"/>
                <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="btn btn-secondary mt-2" OnClick="btnRegresar_Click"/>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="grupo1" ForeColor="#990000" CssClass="alert alert-danger mt-3"/>
            </div>
        </div>
    </div>

</asp:Content>
