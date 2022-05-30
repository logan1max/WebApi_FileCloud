using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi_FileCloud.Models
{
    public class UserFile
    {
        public int id_file { get; set; }
        public int id_user { get; set; }
        public string login { get; set; }
        public string name { get; set; }
        public string source { get; set; }
    }
}
