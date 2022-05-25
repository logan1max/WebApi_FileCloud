using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi_FileCloud.DataLayer.DB;

namespace WebApi_FileCloud.DataLayer.Queries
{
    public class UserQueries
    {
        public static SQLCommand GetUsers()
        {
            string sqlString =
                "SELECT * " +
                "FROM [USERS]";
            var parameters = new List<KeyValuePair<string, object>>
            {
            };
            var sqlCommand = new SQLCommand(parameters, sqlString);
            return sqlCommand;
        }
    }
}
