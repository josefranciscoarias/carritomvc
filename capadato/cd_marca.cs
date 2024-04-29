﻿using capaentidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capadato
{
   public class cd_marca
    {

        public List<marca> Listar()
        {
            List<marca> lista = new List<marca>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cn))
                {
                    string query = "select idmarca,descripcion,activo from marca";
                    SqlCommand cm = new SqlCommand(query, oconexion);
                    cm.CommandType = CommandType.Text;
                    oconexion.Open();
                    using (SqlDataReader dr = cm.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(
                                new marca()
                                {
                                    idmarca = Convert.ToInt32(dr["idmarca"]),
                                    descripcion = dr["descripcion"].ToString(),
                                    activo = Convert.ToBoolean(dr["activo"])
                                });
                        }


                    }
                }




            }
            catch
            {

                lista = new List<marca>();
            }


            return lista;
        }
        public int registrar(marca obj, out string mensaje)
        {
            int idautogenerado = 0;
            mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_registrarmarca", oconexion);
                    cmd.Parameters.AddWithValue("descripcion", obj.descripcion);

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


        public bool editar(marca obj, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_editarmarca", oconexion);
                    cmd.Parameters.AddWithValue("idmarca", obj.idmarca);
                    cmd.Parameters.AddWithValue("descripcion", obj.descripcion);
                    ;
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

        public bool eliminar(int id, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_eliminarmarca", oconexion);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    resultado = cmd.ExecuteNonQuery() > 0 ? true : false;

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
