using Entidades;
using Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VentasWeb
{
    public partial class productos : System.Web.UI.Page
    {
        string script;
        private Producto GenerarProducto() {
            Producto producto=new Producto();
            if (!string.IsNullOrEmpty(txtId.Text))
            {
                producto.Id = Convert.ToInt32(txtId.Text);
                producto.Existe = true;
            }
            else { 
                producto.Id = -1;
                producto.Existe = false;
            }
            producto.Descripcion=txtdescripcion.Text;
            producto.Cantidad=Convert.ToInt32(txtCantidad.Text);
            producto.Precio=Convert.ToDecimal(txtPrecio.Text);

            return producto;
        }
        private void Limpiar() { 
        
            txtId.Text=string.Empty;
            txtdescripcion.Text = string.Empty;
            txtCantidad.Text = string.Empty;
            txtPrecio.Text = string.Empty;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack) {
                    //if (Request.QueryString["Id"]!=null)
                    if (Session["Id_producto"] != null)
                    {
                        Producto producto;
                        LN_Producto logica = new LN_Producto(Config.getConnectionString);
                        producto = logica.ObtenerProducto($"Id={Session["Id_producto"]}");
                        if (producto.Existe)
                        {
                            txtId.Text = producto.Id.ToString();
                            txtdescripcion.Text = producto.Descripcion;
                            txtCantidad.Text = producto.Cantidad.ToString();
                            txtPrecio.Text = Convert.ToInt64(producto.Precio).ToString();
                        }
                        else
                        {
                            Limpiar();
                        }
                    }
                    else { 
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

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Producto producto= GenerarProducto();
                LN_Producto logica=new LN_Producto(Config.getConnectionString);
                int resultado;
                resultado=logica.InsertarModificar(producto);
                if (resultado > 0) {
                    
                    Response.Redirect("ListaProductos.aspx");
                }
                else
                {
                    script = $"javascript:MostrarMensaje('No fue posible ingresar el producto');";
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
            Response.Redirect("ListaProductos.aspx");
        }
    }
}