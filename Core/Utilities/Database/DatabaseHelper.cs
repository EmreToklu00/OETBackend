using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Database
{
    public class DatabaseHelper
    {
        public static string GetMySQLDatabaseConnectionString()
        {
            return "server=localhost;user=root;password=123456;database=oetdbtemplate";
        }
    }
}
