using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace DataAccess
{
    class DatabaseConnection
    {
        private static SqlConnection _connection =null;
        
        private DatabaseConnection()
        {
           
        }
        
        public static SqlConnection GetConnectionObject()
        {
            if (_connection == null)
            {
                _connection = new SqlConnection("data source = localhost;initial catalog = BankSystem; persist security info = True;Integrated Security = SSPI; ");
            }

            return _connection;
        }
    }
}
