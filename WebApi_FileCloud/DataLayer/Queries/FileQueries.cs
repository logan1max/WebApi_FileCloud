using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi_FileCloud.DataLayer.DB;
using WebApi_FileCloud.Models;

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

        public static SQLCommand GetFilesBySource(string source)
        {
            string sqlString =
                @$"SELECT * 
                FROM [FILES] 
                WHERE [SOURCE] = @SOURCE";
            var parameters = new List<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("@SOURCE", source)
            };
            var sqlCommand = new SQLCommand(parameters, sqlString);
            return sqlCommand;
        }


        public static SQLCommand GetFilesByUser(int id)
        {
            string sqlString =
                "SELECT * " +
                "FROM [FILES] " +
                "WHERE [OWNER] = " + id;
            var parameters = new List<KeyValuePair<string, object>>
            {
            };
            var sqlCommand = new SQLCommand(parameters, sqlString);
            return sqlCommand;
        }

        public static SQLCommand InsertFile(Files f)
        {
            string sqlString =
                @$"INSERT INTO[dbo].[FILES]
                ([NAME],
                [SOURCE],
                [OWNER],
                [SIZE])
                VALUES
                (@NAME,
                @SOURCE,
                @OWNER,
                @SIZE);";
            var parameters = new List<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("@NAME",f.name),
                new KeyValuePair<string, object>("@SOURCE", f.source),
                new KeyValuePair<string, object>("@OWNER", f.owner),
                new KeyValuePair<string, object>("@SIZE", f.size),
            };
            var sqlCommand = new SQLCommand(parameters, sqlString);
            return sqlCommand;
        }

        //public static SQLCommand DeleteFilesById(int id)
        //{
        //    string sqlString =
        //        "SELECT FROM [FILES] " +
        //        "WHERE [ID_FILE] = " + id;
        //    var parameters = new List<KeyValuePair<string, object>>
        //    {
        //    };
        //    var sqlCommand = new SQLCommand(parameters, sqlString);
        //    return sqlCommand;
        //}

        //public static SQLCommand DeleteFilesByUserId(int id)
        //{
        //    string sqlString =
        //        "SELECT * " +
        //        "FROM [FILES]";
        //    var parameters = new List<KeyValuePair<string, object>>
        //    {
        //    };
        //    var sqlCommand = new SQLCommand(parameters, sqlString);
        //    return sqlCommand;
        //}
    }
}
