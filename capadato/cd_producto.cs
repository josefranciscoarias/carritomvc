using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using capaentidad;
using System.Data;
using System.Data.SqlClient;

namespace capadato
{
   public class cd_producto
    {
        public List<producto> Listar()
        {
            List<producto> lista = new List<producto>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cn))
                {
                    StringBuilder la = new StringBuilder();
                    la.AppendLine("select p.idproducto, p.nombre, p.descripcion,");
                    la.AppendLine("m.idmarca,m.descripcion[desmarca],");
                    la.AppendLine("c.idcategoria, c.descripcion[descategoria],");
                    la.AppendLine("p.precio, p.stock, p.rutaimagen, p.nombreimagen, p.activo");
                    la.AppendLine("from producto p");
                    la.AppendLine("inner join marca m on m.idmarca = p.idmarca");
                    la.AppendLine("inner join categoria c on c.idcategoria = p.idcategoria");
                    SqlCommand cm = new SqlCommand(la.ToString(), oconexion);
                    cm.CommandType = CommandType.Text;
                    oconexion.Open();
                    using (SqlDataReader dr = cm.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(
                                new producto()
                                {
                                    idproducto = Convert.ToInt32(dr["idproducto"]),
                                    nombre= dr["nombre"].ToString(),
                                    descripcion = dr["descripcion"].ToString(),
                                    omarca = new marca() { idmarca = Convert.ToInt32 (dr["idmarca"]), descripcion = dr["desmarca"].ToString()},
                                    ocategoria = new categoria() { idcategoria= Convert.ToInt32(dr["idcategoria"]), descripcion= dr["descategoria"].ToString()},
                                    precio = Convert.ToDecimal(dr["precio"]),
                                    stock = Convert.ToInt32(dr["stock"]),
                                    rutaimagen= dr["rutaimagen"].ToString(),
                                    nombreimagen = dr["nombreimagen"].ToString(),
                                    activo = Convert.ToBoolean(dr["activo"])
                                });
                        }


                    }
                }




            }
            catch
            {

                lista = new List<producto>();
            }


            return lista;
        }
        public int registrar(producto obj, out string mensaje)
        {
            int idautogenerado = 0;
            mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_registrarproducto", oconexion);
                    cmd.Parameters.AddWithValue("nombre", obj.nombre);
                    cmd.Parameters.AddWithValue("descripcion", obj.descripcion);
                    cmd.Parameters.AddWithValue("idmarca", obj.omarca.idmarca);
                    cmd.Parameters.AddWithValue("idcategoria", obj.ocategoria.idcategoria);
                    cmd.Parameters.AddWithValue("precio", obj.precio);
                    cmd.Parameters.AddWithValue("stock", obj.stock);

                    cmd.Parameters.AddWithValue("activo", obj.activo);
                    cmd.Parameters.Add("resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    idautogenerado = Convert.ToInt32(cmd.Parameters["resultado"].Value);
                    mensaje = cmd.Parameters["mensaje"].Value.ToString();
                }

            }
            catch (Exception ex)
            {
                idautogenerado = 0;
                mensaje = ex.Message;


            }
            return idautogenerado;


        }


        public bool editar(producto obj, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_editarproducto", oconexion);
                    cmd.Parameters.AddWithValue("idproducto", obj.idproducto);
                    cmd.Parameters.AddWithValue("nombre", obj.nombre);
                    cmd.Parameters.AddWithValue("descripcion", obj.descripcion);
                    cmd.Parameters.AddWithValue("idmarca", obj.omarca.idmarca);
                    cmd.Parameters.AddWithValue("idcategoria", obj.ocategoria.idcategoria);
                    cmd.Parameters.AddWithValue("precio", obj.precio);
                    cmd.Parameters.AddWithValue("stock", obj.stock);
                    
                    cmd.Parameters.AddWithValue("activo", obj.activo);
                    cmd.Parameters.Add("resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["resultado"].Value);
                    mensaje = cmd.Parameters["mensaje"].Value.ToString();
                }

            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;


            }
            return resultado;


        }
        public bool guardardatosimagen(producto oproducto, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cn))
                {
                    string query = "update producto set rutaimagen = @rutaimagen, nombreimagen = @nombreimagen where idproducto = @idproducto";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                  
                    cmd.Parameters.AddWithValue("rutaimagen", oproducto.rutaimagen);
                    cmd.Parameters.AddWithValue("nombreimagen", oproducto.nombreimagen);

                    cmd.Parameters.AddWithValue("idproducto", oproducto.idproducto);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        resultado = true;
                    }
                    else {
                        mensaje = "no se pudo actualizar imagen";
                    }
                   
                }

            }
            catch (Exception ex)
            {
                resultado= false;
                mensaje = ex.Message;


            }
            return resultado;

        }

        public bool eliminar(int id, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_eliminarproducto", oconexion);
                    cmd.Parameters.AddWithValue("idproducto", id);
                    cmd.Parameters.Add("resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.Bit).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["resultado"].Value);
                    mensaje = cmd.Parameters["mensaje"].Value.ToString();

                }

            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;


            }
            return resultado;


        }



    }
}
