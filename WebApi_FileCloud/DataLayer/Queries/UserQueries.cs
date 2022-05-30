using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi_FileCloud.DataLayer.DB;
using WebApi_FileCloud.Models;

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

        public static SQLCommand GetUsersById(int id)
        {
            string sqlString =
                "SELECT * " +
                "FROM [USERS] " +
                "WHERE [ID_USER] = " + id;
            var parameters = new List<KeyValuePair<string, object>>
            {
            };
            var sqlCommand = new SQLCommand(parameters, sqlString);
            return sqlCommand;
        }

        public static SQLCommand InsertUser(User u)
        {
            string sqlString =
                @$"INSERT INTO[dbo].[USERS]
                ([ID_USER],
                [LOGIN],
                [PASSWORD])
                VALUES
                (@ID_USER,
                @LOGIN,
                @PASSWORD);";
            var parameters = new List<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("@ID_USER",u.id_user),
                new KeyValuePair<string, object>("@LOGIN", u.login),
                new KeyValuePair<string, object>("@PASSWORD", u.password),
            };
            var sqlCommand = new SQLCommand(parameters, sqlString);
            return sqlCommand;
        }

        public static SQLCommand AuthUser(Person p)
        {
            string sqlString =
                @$"SELECT * 
                FROM [USERS] 
                WHERE [LOGIN] = @LOGIN
                AND [PASSWORD] = @PASSWORD";                
            var parameters = new List<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("@LOGIN", p.login),
                new KeyValuePair<string, object>("@PASSWORD", p.password),
            };
            var sqlCommand = new SQLCommand(parameters, sqlString);
            return sqlCommand;
        }
    }
}
