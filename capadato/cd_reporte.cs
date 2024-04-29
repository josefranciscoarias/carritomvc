using capaentidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capadato
{
   public class cd_reporte
    {
        public List<reporte> venta(string fechainicio, string fechafin, string idtransaccion)
        {
            List<reporte> lista = new List<reporte>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cn))
                {
                   
                    SqlCommand cm = new SqlCommand("sp_reporteventas", oconexion);
                    cm.Parameters.AddWithValue("fechainicio", fechainicio);
                    cm.Parameters.AddWithValue("fechafin", fechafin);
                    cm.Parameters.AddWithValue("idtransaccion", idtransaccion);
                    cm.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    using(SqlDataReader dr = cm.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(
                                new reporte()
                                {
                                    fechaventa = dr["fechaventa"].ToString(),
                                    cliente = dr["cliente"].ToString(),
                                    producto = dr["producto"].ToString(),
                                    precio = Convert.ToDecimal(dr["precio"],new CultureInfo("es-PE")),
                                    cantidad =Convert.ToInt32(dr["cantidad"].ToString()),
                                    total = Convert.ToDecimal(dr["total"], new CultureInfo("es-PE")),
                                    idtransaccion = dr["idtransaccion"].ToString()
                                });
                        }


                    }
                }




            }
            catch
            {

                lista = new List<reporte>();
            }


            return lista;
        }





        public dashboard verdashboard()
        {
            dashboard objeto = new dashboard();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cn))
                {
                  
                    SqlCommand cm = new SqlCommand("sp_reportedashboard", oconexion);
                    cm.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    using (SqlDataReader dr = cm.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            objeto = new dashboard() {
                            totalcliente = Convert.ToInt32(dr["totalcliente"]),
                                totalventa = Convert.ToInt32(dr["totalventa"]),
                                totalproducto = Convert.ToInt32(dr["totalproducto"]),

                            };
                               
                        }


                    }
                }




            }
            catch
            {

                objeto = new dashboard();
            }


            return objeto;
        }

    }
}
