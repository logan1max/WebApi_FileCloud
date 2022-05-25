using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace WebApi_FileCloud.DataLayer.DB
{
    class MSSQL
    {
        public async Task<List<T>> Request<T>(SQLCommand sqlCommand, string connectionString)
            where T : class, new()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //новое преобразование через расширение
                SqlCommand command = sqlCommand.ToSqlCommand(connection);

                await connection.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    DataTable dt = new DataTable();

                    dt.Load(reader);

                    List<T> list = new List<T>();

                    foreach (var row in dt.AsEnumerable())
                    {
                        T obj = new T();

                        foreach (var prop in obj.GetType().GetProperties())
                        {
                            try
                            {

                                PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);

                                bool columnExists = false;

                                for (int i = 0; i < dt.Columns.Count && !columnExists; i++)
                                {
                                    if (dt.Columns[i].ColumnName.ToLower() == prop.Name.ToLower())
                                    {
                                        columnExists = true;
                                    }
                                }

                                if (columnExists)
                                {
                                    if (propertyInfo.PropertyType.BaseType == typeof(System.Enum))
                                    {
                                        propertyInfo.SetValue(obj, Enum.Parse(propertyInfo.PropertyType, row[prop.Name].ToString()));
                                    }
                                    else if (propertyInfo.PropertyType == typeof(DateTime?))
                                    {
                                        if (row[prop.Name] == DBNull.Value)
                                        {
                                            propertyInfo.SetValue(obj, null);
                                        }
                                        else
                                        {
                                            propertyInfo.SetValue(obj, (DateTime?)(row[prop.Name] ?? null));
                                        }
                                    }
                                    else if (propertyInfo.PropertyType == typeof(int?))
                                    {
                                        if (row[prop.Name] == DBNull.Value)
                                        {
                                            propertyInfo.SetValue(obj, null);
                                        }
                                        else
                                        {
                                            propertyInfo.SetValue(obj, (int?)(row[prop.Name] ?? null));
                                        }

                                    }
                                    else if (propertyInfo.PropertyType == typeof(byte?))
                                    {
                                        if (row[prop.Name] == DBNull.Value)
                                        {
                                            propertyInfo.SetValue(obj, null);
                                        }
                                        else
                                        {
                                            propertyInfo.SetValue(obj, (byte?)(row[prop.Name] ?? null));
                                        }

                                    }
                                    else
                                    {
                                        propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                continue;
                            }
                        }
                        list.Add(obj);
                    }
                    return list;
                }
            }
        }

        public async Task<List<string>> Request(SQLCommand sqlCommand, string connectionString)
        {
            var result = new List<string>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = sqlCommand.ToSqlCommand(connection);

                await connection.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        result.Add(reader[0].ToString());
                    }
                }

                return result;
            }
        }

        public DataTable GetDataTable(SQLCommand sqlCommand, string SQLConnectionString)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(SQLConnectionString))
            {
                SqlCommand command = sqlCommand.ToSqlCommand(connection);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    dt.Load(reader);
                }

                command.Parameters.Clear();
            }

            return dt;
        }
    }

    public static class Account
    {
        public static string ConnectionString { get; set; }
    }
}
