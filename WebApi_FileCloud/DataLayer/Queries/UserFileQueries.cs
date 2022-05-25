using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi_FileCloud.DataLayer.DB;

namespace WebApi_FileCloud.DataLayer.Queries
{
    public class UserFileQueries
    {
        public static SQLCommand GetUserFiles()
        {
            string sqlString =
                "SELECT Files.id_file, Files.name, Files.source, Files.size, Users.id_user, Users.login " +
                "FROM Files, Users, Users_Files " +
                "WHERE Users_Files.id_user=Users.id_user AND Users_Files.id_file=Files.id_file";
            var parameters = new List<KeyValuePair<string, object>>
            {
            };
            var sqlCommand = new SQLCommand(parameters, sqlString);
            return sqlCommand;
        }
    }

}
