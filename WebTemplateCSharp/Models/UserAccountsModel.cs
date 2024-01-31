using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebTemplateCSharp.Models
{
    public class UserAccountsModel
    {
        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        [Display(Name = "IdNumber")]
        public string IdNumber { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "FullName")]
        public string FullName { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "Username")]
        public string Username { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "Section")]
        public string Section { get; set; }
        [Required]
        [MaxLength(20)]
        [Display(Name = "Role")]
        public string Role { get; set; }
    }
}
