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
    public partial class frmBuscarVentas : Form
    {
        //evento
        public event EventHandler AceptarVenta;
        //variables globales
        private int id_venta = 0;
        public frmBuscarVentas()
        {
            InitializeComponent();
        }
        private void CargarVentas(string condicion = "")
        {
            LN_Ventas logica = new LN_Ventas(Config.getConnectionString);
            List<Venta> lista;
            try
            {
                lista = logica.ListarVentas(condicion);
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
        private void SeleccionarVenta()
        {
            try
            {
                if (grdLista.SelectedRows.Count > 0)
                {
                    id_venta = (int)grdLista.SelectedRows[0].Cells[0].Value;
                    AceptarVenta(id_venta, null);
                    Close();
                }
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            id_venta = -1;
            AceptarVenta(id_venta, null);
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                SeleccionarVenta();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmBuscarVentas_Load(object sender, EventArgs e)
        {
            try
            {
                CargarVentas("upper(estado)='PENDIENTE'");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void grdLista_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                SeleccionarVenta();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string condicion = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(txtNombre.Text))
                {
                    condicion = $"(concat(nombre,' ',APELLIDO) like '%{txtNombre.Text}%' or fecha='{dtpfecha.Value.ToString("yyyy-MM-dd")}') and upper(estado)='PENDIENTE'";
                }
                CargarVentas(condicion);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
