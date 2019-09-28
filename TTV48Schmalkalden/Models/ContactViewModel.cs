using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TTV48Schmalkalden.Models
{
    public class ContactViewModel
    {
        [DataType(DataType.Text)]
        public string FullName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Die Email hat ein ungültiges Format")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Subject { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Message { get; set; }
    }
}
