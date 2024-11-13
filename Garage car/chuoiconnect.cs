using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Garage_car
{
    public class chuoiconnect
    {
        public static string chuoi()   
        {
            return @"Data Source=HOANGTRAN;Initial Catalog=QLgaracar;User ID=admin;Password=123;TrustServerCertificate=False";
        }
    }
}
