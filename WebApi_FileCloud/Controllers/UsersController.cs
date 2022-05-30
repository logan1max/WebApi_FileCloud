using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApi_FileCloud.DataLayer.Services;
using WebApi_FileCloud.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi_FileCloud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        UserService _uSvc = new UserService();

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var dt = await _uSvc.GetUsers();

                return Content(JsonConvert.SerializeObject(dt, Formatting.Indented, new JsonSerializerSettings
                { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore }));
            }
            catch(Exception ex)
            {
                return new BadRequestResult();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var dt = await _uSvc.GetUsersById(id);

                return Content(JsonConvert.SerializeObject(dt, Formatting.Indented, new JsonSerializerSettings
                { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore }));
            }
            catch(Exception ex)
            {
                return new BadRequestResult();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User u)
        {
            try
            {
                await _uSvc.InsertUser(u);

                string path = @"\FileCloud\" + u.id_user.ToString();

                DirectoryInfo dirInfo = new DirectoryInfo(path);
                if (!dirInfo.Exists)
                {
                    dirInfo.Create();
                }

                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }
        }

        [Route("Authorize")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Person p)
        {
            var res = await _uSvc.AuthUser(p);

            if (res == true)
            {
                return new OkResult();
            }
            else
            {
                return new UnauthorizedResult();
            }
            
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
