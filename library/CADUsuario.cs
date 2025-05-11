using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace library
{
    public class CADUsuario
    {
        private string conexion;
        public CADUsuario() {
            conexion = ConfigurationManager.ConnectionStrings["miconex"].ToString();
        }
        public bool Create(ENUsuario usuario)
        {
            bool resultado = false;
            SqlConnection conn = new SqlConnection(conexion);

            try
            {
                conn.Open();

                // Creamos la consulta SQL con parámetros
                string query = @"INSERT INTO usuario
                        (nombre, telefono, correo, direccion, contrasenya, saldo, numero_tar, caducidad_tar, cvv, id_rol, id_municipio, imagen)
                        OUTPUT INSERTED.id -- Esto devuelve el ID generado automáticamente por la base de datos
                        VALUES
                        (@nombre, @telefono, @correo, @direccion, @contrasenya, @saldo, @numero_tar, @caducidad_tar, @cvv, @id_rol, @id_municipio, @imagen)";

                SqlCommand cmd = new SqlCommand(query, conn);

                // Asignamos valores a los parámetros de la consulta
                cmd.Parameters.AddWithValue("@nombre", usuario.Nombre);
                cmd.Parameters.AddWithValue("@telefono", usuario.Telefono);
                cmd.Parameters.AddWithValue("@correo", usuario.Correo);
                cmd.Parameters.AddWithValue("@direccion", usuario.Direccion);
                cmd.Parameters.AddWithValue("@contrasenya", usuario.Password);
                cmd.Parameters.AddWithValue("@saldo", usuario.Saldo);
                cmd.Parameters.AddWithValue("@numero_tar", usuario.NumTar);
                cmd.Parameters.AddWithValue("@caducidad_tar", usuario.Caducidad);
                cmd.Parameters.AddWithValue("@cvv", usuario.Cvv);
                cmd.Parameters.AddWithValue("@id_rol", usuario.Rol);
                cmd.Parameters.AddWithValue("@id_municipio", usuario.Municipio);
                cmd.Parameters.AddWithValue("@imagen", usuario.Imagen);

                // Ejecutamos la consulta y recuperamos el ID insertado
                int insertedID = (int)cmd.ExecuteScalar();
                usuario.ID = insertedID;  // Asignamos el ID al objeto usuario

                resultado = true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el usuario: " + ex.Message);
            }

            finally
            {
                conn.Close();
            }

            return resultado;
        }

        public bool Delete(ENUsuario usuario) {
            SqlConnection conn = new SqlConnection(conexion);
            bool resultado = false;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM usuario WHERE id = @id", conn);
                cmd.Parameters.AddWithValue("@id", usuario.ID);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                resultado = false;
            }
            finally
            {
                conn.Close();
            }
            return resultado;
        }
        public bool Read(ENUsuario usuario) {
            SqlConnection conn = new SqlConnection(conexion);
            bool resultado = false;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM usuario WHERE id = @id", conn);
                cmd.Parameters.AddWithValue("@id", usuario.ID);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    usuario.Imagen = reader["imagen"].ToString();
                    usuario.Password = reader["contrasenya"].ToString();
                    usuario.Correo = reader["correo"].ToString();
                    usuario.Nombre = reader["nombre"].ToString();
                    usuario.Saldo = float.Parse(reader["saldo"].ToString());
                    usuario.NumTar = reader["numero_tar"].ToString();
                    usuario.Caducidad = DateTime.Parse(reader["caducidad_tar"].ToString());
                    usuario.Cvv = reader["cvv"].ToString();
                    usuario.Direccion = reader["direccion"].ToString();
                    usuario.Telefono = reader["telefono"].ToString();
                    usuario.Rol = int.Parse(reader["id_rol"].ToString());
                    usuario.Municipio = int.Parse(reader["id_municipio"].ToString());
                    resultado = true;
                }
                else
                {
                    resultado = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                resultado = false;
            }
            finally
            {
                conn.Close();
            }
            return resultado;
        }
        public bool Update(ENUsuario usuario) {
            return true;
        }

        public bool LoginUsu(ENUsuario usuario) {
            SqlConnection conn = new SqlConnection(conexion);
            bool resultado = false;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM usuario WHERE correo = @correo and contrasenya = @passw", conn);
                cmd.Parameters.AddWithValue("@correo", usuario.Correo);
                cmd.Parameters.AddWithValue("@passw", usuario.Password);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    usuario.ID = int.Parse(reader["id"].ToString());
                    resultado = true;
                }
                else {
                    resultado = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                resultado = false;
            }
            finally { 
                conn.Close();
            }
            return resultado;
        }

        public bool ExisteCorreo(string correo)
        {
            bool existe = false;
            SqlConnection cn = new SqlConnection(conexion);

            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM usuario WHERE correo = @correo", cn);
                cmd.Parameters.AddWithValue("@correo", correo);
                int count = (int)cmd.ExecuteScalar();
                existe = count > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                existe = false;
            }
            finally
            {
                cn.Close();
            }

            return existe;
        }
    }

}

