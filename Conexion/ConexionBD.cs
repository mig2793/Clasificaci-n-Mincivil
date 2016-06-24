using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Configuration;

namespace Conexion
{
    public class ConexionBD
    {
        #region Propiedades
        public SqlConnection _sqlconeccion = new SqlConnection();
        private SqlCommand _sqlcomand;
        private string _strConeccion;

        public string strConecciones
        {
            set { _strConeccion = value; }
            get { return _strConeccion; }
        }
        public SqlConnection sqlconecciones
        {
            set { _sqlconeccion = value; }
            get { return _sqlconeccion; }
        }
        #endregion

        #region Constructores
        public ConexionBD()
        {

        }
        #endregion

        #region Metodos

        /* public void Desconectar()
         {
             bool desconectar = false;

             try
             {
                 _sqlconeccion.Close();
                 _sqlconeccion.Dispose();
                 desconectar = true;
             }

             catch(Exception error)
             {
                 desconectar = false;

             }

         }

         public bool conectorWebConfig()
         {
             bool conectar = false;
             try
             {
                 _strConeccion = System.Configuration.ConfigurationManager.ConnectionStrings["Micon"].ConnectionString;
                 _sqlconeccion.ConnectionString = _strConeccion;
                 _sqlconeccion.Open();
                 conectar = true;
             }
             catch(Exception error)
             {

                 conectar = false;
             }

             return conectar;
         }*/

        public static SqlCommand crearcomando()
        {
            string cadenaconexion = configuracion.cadenacon;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = cadenaconexion;
            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.Connection = conexion;
            return comando;
        }

        public static SqlCommand crearComandoProc(string proc)
        {
            string cadenaconexion = configuracion.cadenacon;
            SqlConnection conexion = new SqlConnection(cadenaconexion);
            SqlCommand comando = new SqlCommand(proc, conexion);
            comando.CommandType = CommandType.StoredProcedure;
            return comando;
        }
        public static int EjecutarComando(SqlCommand comando)
        {
            try
            {
                comando.Connection.Open();
                return comando.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                comando.Connection.Dispose();
                comando.Connection.Close();
            }
        }

        public static DataTable EjecutarSelect(SqlCommand comando)
        {
            DataTable tabla = new DataTable();
            try
            {
                comando.Connection.Open();
                SqlDataAdapter adaptador = new SqlDataAdapter();
                adaptador.SelectCommand = comando;
                adaptador.Fill(tabla);

            }
            catch
            {
                throw;
            }
            finally
            {
                comando.Connection.Close();

            }
            return tabla;
        }

        #endregion
    }
}
