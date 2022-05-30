using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi_FileCloud.DataLayer.DB;
using WebApi_FileCloud.DataLayer.Queries;
using WebApi_FileCloud.Models;

namespace WebApi_FileCloud.DataLayer.Services
{
    public class FileService
    {
        public async Task<List<Files>> GetFiles()
        {
            var files = await new DB.MSSQL().Request<Files>(FileQueries.GetFiles(), Account.ConnectionString);

            return files;
        }

        public async Task<List<Files>> GetFilesByUser(int id)
        {
            var files = await new DB.MSSQL().Request<Files>(FileQueries.GetFilesByUser(id), Account.ConnectionString);

            return files;
        }

        public async Task InsertFile(Files f)
        {
            var temp = await new DB.MSSQL().Request<Files>(FileQueries.GetFilesBySource(f.source), Account.ConnectionString);

            if (temp.FirstOrDefault() == null)
            {
                await new DB.MSSQL().Request(FileQueries.InsertFile(f), Account.ConnectionString);
            }
        }
    }
}
