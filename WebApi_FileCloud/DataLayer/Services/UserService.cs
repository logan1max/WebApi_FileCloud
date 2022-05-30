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

        public async Task<List<User>> GetUsersById(int id)
        {
            var users = await new DB.MSSQL().Request<User>(UserQueries.GetUsersById(id), Account.ConnectionString);

            return users;
        }

        public async Task InsertUser(User u)
        {
            var temp = await new DB.MSSQL().Request<User>(UserQueries.GetUsersById(u.id_user), Account.ConnectionString);

            if (temp.FirstOrDefault() == null)
            {
                await new DB.MSSQL().Request(UserQueries.InsertUser(u), Account.ConnectionString);
            }
        }

        public async Task<bool> AuthUser(Person p)
        {
            var temp = await new DB.MSSQL().Request<User>(UserQueries.AuthUser(p), Account.ConnectionString);

            if (temp.FirstOrDefault() != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
