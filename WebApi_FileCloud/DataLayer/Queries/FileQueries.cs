using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi_FileCloud.DataLayer.DB;

namespace WebApi_FileCloud.DataLayer.Queries
{
    public class FileQueries
    {
        public static SQLCommand GetFiles()
        {
            string sqlString =
                "SELECT * " +
                "FROM [FILES]";
            var parameters = new List<KeyValuePair<string, object>>
            {
            };
            var sqlCommand = new SQLCommand(parameters, sqlString);
            return sqlCommand;
        }

        public static SQLCommand DeleteFilesById(int id)
        {
            string sqlString =
                "SELECT FROM [FILES] " +
                "WHERE [ID_FILE] = " + id;
            var parameters = new List<KeyValuePair<string, object>>
            {
            };
            var sqlCommand = new SQLCommand(parameters, sqlString);
            return sqlCommand;
        }

        public static SQLCommand DeleteFilesByUserId(int id)
        {
            string sqlString =
                "SELECT * " +
                "FROM [FILES]";
            var parameters = new List<KeyValuePair<string, object>>
            {
            };
            var sqlCommand = new SQLCommand(parameters, sqlString);
            return sqlCommand;
        }
    }
}
