using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebTemplateCSharp.Models
{
    public class UserAccounts
    {
        [Key]
        public int id { get; set; }
        [MaxLength(20)]
        public string id_number { get; set; }
        [MaxLength(50)]
        public string full_name { get; set; }
        [MaxLength(50)]
        public string username { get; set; }
        [MaxLength(50)]
        public string password { get; set; }
        [MaxLength(50)]
        public string section { get; set; }
        [MaxLength(20)]
        public string role { get; set; }
    }
}
