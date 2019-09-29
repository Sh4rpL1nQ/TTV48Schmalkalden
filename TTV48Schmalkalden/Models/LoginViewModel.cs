using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TTV48Schmalkalden.Models
{
    public class LoginViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(4)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
