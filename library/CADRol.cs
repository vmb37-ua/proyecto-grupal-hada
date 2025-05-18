using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Security.Policy;

namespace library
{
    public class CADRol
    {
        private string constring { get; set; }
        public CADRol() {
            constring = ConfigurationManager.ConnectionStrings["miconex"].ToString();
        }
        public bool Create(ENRol rol)
        {
            bool result = true;
            SqlConnection c = new SqlConnection(constring);
            try
            {
                c.Open();
                SqlCommand com = new SqlCommand("INSERT INTO rol (nombre, descripcion) VALUES (@Nombre, @Descripcion)", c);
                com.Parameters.AddWithValue("@Nombre", rol.Nombre);
                com.Parameters.AddWithValue("@Descripcion", rol.Descripcion);
                com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Rol operation has failed. Error: {0}", ex.Message);
                result = false;
            }
            finally
            {
                c.Close();
            }
            return result;
        }
        public bool Update(ENRol rol)
        {
            bool result = true;
            SqlConnection c = new SqlConnection(constring);
            try
            {
                c.Open();
                SqlCommand com = new SqlCommand("UPDATE rol SET nombre=@Nombre, descripcion=@Descripcion WHERE id_rol=@IdRol", c);
                com.Parameters.AddWithValue("@IdRol", rol.Id_rol);
                com.Parameters.AddWithValue("@Nombre", rol.Nombre);
                com.Parameters.AddWithValue("@Descripcion", rol.Descripcion);
                com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Rol operation has failed.Error: {0}", ex.Message);
                result = false;
            }
            finally
            {
                c.Close();
            }
            return result;
        }
        public bool Delete(ENRol rol)
        {
            bool result = true;
            SqlConnection c = new SqlConnection(constring);
            try
            {
                c.Open();
                SqlCommand com = new SqlCommand("DELETE FROM rol where id_rol=@IdRol", c);
                com.Parameters.AddWithValue("@IdRol", rol.Id_rol);
                com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Rol operation has failed. Error: {0}", ex.Message);
                result = false;
            }
            finally
            {
                c.Close();
            }
            return result;
        }
        public bool Read(ENRol rol)
        {
            bool result = true;
            SqlConnection c = new SqlConnection(constring);
            try
            {
                c.Open();
                SqlCommand com = new SqlCommand("SELECT * FROM rol WHERE id_rol=@IdRol", c);
                com.Parameters.AddWithValue("@IdRol", rol.Id_rol);
                SqlDataReader dr = com.ExecuteReader();
                dr.Read();
                rol.Nombre = dr["nombre"].ToString();
                rol.Descripcion = dr["descripcion"].ToString();
                dr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Rol operation has failed.Error: {0}", ex.Message);
                result = false;
            }
            finally
            {
                c.Close();
            }
            return result;
        }
        public bool ReadFirst(ENRol rol)
        {
            bool result = true;
            SqlConnection c = new SqlConnection(constring);
            try
            {
                c.Open();
                SqlCommand com = new SqlCommand("SELECT TOP 1 * FROM rol", c);
                SqlDataReader dr=com.ExecuteReader();
                dr.Read();
                rol.Id_rol = int.Parse(dr["id_rol"].ToString());
                rol.Nombre = dr["nombre"].ToString();
                rol.Descripcion = dr["descripcion"].ToString();
                dr.Close();
            }
            catch(Exception ex){
                Console.WriteLine("Rol operation has failed.Error: {0}", ex.Message);
                result = false;
            }
            finally
            {
                c.Close();
            }
            return result;
        }
        public bool ReadNext(ENRol rol)
        {
            bool result = true;
            SqlConnection c = new SqlConnection(constring);
            try
            {
                c.Open();
                SqlCommand com = new SqlCommand("SELECT TOP 1 * FROM rol WHERE id_rol > @IdRol ORDER BY id_rol ASC", c);
                com.Parameters.AddWithValue("@IdRol", rol.Id_rol);
                SqlDataReader dr = com.ExecuteReader();
                dr.Read();
                rol.Id_rol = int.Parse(dr["id_rol"].ToString());
                rol.Nombre = dr["nombre"].ToString();
                rol.Descripcion = dr["descripcion"].ToString();
                dr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Rol operation has failed.Error: {0}", ex.Message);
                result = false;
            }
            finally
            {
                c.Close();
            }
            return result;
        }
        public bool ReadPrev(ENRol rol)
        {
            bool result = true;
            SqlConnection c = new SqlConnection(constring);
            try
            {
                c.Open();
                SqlCommand com = new SqlCommand("SELECT TOP 1 * FROM rol WHERE id_rol < @IdRol ORDER BY id_rol DESC", c);
                com.Parameters.AddWithValue("@IdRol", rol.Id_rol);
                SqlDataReader dr = com.ExecuteReader();
                dr.Read();
                rol.Id_rol = int.Parse(dr["id_rol"].ToString());
                rol.Nombre = dr["nombre"].ToString();
                rol.Descripcion = dr["descripcion"].ToString();
                dr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Rol operation has failed.Error: {0}", ex.Message);
                result = false;
            }
            finally
            {
                c.Close();
            }
            return result;
        }
        public List<ENRol> ReadAll()
        {
            SqlConnection c = new SqlConnection(constring);
            List<ENRol> list = new List<ENRol>();
            ENRol rol;
            try
            {
                c.Open();
                SqlCommand com = new SqlCommand("SELECT * FROM rol", c);
                SqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    rol = new ENRol();
                    rol.Id_rol = int.Parse(dr["id_rol"].ToString());
                    rol.Nombre = dr["nombre"].ToString();
                    rol.Descripcion = dr["descripcion"].ToString();
                    list.Add(rol);
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Product operation has failed.Error: {0}", ex.Message);
            }
            finally
            {
                c.Close();
            }
            return list;
        }
    }
}