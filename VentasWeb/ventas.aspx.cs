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
    public partial class ventas : System.Web.UI.Page
    {
        string script;

        private Venta GenerarVenta()
        {
            Venta venta = new Venta();
            if (!string.IsNullOrEmpty(txtID.Text))
            {
                venta.Id = Convert.ToInt32(txtID.Text);
                venta.Existe = true;
            } else {
                venta.Id = -1;
                venta.Existe = false;
            }
            venta.Tipo = drlstTipo.SelectedValue;
            venta.Estado = drlstEstado.SelectedValue;

            return venta;
        }

        private void Limpiar()
        {
            txtID.Text = string.Empty;
            drlstTipo.SelectedIndex = -1;
            drlstEstado.SelectedIndex = -1;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //if (Request.QueryString["Id"]!=null)
                    if (Session["ID_Venta"] != null)
                    {
                        Venta venta;
                        LN_Ventas logica = new LN_Ventas(Config.getConnectionString);
                        venta = logica.ObtenerVenta($"ID={Session["ID_Venta"]}");
                        if (venta.Existe)
                        {
                            txtID.Text = venta.Id.ToString();
                            drlstTipo.SelectedIndex = 0;
                            drlstEstado.SelectedIndex = 0;
                        }
                        else
                        {
                            Limpiar();
                        }
                    }
                    else
                    {
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
            
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListaVentas.aspx");
        }
    }
}