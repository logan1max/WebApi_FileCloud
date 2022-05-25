using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi_FileCloud.DataLayer.DB;
using WebApi_FileCloud.DataLayer.Queries;
using WebApi_FileCloud.Models;

namespace WebApi_FileCloud.DataLayer.Services
{
    public class UserFileService
    {
        public async Task<List<UserFile>> GetUserFiles()
        {
            var users = await new DB.MSSQL().Request<UserFile>(UserFileQueries.GetUserFiles(), Account.ConnectionString);

            return users;
        }
    }
}
