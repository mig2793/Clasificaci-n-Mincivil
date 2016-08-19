using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Conexion
{
    public class clasificacion
    {
        #region Atributos
        private string codItem;
        private string descripcionItem;
        private string descripcion_pptal;
        private int Clasifi;
        #endregion

        #region Metodos Set Get

        public string CodItem
        {
            get { return codItem; }
            set { codItem = value; }
        }

        public string DescripcionItem
        {
            get { return descripcionItem; }
            set { descripcionItem = value; }
        }

        public string DescriptionPptal
        {
            get { return descripcion_pptal; }
            set { descripcion_pptal = value; }
        }
        public int _Clasifi
        {
            get { return Clasifi; }
            set { Clasifi = value; }
        }
        #endregion

        #region Metodos Crud

        public int ActualizarClasificacion(int _clasifi, string _codItem)
        {
            SqlCommand comando = ConexionBD.crearcomando();
            comando.CommandText = "UPDATE MaestroXGrupos SET Clasif = '" + _clasifi + "' WHERE codigoItem = '" + _codItem + "'";
            return ConexionBD.EjecutarComando(comando);
        }

        public  DataTable SelectLike(string condicion, int searchOption)
        {
            SqlCommand comando = ConexionBD.crearcomando();
            if (searchOption == 1)
            {
                comando.CommandText = "select se.codigoItem, se.descripcionItem, se.descPartidaPptal, mg.Clasif FROM SeguimientoPMV2 se,MaestroXGrupos mg WHERE se.codigoItem = mg.CodigoItem AND (se.codigoItem LIKE '%" + condicion + "%' OR se.descripcionItem LIKE '%" + condicion + "%' OR se.descPartidaPptal LIKE '%" + condicion + "%')";
            }
            else if (searchOption == 2)
            {
                comando.CommandText = "select se.codigoItem, se.descripcionItem, se.descPartidaPptal, mg.Clasif FROM SeguimientoPMV2 se,MaestroXGrupos mg WHERE se.codigoItem = mg.CodigoItem AND (se.codigoItem LIKE '" + condicion + "%' OR se.descripcionItem LIKE '" + condicion + "%')";
            }
            else if (searchOption == 3)
            {
                comando.CommandText = "select se.codigoItem, se.descripcionItem,se. descPartidaPptal, mg.Clasif FROM SeguimientoPMV2 se,MaestroXGrupos mg WHERE se.codigoItem = mg.CodigoItem AND (se.codigoItem LIKE '%" + condicion + "' OR se.descripcionItem LIKE '%" + condicion + "')";
            }
            else if (searchOption == 4)
            {
                comando.CommandText = "select se.codigoItem, se.descripcionItem, se.descPartidaPptal, mg.Clasif FROM SeguimientoPMV2 se,MaestroXGrupos mg WHERE se.codigoItem = mg.CodigoItem AND (mg.Clasif = '" + condicion + "')";
            }
            else if (searchOption == 5)
            {
                comando.CommandText = "select se.codigoItem, se.descripcionItem, se.descPartidaPptal, mg.Clasif FROM SeguimientoPMV2 se,MaestroXGrupos mg WHERE se.codigoItem = mg.CodigoItem AND(mg.CodigoItem = '')";
            }
            else if (searchOption == 6)
            {
                comando.CommandText = "select se.codigoItem, se.descripcionItem, se.descPartidaPptal, mg.Clasif FROM DetalleProductoV2 se,MaestroXGrupos mg WHERE se.codigoItem = mg.CodigoItem AND (se.codigoItem LIKE '%" + condicion + "%' OR se.descripcionItem LIKE '%" + condicion + "%' OR se.descPartidaPptal LIKE '%" + condicion + "%')";
            }
            return ConexionBD.EjecutarSelect(comando);
        }
        public DataTable SelectOptionalInput(string condicion,string inputOptional)
        {
            SqlCommand comando = ConexionBD.crearcomando();

            comando.CommandText = "select se.codigoItem, se.descripcionItem, se.descPartidaPptal, mg.Clasif FROM SeguimientoPMV2 se,MaestroXGrupos mg WHERE se.codigoItem = mg.CodigoItem AND ((se.codigoItem LIKE '%" + condicion + "%' OR se.descripcionItem LIKE '%" + condicion + "%' OR se.descPartidaPptal LIKE '%" + condicion + "%')AND(mg.Clasif = '" + inputOptional + "'))";
    
            return ConexionBD.EjecutarSelect(comando);
        }
        #endregion

        public clasificacion() { }
    }
}
