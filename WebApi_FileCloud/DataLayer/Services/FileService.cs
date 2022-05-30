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
        public async Task<List<File>> GetFiles()
        {
            var files = await new DB.MSSQL().Request<File>(FileQueries.GetFiles(), Account.ConnectionString);

            return files;
        }
    }
}
