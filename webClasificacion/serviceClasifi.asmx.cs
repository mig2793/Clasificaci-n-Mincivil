using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Conexion;
using System.Web.Script.Serialization;
using System.Data;
using System.Data.SqlClient;

namespace webClasificacion
{

    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]

    public class serviceClasifi : System.Web.Services.WebService
    {

        [WebMethod]
        public List<clasificacion> GetSelect(string condicion, int option)
        {
            List<clasificacion> listTemple = new List<clasificacion>();
            int clasificacionRow;
            DataTable selectTable = new DataTable();

            clasificacion clasi = new clasificacion();
            selectTable = clasi.SelectLike(condicion,option);

            foreach(DataRow row in selectTable.Rows)
            {

              clasificacionRow = Convert.ToInt32(row["Clasif"]);

                listTemple.Add(new clasificacion() { CodItem = row["codigoItem"].ToString(), DescripcionItem = row["descripcionItem"].ToString(), DescriptionPptal = row["descPartidaPptal"].ToString(), _Clasifi = clasificacionRow });

            }

            return listTemple;
        }

        [WebMethod]
        public List<clasificacion> GetSelectOptional(string condicion,string inputOptional)
        {
            List<clasificacion> listTemple = new List<clasificacion>();
            int clasificacionRow;
            DataTable selectTable = new DataTable();

            clasificacion clasi = new clasificacion();
            selectTable = clasi.SelectOptionalInput(condicion, inputOptional);

            foreach (DataRow row in selectTable.Rows)
            {
                if (row["Clasif"] == null)
                {
                    clasificacionRow = 50;
                }
                else
                {
                    clasificacionRow = Convert.ToInt32(row["Clasif"]);

                }

                listTemple.Add(new clasificacion() { CodItem = row["codigoItem"].ToString(), DescripcionItem = row["descripcionItem"].ToString(), DescriptionPptal = row["descPartidaPptal"].ToString(), _Clasifi = clasificacionRow });

            }

            return listTemple;
        }
        [WebMethod]
        public List<Categorias> GetCategorias()
        {
            List<Categorias> categorys = new List<Categorias>();
            DataTable selectTable = new DataTable();

            Categorias clasi = new Categorias();
            selectTable = clasi.SelectCategorias();

            foreach (DataRow row in selectTable.Rows)
            {
                categorys.Add(new Categorias() { _categorias = row["NombreGr"].ToString() });

            }

            return categorys;
        }
        [WebMethod]
        public int UpdateClasification(int _clasifi, string _codItem)
        {
            int responseClasification;

            clasificacion clasi = new clasificacion();
            responseClasification = clasi.ActualizarClasificacion(_clasifi, _codItem);

            return responseClasification;
        }
    }
}
