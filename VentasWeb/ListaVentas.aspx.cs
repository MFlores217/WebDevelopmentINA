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
    public partial class ListaVentas : System.Web.UI.Page
    {
        string script;

        private void CargarVentas(string condicion="")
        {
            LN_Ventas Logica = new LN_Ventas(Config.getConnectionString);
            List<Venta> Ventas;
            try{
                Ventas = Logica.ListarVentas(condicion);

                grdLista.DataSource = Ventas;
                grdLista.DataBind();
            }
            catch (Exception ex){

                throw ex;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    CargarVentas();
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
                condicion = $"VENTAS.ID = {txtIDFactura.Text}";
                CargarVentas(condicion);
            }
            catch (Exception ex)
            {

                script = $"javascript:MostrarMensaje('{ex.Message}');";
                ScriptManager.RegisterStartupScript(this, typeof(string), "Mensaje", script, true);
            }
        }

        protected void btnNuevaFactura_Click(object sender, EventArgs e)
        {
            Session.Remove("ID_Venta");
            Response.Redirect("ventas.aspx");
        }

        protected void grdLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grdLista.PageIndex = e.NewPageIndex;
                CargarVentas();
            }
            catch (Exception ex)
            {

                script = $"javascript:MostrarMensaje('{ex.Message}');";
                ScriptManager.RegisterStartupScript(this, typeof(string), "Mensaje", script, true);
            }
        }

        protected void lnkModificar_Command(object sender, CommandEventArgs e)
        {
            Session["ID_Venta"] = e.CommandArgument.ToString();
            Response.Redirect("ventas.aspx");
        }

        protected void lnkEliminar_Command(object sender, CommandEventArgs e)
        {
            LN_Ventas logica = new LN_Ventas(Config.getConnectionString);
            Venta venta;
            int id = Convert.ToInt32(e.CommandArgument.ToString());
            try
            {
                venta = logica.ObtenerVenta($"v.ID={id}");
                if (venta.Existe){
                    logica.Eliminar(id);
                    CargarVentas();
                }
            }
            catch (Exception ex)
            {

                script = $"javascript:MostrarMensaje('{ex.Message}');";
                ScriptManager.RegisterStartupScript(this, typeof(string), "Mensaje", script, true);
            }
        }
    }
}