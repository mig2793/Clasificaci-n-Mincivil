using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Conexion
{
    class configuracion
    {
        static string cadenaconexion = @"Data Source=SQL01\SQLAPPS;Initial Catalog=iConstruyeR;USER ID=sa; PASSWORD=mincivil123";

        public static string cadenacon
        {

            get
            {
                return cadenaconexion;
            }
        }
    }
}
