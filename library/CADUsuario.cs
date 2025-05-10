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
            conexion = ConfigurationManager.AppSettings.ToString();
        }
        public bool Create(ENUsuario usuario)
        {
            return true;
        }
        public bool Delete(ENUsuario usuario) { 
            return true;
        }
        public bool Read(ENUsuario usuario) {
            return true;
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
