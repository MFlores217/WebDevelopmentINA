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
    public partial class ListaProductos : System.Web.UI.Page
    {
        string script;

        private void CargarProductos(string condicion = "")
        {
            LN_Producto logica = new LN_Producto(Config.getConnectionString);
            List<Producto> lista;
            try
            {
                lista = logica.ListarProductos(condicion);

                grdLista.DataSource = lista;
                grdLista.DataBind();


            }
            catch (Exception e)
            {

                throw e;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (!IsPostBack) {
                    CargarProductos();
                }
            }
            catch (Exception ex)
            {

                script = $"javascript:MostrarMensaje('{ex.Message}');";
                ScriptManager.RegisterStartupScript(this, typeof(string), "Mensaje", script, true);
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string condicion;
                condicion = $"descripcion like '%{txtdescripcion.Text}%'";
                CargarProductos(condicion);
            }
            catch (Exception ex)
            {

                script = $"javascript:MostrarMensaje('{ex.Message}');";
                ScriptManager.RegisterStartupScript(this, typeof(string), "Mensaje", script, true);
            }
            
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Session.Remove("Id_producto");
            Response.Redirect("productos.aspx");
        }

        protected void lnkModificar_Command(object sender, CommandEventArgs e)
        {
            Session["Id_producto"]=e.CommandArgument.ToString();
            Response.Redirect("productos.aspx");
            //Response.Redirect("productos.aspx?Id="+ e.CommandArgument.ToString());
        }

        protected void lnkEliminar_Command(object sender, CommandEventArgs e)
        {
            LN_Producto logica = new LN_Producto(Config.getConnectionString);
            Producto producto;
            int id=Convert.ToInt32(e.CommandArgument.ToString());
            try
            {
                producto = logica.ObtenerProducto($"Id={id}");
                if (producto.Existe) {
                    logica.Eliminar(id);
                    CargarProductos();
                }
            }
            catch (Exception ex)
            {

                script = $"javascript:MostrarMensaje('{ex.Message}');";
                ScriptManager.RegisterStartupScript(this, typeof(string), "Mensaje", script, true);
            }

        }

        protected void grdLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grdLista.PageIndex = e.NewPageIndex;
                CargarProductos();
            }
            catch (Exception ex)
            {

                script = $"javascript:MostrarMensaje('{ex.Message}');";
                ScriptManager.RegisterStartupScript(this, typeof(string), "Mensaje", script, true);
            }
            
        }
    }
}