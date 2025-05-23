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
    /// <summary>
    /// Clase que representa un usuario en la base de datos.
    /// </summary>
    public class CADUsuario
    {
        /// <summary>
        /// Cadena que almacena la cadena de conexión a la base de datos.
        /// </summary>
        private string conexion;
        /// <summary>
        /// Constructor por defecto. Inicializa la cadena de conexión a la base de datos.
        /// </summary>
        public CADUsuario() {
            conexion = ConfigurationManager.ConnectionStrings["miconex"].ToString();
        }
        /// <summary>
        /// Crea un usuario en la base de datos con la información proporcionada por el objeto entidad.
        /// </summary>
        /// <param name="usuario">Entidad que representa un usuario</param>
        /// <returns>True si se realiza la operación correctamente. False si no, o excepción</returns>
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
        /// <summary>
        /// Elimina un usuario en la base de datos con la información proporcionada por el objeto entidad.
        /// </summary>
        /// <param name="usuario">Entidad que representa un usuario</param>
        /// <returns>True si se realiza la operación correctamente. False si no, o excepción</returns>
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
        /// <summary>
        /// Lee un usuario de la base de datos con la id proporcionada por el objeto entidad.
        /// </summary>
        /// <param name="usuario">Entidad que representa un usuario</param>
        /// <returns>True si se realiza la operación correctamente. False si no, o excepción</returns>
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
        /// <summary>
        /// Actualiza un usuario en la base de datos con la id y la información proporcionada por el objeto entidad.
        /// </summary>
        /// <param name="usuario">Entidad que representa un usuario</param>
        /// <returns>True si se realiza la operación correctamente. False si no, o excepción</returns>
        public bool Update(ENUsuario usuario) {
            bool resultado = false;
            string query = "UPDATE usuario SET contrasenya = @contrasenya, correo = @correo, nombre = @nombre, saldo = @saldo, numero_tar = @numero_tar, caducidad_tar = @caducidad_tar, cvv = @cvv, direccion = @direccion, telefono = @telefono, id_rol = @id_rol, id_municipio = @id_municipio WHERE id = @id";

            SqlConnection conn = new SqlConnection(conexion);
            conn.Open();

            try { 
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@contrasenya", usuario.Password);
                cmd.Parameters.AddWithValue("@correo", usuario.Correo);
                cmd.Parameters.AddWithValue("@nombre", usuario.Nombre);
                cmd.Parameters.AddWithValue("@saldo", usuario.Saldo);
                cmd.Parameters.AddWithValue("@numero_tar", usuario.NumTar);
                cmd.Parameters.AddWithValue("@caducidad_tar", usuario.Caducidad.Date);
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
                resultado = false;
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            
            return resultado;
        }
        /// <summary>
        /// Comprueba si existe un usuario con cierto correo y contraseña.
        /// </summary>
        /// <param name="usuario">Entidad que representa un usuario</param>
        /// <returns>True si existe dicho usuario. False si no.</returns>
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
        /// <summary>
        /// Lee todos los usuarios de la base de datos.
        /// </summary>
        /// <returns>Lista con todas las entidades de todos los usuarios</returns>
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
        /// <summary>
        /// Lee todos los usuarios que tienen un rol específico.
        /// </summary>
        /// <param name="idRol">ID del rol a buscar usuarios</param>
        /// <returns>Lista con todos las entidades usuario que tienen el mismo rol.</returns>
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

        /// <summary>
        /// Verifica si el correo existe para algún usuario.
        /// </summary>
        /// <param name="correo">Correo electrónico a comprobar</param>
        /// <returns>True si existe alguien con el correo, false si no.</returns>
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
        /// <summary>
        /// Actualiza solamente la foto de perfil de un usuario pasada en la entidad.
        /// </summary>
        /// <param name="usuario">Entidad usuario que contiene la foto a actualizar.</param>
        /// <returns>True si se actualiza correctamente, false si no</returns>
        public bool UpdateFoto(ENUsuario usuario)
        {
            bool resultado = false;
            string query = "UPDATE usuario SET imagen = @imagen WHERE id = @id";

            SqlConnection conn = new SqlConnection(conexion);
            conn.Open();

            try
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@imagen", usuario.Imagen);
                cmd.Parameters.AddWithValue("@id", usuario.ID);

                int filasAfectadas = cmd.ExecuteNonQuery();
                resultado = filasAfectadas != 0;
            }
            catch (Exception ex)
            {
                resultado = false;
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return resultado;
        }
        /// <summary>
        /// Lee un usuario con un correo pasado por la entidad. Solo puede existir uno o ninguno.
        /// </summary>
        /// <param name="usu">Entidad usuario con el correo electrónico a buscar</param>
        /// <returns>True si la operación es existosa, false si no</returns>
        public bool ReadByCorreo(ENUsuario usu)
        {

            try
            {
                SqlConnection conn = new SqlConnection(conexion);
                DataSet bdvirtual = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM usuario WHERE correo = @correo", conn);
                da.SelectCommand.Parameters.AddWithValue("@correo", usu.Correo);
                da.Fill(bdvirtual, "usuario");
                DataTable dt = new DataTable();
                dt = bdvirtual.Tables["usuario"];

                foreach (DataRow fila in dt.Rows)
                {
                    usu.ID = int.Parse(fila["id"].ToString());
                    usu.Nombre = fila["nombre"].ToString();
                    usu.Password = fila["contrasenya"].ToString();
                    usu.Correo = fila["correo"].ToString();
                    usu.Imagen = fila["imagen"].ToString();
                    usu.Saldo = float.Parse(fila["saldo"].ToString());
                    usu.NumTar = fila["numero_tar"].ToString();
                    usu.Caducidad = DateTime.Parse(fila["caducidad_tar"].ToString());
                    usu.Cvv = fila["cvv"].ToString();
                    usu.Direccion = fila["direccion"].ToString();
                    usu.Telefono = fila["telefono"].ToString();
                    usu.Rol = int.Parse(fila["id_rol"].ToString());
                    usu.Municipio = int.Parse(fila["id_municipio"].ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            return true;
        }

    }

}

