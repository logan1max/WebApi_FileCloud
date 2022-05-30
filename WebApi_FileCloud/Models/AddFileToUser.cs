using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi_FileCloud.Models
{
    public class AddFileToUser
    {
        public string login { get; set; }
        public string filePath { get; set; }
    }
}
