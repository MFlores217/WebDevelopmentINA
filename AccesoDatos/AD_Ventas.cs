using Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace AccesoDatos
{
    public class AD_Ventas
    {
        private string _cadenaConexion;
        private int idventa;

        public int Idventa
        {
            get { return idventa; }
        }
        public AD_Ventas(string cadenaConexion)
        {
            idventa = 0;
            _cadenaConexion = cadenaConexion;
        }

        public int Insertar(Venta venta, Detalle detalle)
        {
            int resultado = 0;
            SqlConnection cnn = new SqlConnection(_cadenaConexion);
            SqlCommand cmd = new SqlCommand();
            string sentencia;

            Venta busqueda;
            Detalle detalleBuscado = new Detalle();
            AD_Detalle AD_Det = new AD_Detalle(_cadenaConexion);
            cmd.Connection = cnn;
            cnn.Open();
            SqlTransaction trans = cnn.BeginTransaction();
            idventa = venta.Id;

            try
            {

                cmd.Transaction = trans;
                busqueda = ObtenerVenta($"v.id={venta.Id}");

                if (!busqueda.Existe)
                {
                    sentencia = "Insert into Ventas(fecha,tipo,clienteId,estado) values(@fecha,@tipo,@clienteId,@estado) select SCOPE_IDENTITY()";
                    cmd.CommandText = sentencia;
                    cmd.Parameters.AddWithValue("@fecha", venta.Fecha);
                    cmd.Parameters.AddWithValue("@tipo", venta.Tipo);
                    cmd.Parameters.AddWithValue("@clienteId", venta.ClienteId);
                    cmd.Parameters.AddWithValue("@estado", venta.Estado);
                    idventa = Convert.ToInt32(cmd.ExecuteScalar());
                    sentencia = "Insert into Detalle(ventaId,productoId,cantidad,precioventa) values(@ventaId, @productoId,@cantidad,@precioventa)";
                    cmd.CommandText = sentencia;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@ventaId", idventa);
                    cmd.Parameters.AddWithValue("@productoId", detalle.ProductoId);
                    cmd.Parameters.AddWithValue("@cantidad", detalle.Cantidad);
                    cmd.Parameters.AddWithValue("@precioventa", detalle.PrecioVenta);
                    cmd.ExecuteNonQuery();
                    trans.Commit();
                    resultado = 1;  //inserto venta y detalle
                }
                else
                {
                    if (busqueda.Estado.ToUpper().Trim() == "PENDIENTE")
                    {
                        sentencia = "Update Ventas set clienteId=@clienteId,Tipo=@tipo where id=@Id";
                        cmd.CommandText = sentencia;
                        cmd.Parameters.AddWithValue("@Id", venta.Id);
                        cmd.Parameters.AddWithValue("@tipo", venta.Tipo);
                        cmd.Parameters.AddWithValue("@clienteId", venta.ClienteId);

                        cmd.ExecuteNonQuery();
                        detalleBuscado = AD_Det.ObtenerDetalle($"productoId={detalle.ProductoId} and ventaId={venta.Id}");
                        if (detalleBuscado.Existe)
                        {
                            detalle.Id = detalleBuscado.Id;
                            sentencia = "Update Detalle set productoid=@productoId, cantidad=@cantidad,precioventa=@precioventa where id=@Id";
                            cmd.CommandText = sentencia;
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@Id", detalle.Id);
                            cmd.Parameters.AddWithValue("@productoId", detalle.ProductoId);
                            cmd.Parameters.AddWithValue("@cantidad", detalle.Cantidad);
                            cmd.Parameters.AddWithValue("@precioventa", detalle.PrecioVenta);
                            cmd.ExecuteNonQuery();
                            resultado = 2;  // actualizo venta y detalle
                        }
                        else
                        {
                            sentencia = "Insert into Detalle(ventaId,productoId,cantidad,precioventa) values(@ventaId, @productoId,@cantidad,@precioventa)";
                            cmd.CommandText = sentencia;
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@ventaId", venta.Id);
                            cmd.Parameters.AddWithValue("@productoId", detalle.ProductoId);
                            cmd.Parameters.AddWithValue("@cantidad", detalle.Cantidad);
                            cmd.Parameters.AddWithValue("@precioventa", detalle.PrecioVenta);
                            cmd.ExecuteNonQuery();
                            resultado = 3;  // actualizo venta e inserto detalle

                        }

                    }
                    else
                    {
                        resultado = 4;  // no se puede actualizar porque la venta esta cancelada  
                    }
                    trans.Commit();
                }

                cnn.Close();
            }
            catch (Exception e)
            {
                trans.Rollback();
                cnn.Close();
                throw e;
            }

            return resultado;
        }

        public Venta ObtenerVenta(string condicion = "")
        {
            DataSet datos = new DataSet();
            SqlConnection cnn = new SqlConnection(_cadenaConexion);
            SqlDataAdapter adapter;
            Venta venta = new Venta();

            string sentencia = "Select v.id,clienteId,Concat(nombre,' ',apellido) as NombreCliente,fecha,tipo,estado from Ventas v inner join Clientes c on v.clienteid=c.id";
            if (!string.IsNullOrEmpty(condicion))
            {
                sentencia = $"{sentencia} where {condicion}";
            }
            try
            {
                adapter = new SqlDataAdapter(sentencia, cnn);
                adapter.Fill(datos, "Ventas");
                //linq lenguaje de c# para manejo de consultas
                if (datos.Tables[0].Rows.Count > 0)
                {
                    venta = (from DataRow registro in datos.Tables[0].Rows
                             select new Venta()
                             {
                                 Id = Convert.ToInt32(registro[0]),
                                 ClienteId = Convert.ToInt32(registro[1]),
                                 NombreCliente = registro[2].ToString(),
                                 Fecha = Convert.ToDateTime(registro[3]),
                                 Tipo = registro[4].ToString(),
                                 Estado = registro[5].ToString(),
                                 Existe = true
                             }).FirstOrDefault();

                }
            }
            catch (Exception e)
            {

                throw e;
            }

            return venta;

        }

        public List<Venta> ListarVentas(string condicion = "")
        {
            DataSet datos = new DataSet();
            SqlConnection cnn = new SqlConnection(_cadenaConexion);
            SqlDataAdapter adapter;
            List<Venta> ventas = new List<Venta>();

            string sentencia = "select VENTAS.ID,NOMBRE + ' ' + APELLIDO as NombreCliente,FECHA,TIPO,CLIENTEID,(select sum(subtotal) from DetallesVenta where DetallesVenta.VENTAID=VENTAS.ID) as Total from VENTAS inner join CLIENTES on CLIENTES.ID=CLIENTEID";
            if (!string.IsNullOrEmpty(condicion))
            {
                sentencia = $"{sentencia} WHERE {condicion}";
            }
            try
            {
                adapter = new SqlDataAdapter(sentencia, cnn);
                adapter.Fill(datos, "Ventas");
                //linq lenguaje de c# para manejo de consultas
                if (datos.Tables[0].Rows.Count > 0)
                    ventas = (from DataRow registro in datos.Tables["Ventas"].Rows
                              select new Venta()
                              {
                                  Id = Convert.ToInt32(registro[0]),
                                  NombreCliente = registro[1].ToString(),
                                  Fecha = Convert.ToDateTime(registro[2]),
                                  Tipo = registro[3].ToString(),
                                  ClienteId = Convert.ToInt32(registro[4]),
                                  Total = Convert.ToDecimal(registro[5]),
                                  Existe = true
                              }
                           ).ToList();
            }
            catch (Exception e)
            {

                throw e;
            }

            return ventas;

        }

        public bool FacturarVenta(int id)
        {
            bool resultado = false;
            SqlConnection cnn = new SqlConnection(_cadenaConexion);
            SqlCommand comando = new SqlCommand();
            string sentencia;
            comando.Connection = cnn;
            string s = id.ToString();
            sentencia = "UPDATE VENTAS SET ESTADO = 'CANCELADO' WHERE ID=@ID";
            comando.CommandText = sentencia;
            comando.Parameters.AddWithValue("@id", id);

            try
            {
                cnn.Open();
                if (comando.ExecuteNonQuery() > 0)
                {
                    resultado = true;
                }
                cnn.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                cnn.Dispose();
                comando.Dispose();
            }

            return resultado;

        }

        public bool Eliminar(int id)
        {
            bool resultado = false;
            SqlConnection cnn = new SqlConnection(_cadenaConexion);
            SqlCommand comando = new SqlCommand();
            string sentencia;
            comando.Connection = cnn;
            sentencia = $"Delete Detalle where VENTAID={id}";
            comando.CommandText = sentencia;
            try{
                cnn.Open();
                if (comando.ExecuteNonQuery() > 0){
                    sentencia = "Delete Ventas WHERE ID=@ID";
                    comando.CommandText = sentencia;
                    comando.Parameters.AddWithValue("@ID", id);
                    if (comando.ExecuteNonQuery() > 0){
                        resultado = true;
                    }
                }
                cnn.Close();
            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {
                cnn.Dispose();
                comando.Dispose();
            }
            return resultado;
        }
    }
}
