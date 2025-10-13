using System.Configuration;
using System.Data.SqlClient;

namespace RealEstate.Utils
{
    public static class DbHelper
    {
        private static readonly string ConnString = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnString);
        }
    }
}