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
            return true;
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

    }
}
