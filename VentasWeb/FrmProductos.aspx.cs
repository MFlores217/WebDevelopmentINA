using BussisnesLogic;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VentasWeb
{
    public partial class FrmProductos : System.Web.UI.Page
    {
        string script;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if(Session["Id_producto"] != null)
                    {
                        EntidadProducto producto;
                        BL_Producto logica = new BL_Producto(Configuracion.getConnectionString);
                        producto = logica.ObtenerProducto($"Id={Session["Id_producto"]}");
                        if (producto.Existe) {
                            txtId.Text = producto.Id.ToString();
                            txtDescripcion.Text = producto.Descripcion.ToString();
                            txtCantidad.Text = producto.Cantidad.ToString();
                            txtPrecio.Text = producto.Precio.ToString();
                        } else {
                            Limpiar();
                        }
                    } else {
                        Limpiar();
                    }
                }
            }
            catch (Exception ex)
            {

                script = $"javascript:MostrarMensaje('{ex.Message}');";
                ScriptManager.RegisterStartupScript(this, typeof(string), "Mensaje", script, true);
            }
        }

        private EntidadProducto GenerarProducto()
        {
            EntidadProducto producto = new EntidadProducto();
            if (!string.IsNullOrEmpty(txtId.Text))
            {
                producto.Id = Convert.ToInt32(txtId.Text);
                producto.Existe = true;
            } else {
                producto.Id = -1;
                producto.Existe = false;
            }
            producto.Descripcion = txtDescripcion.Text;
            producto.Cantidad = Convert.ToInt32(txtCantidad.Text);
            producto.Precio = Convert.ToDecimal(txtPrecio.Text);

            return producto;
        }

        private void Limpiar()
        {
            txtId.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtCantidad.Text = string.Empty;
            txtPrecio.Text = string.Empty;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                EntidadProducto producto = GenerarProducto();
                BL_Producto logica = new BL_Producto(Configuracion.getConnectionString);
                int resultado;
                resultado = logica.InsertarModificar(producto);
                if(resultado > 0)
                {
                    script = $"javascript:MostrarMensaje('Producto ingresado satisfactoriamente');";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "Mensaje", script, true);
                    Response.Redirect("FrmListaProductos.aspx");
                } else {
                    script = $"javascript:MostrarMensaje('No fue posible insertar el producto');";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "Mensaje", script, true);
                    Limpiar();
                }
            }
            catch (Exception ex)
            {

                script = $"javascript:MostrarMensaje('{ex.Message}');";
                ScriptManager.RegisterStartupScript(this, typeof(string), "Mensaje", script, true);
            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("FrmListaProductos.aspx");
        }
    }
}