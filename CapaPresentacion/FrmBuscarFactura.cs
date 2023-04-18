using Entidades;
using Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class FrmBuscarFactura : Form
    {
        public FrmBuscarFactura()
        {
            InitializeComponent();
        }


        private void CargarFacturas(string condicion = "")
        {
            LN_Ventas logica = new LN_Ventas(Config.getConnectionString);
            Venta lista;
            try
            {
                lista = logica.ObtenerVenta(condicion);
                //if (lista.Count > 0)
                //{
                grdLista.DataSource = lista;

                //}
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }


}
