using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

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
            SqlCommand cmd = new SqlCommand("INSERT INTO usuario (imagen, contrasenya, correo, nombre, saldo, numero_tar, caducidad_tar, cvv, direccion, telefono, id_rol, id_municipio) VALUES (@imagen, @contrasenya, @correo, @nombre, @saldo, @numero_tar, @caducidad_tar, @cvv, @direccion, @telefono, @id_rol, @id_municipio)", conn);

            cmd.Parameters.AddWithValue("@imagen", usuario.Imagen);
            cmd.Parameters.AddWithValue("@contrasenya", usuario.Password);
            cmd.Parameters.AddWithValue("@correo", usuario.Correo);
            cmd.Parameters.AddWithValue("@nombre", usuario.Nombre);
            cmd.Parameters.AddWithValue("@saldo", usuario.Saldo);
            cmd.Parameters.AddWithValue("@numero_tar", usuario.NumTar);
            cmd.Parameters.AddWithValue("@caducidad_tar", usuario.Caducidad);
            cmd.Parameters.AddWithValue("@cvv", usuario.Cvv);
            cmd.Parameters.AddWithValue("@direccion", usuario.Direccion);
            cmd.Parameters.AddWithValue("@telefono", usuario.Telefono);
            cmd.Parameters.AddWithValue("@id_rol", usuario.Rol);
            cmd.Parameters.AddWithValue("@id_municipio", usuario.Municipio);

            try
            {
                conn.Open();
                int filasAfectadas = cmd.ExecuteNonQuery();
                resultado = filasAfectadas != 0;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
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
            bool resultado = false;
            string query = "UPDATE usuario SET imagen = @imagen, contrasenya = @contrasenya, correo = @correo, nombre = @nombre, saldo = @saldo, numero_tar = @numero_tar, caducidad_tar = @caducidad_tar, cvv = @cvv, direccion = @direccion, telefono = @telefono, id_rol = @id_rol, id_municipio = @id_municipio WHERE id = @id";

            SqlConnection conn = new SqlConnection(conexion);
            conn.Open();

            try { 
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@imagen", usuario.Imagen);
                cmd.Parameters.AddWithValue("@contrasenya", usuario.Password);
                cmd.Parameters.AddWithValue("@correo", usuario.Correo);
                cmd.Parameters.AddWithValue("@nombre", usuario.Nombre);
                cmd.Parameters.AddWithValue("@saldo", usuario.Saldo);
                cmd.Parameters.AddWithValue("@numero_tar", usuario.NumTar);
                cmd.Parameters.AddWithValue("@caducidad_tar", usuario.Caducidad);
                cmd.Parameters.AddWithValue("@cvv", usuario.Cvv);
                cmd.Parameters.AddWithValue("@direccion", usuario.Direccion);
                cmd.Parameters.AddWithValue("@telefono", usuario.Telefono);
                cmd.Parameters.AddWithValue("@id_rol", usuario.Rol);
                cmd.Parameters.AddWithValue("@id_municipio", usuario.Municipio);
                cmd.Parameters.AddWithValue("@id", usuario.ID);
           
                int filasAfectadas = cmd.ExecuteNonQuery();
                resultado = filasAfectadas != 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            
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

        public List<ENUsuario> ReadAll() {
            List<ENUsuario> lista = new List<ENUsuario>();
            try
            {
                SqlConnection conn = new SqlConnection(conexion);
                DataSet bdvirtual = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM usuario", conn);
                da.Fill(bdvirtual, "usuario");
                DataTable dt = new DataTable();
                dt = bdvirtual.Tables["usuario"];
                foreach (DataRow fila in dt.Rows)
                {
                    ENUsuario usuario = new ENUsuario();
                    usuario.ID = int.Parse(fila["id"].ToString());
                    usuario.Nombre = fila["nombre"].ToString();
                    usuario.Password = fila["contrasenya"].ToString();
                    usuario.Correo = fila["correo"].ToString();
                    usuario.Imagen = fila["imagen"].ToString();
                    usuario.Saldo = float.Parse(fila["saldo"].ToString());
                    usuario.NumTar = fila["numero_tar"].ToString();
                    usuario.Caducidad = DateTime.Parse(fila["caducidad_tar"].ToString());
                    usuario.Cvv = fila["cvv"].ToString();
                    usuario.Direccion = fila["direccion"].ToString();
                    usuario.Telefono = fila["telefono"].ToString();
                    usuario.Rol = int.Parse(fila["id_rol"].ToString());
                    usuario.Municipio = int.Parse(fila["id_municipio"].ToString());

                    lista.Add(usuario);
                }
            }
            catch (Exception ex) { 
                Console.WriteLine(ex.Message);
            }

            return lista;
        }

        public List<ENUsuario> ReadByRol(int idRol)
        {
            List<ENUsuario> lista = new List<ENUsuario>();

            try
            {
                SqlConnection conn = new SqlConnection(conexion);
                DataSet bdvirtual = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM usuario WHERE id_rol = @idRol", conn);
                da.SelectCommand.Parameters.AddWithValue("@idRol", idRol);
                da.Fill(bdvirtual, "usuario");
                DataTable dt = new DataTable();
                dt = bdvirtual.Tables["usuario"];

                foreach (DataRow fila in dt.Rows)
                {
                    ENUsuario usuario = new ENUsuario();
                    usuario.ID = int.Parse(fila["id"].ToString());
                    usuario.Nombre = fila["nombre"].ToString();
                    usuario.Password = fila["contrasenya"].ToString();
                    usuario.Correo = fila["correo"].ToString();
                    usuario.Imagen = fila["imagen"].ToString();
                    usuario.Saldo = float.Parse(fila["saldo"].ToString());
                    usuario.NumTar = fila["numero_tar"].ToString();
                    usuario.Caducidad = DateTime.Parse(fila["caducidad_tar"].ToString());
                    usuario.Cvv = fila["cvv"].ToString();
                    usuario.Direccion = fila["direccion"].ToString();
                    usuario.Telefono = fila["telefono"].ToString();
                    usuario.Rol = int.Parse(fila["id_rol"].ToString());
                    usuario.Municipio = int.Parse(fila["id_municipio"].ToString());

                    lista.Add(usuario);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return lista;
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

