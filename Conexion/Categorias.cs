using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Conexion
{
    public class Categorias
    {
        #region Atributos
        private string categorias;
        #endregion

        #region Metodos Set Get
        public string _categorias
        {
            get { return categorias; }
            set { categorias = value; }
        }
        #endregion

        #region Metodos
        public DataTable SelectCategorias()
        {
            SqlCommand comando = ConexionBD.crearcomando();
            comando.CommandText = "select NombreGr from Grupos";
            return ConexionBD.EjecutarSelect(comando);
        }
        #endregion
    }
}
