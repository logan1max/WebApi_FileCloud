using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi_FileCloud.DataLayer.Services;
using WebApi_FileCloud.Models;
using System.IO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi_FileCloud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        FileService _fSvc = new FileService();
        UserService _uSvc = new UserService();

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var dt = await _fSvc.GetFiles();

                return Content(JsonConvert.SerializeObject(dt, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore }));
            }
            catch (Exception ex) { return new BadRequestResult(); }
        }

       // [Route("Fileuser")]
        [HttpGet("{login}")]
        public async Task<IActionResult> Get(string login)
        {
            try
            {
                var users = await _uSvc.GetUsersByLogin(login);

                var dt = await _fSvc.GetFilesByUser(users.FirstOrDefault().id_user);

                return Content(JsonConvert.SerializeObject(dt, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore }));
            }
            catch (Exception ex) { return new BadRequestResult(); }
        }


        [Route("Insert")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddFileToUser addFile)
        {
            try
            {
                var users = await _uSvc.GetUsersByLogin(addFile.login);

                string path = @"\FileCloud\" + users.FirstOrDefault().id_user;

                DirectoryInfo dirInfo = new DirectoryInfo(path);
                if (!dirInfo.Exists)
                {
                    dirInfo.Create();
                }

                FileInfo fileInfo = new FileInfo(addFile.filePath);

                fileInfo.CopyTo(path + @"\" + fileInfo.Name);

                Files files = new Files
                {
                    name = fileInfo.Name,
                    size = fileInfo.Length,
                    source = fileInfo.FullName,
                    owner = users.FirstOrDefault().id_user
                };

                await _fSvc.InsertFile(files);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
