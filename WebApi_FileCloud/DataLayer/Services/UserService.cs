using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi_FileCloud.DataLayer.DB;
using WebApi_FileCloud.DataLayer.Queries;
using WebApi_FileCloud.Models;

namespace WebApi_FileCloud.DataLayer.Services
{
    public class UserService
    {
        public async Task<List<User>> GetUsers()
        {
            var users = await new DB.MSSQL().Request<User>(UserQueries.GetUsers(), Account.ConnectionString);
            
            return users;
        }
    }
}
