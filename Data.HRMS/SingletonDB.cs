using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.HRMS
{
    public sealed class  SingletonDB
    {
        private static  SingletonDB instance =null;
        private readonly SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSdb"].ConnectionString);

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static SingletonDB()
        {
        }

        private SingletonDB()
        {
        }

        public static SingletonDB Instance
        {
            get
            {
                if (instance==null)
                {
                     instance = new SingletonDB();
                }
                return instance;
            }
        }

        public SqlConnection GetDBConnection()
        {
            return con;
        }
    }
}
