using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace WebApi_FileCloud.DataLayer.DB
{
    public class SQLCommand
    {
        public SQLCommand(List<KeyValuePair<string, object>> parameters, string request)
        {
            if (parameters != null)
            {
                foreach (KeyValuePair<string, object> parameter in parameters)
                {
                    if (parameter.Value == null)
                    {
                        continue;
                    }

                    if (parameter.Value.GetType() == "".GetType())
                    {
                        parameter.Value.ToString().Replace("'", String.Empty);
                    }

                    parameter.Value.ToString().Replace("\"", String.Empty);
                }
            }

            Parameters = parameters;
            Request = request;
        }

        public SQLCommand(string request, List<SqlParameter> parameters)
        {
            Request = request;
            SqlParameters = parameters;
        }

        public List<KeyValuePair<string, object>> Parameters { get; }

        public List<SqlParameter> SqlParameters { get; }

        public string Request { get; }
    }

    public static class SqlCommandExtensions
    {
        internal static SqlCommand ToSqlCommand(this SQLCommand sqlCommand, SqlConnection connection)
        {
            SqlCommand command = new SqlCommand(sqlCommand.Request, connection);

            if (sqlCommand.Parameters != null)
            {
                foreach (KeyValuePair<string, object> parameter in sqlCommand.Parameters)
                {
                    var sqlType = GetSqlDbType(parameter);

                    command.Parameters.Add(parameter.Key, sqlType);
                    command.Parameters[parameter.Key].Value = parameter.Value ?? DBNull.Value;
                }
            }

            if (sqlCommand.SqlParameters != null)
            {
                foreach (SqlParameter parameter in sqlCommand.SqlParameters)
                {
                    command.Parameters.Add(parameter);
                }
            }

            return command;
        }

        private static SqlDbType GetSqlDbType(KeyValuePair<string, object> pair)
        {
            if (pair.Value == null)
            {
                return SqlDbType.VarChar;
            }

            if (pair.Value.GetType() == typeof(DateTime))
            {
                return SqlDbType.DateTime;
            }

            if (pair.Value.GetType() == typeof(byte[]))
            {
                return SqlDbType.VarBinary;
            }

            if (pair.Value.GetType() == typeof(int))
            {
                return SqlDbType.Int;
            }

            if (pair.Value.GetType() == typeof(bool))
            {
                return SqlDbType.Bit;
            }

            return SqlDbType.VarChar;
        }
    }
}
