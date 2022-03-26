using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CRUD2.Models
{
    public class Mantenimiento
    {
        private SqlConnection conexion;
        private void Conectar()
        {
            string constr = ConfigurationManager.ConnectionStrings["admin"].ToString();
            conexion = new SqlConnection(constr);
        }
        public int Alta(Articulo art)
        {
            Conectar();
            SqlCommand comando = new SqlCommand("insert into articulos (codigo,descripcion,precio) values (@codigo,@descripcion,@precio)", conexion);
            comando.Parameters.Add("@codigo", SqlDbType.Int);
            comando.Parameters.Add("@descripcion", SqlDbType.VarChar);
            comando.Parameters.Add("@precio", SqlDbType.Float);
            comando.Parameters["@codigo"].Value = art.Codigo;
            comando.Parameters["@descripcion"].Value = art.Descripcion;
            comando.Parameters["@precio"].Value = art.Precio;
            conexion.Open();
            int i = comando.ExecuteNonQuery();
            conexion.Close();
            return i;
        }
        public List<Articulo> RecuperarTodos()
        {
            Conectar();
            List<Articulo> articulos = new List<Articulo>();
            SqlCommand comando = new SqlCommand("select codigo,descripcion,precio from articulos", conexion);
            conexion.Open();
            SqlDataReader registro = comando.ExecuteReader();
            while (registro.Read())
            {
                Articulo art = new Articulo
                {
                    Codigo = int.Parse(registro["codigo"].ToString()),
                    Descripcion = registro["descripcion"].ToString(),
                    Precio = float.Parse(registro["precio"].ToString())
                };
                articulos.Add(art);
            }
            conexion.Close();
            return articulos;
        }
        public Articulo Recuperar(int codigo)
        {
            Conectar();
            SqlCommand comando = new SqlCommand("select codigo,descripcion,precio from articulos where codigo=@codigo", conexion);
            comando.Parameters.Add("@codigo", SqlDbType.Int);
            comando.Parameters["@codigo"].Value = codigo;
            conexion.Open();
            SqlDataReader registro = comando.ExecuteReader();
            Articulo art = new Articulo();
            if (registro.Read())
            {
                art.Codigo = int.Parse(registro["codigo"].ToString());
                art.Descripcion = registro["descripcion"].ToString();
                art.Precio = float.Parse(registro["precio"].ToString());
            }
            conexion.Close();
            return art;
        }
        public int Modificar (Articulo art)
        {
            Conectar();
            SqlCommand comando = new SqlCommand("update articulos set descripcion=@descripcion,precio=@precio where codigo=@codigo", conexion);
            comando.Parameters.Add("@descripcion", SqlDbType.VarChar);
            comando.Parameters["@descripcion"].Value = art.Descripcion;
            comando.Parameters.Add("@precio", SqlDbType.Float);
            comando.Parameters["@precio"].Value = art.Precio;
            comando.Parameters.Add("@codigo", SqlDbType.Int);
            comando.Parameters["@codigo"].Value = art.Precio;
            conexion.Open();
            int i = comando.ExecuteNonQuery();
            conexion.Close();
            return i;
        }
        public int Borrar (int codigo)
        {
            Conectar();
            SqlCommand comando = new SqlCommand("delete from articulos where codigo=@codigo", conexion);
            comando.Parameters.Add("@codigo", SqlDbType.Int);
            comando.Parameters["@codigo"].Value = codigo;
            conexion.Open();
            int i = comando.ExecuteNonQuery();
            conexion.Close();
            return i;
        }
    }
}