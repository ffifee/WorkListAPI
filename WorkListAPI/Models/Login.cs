using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkListAPI.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Potrebno je unijeti korisničko ime.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Potrebno je unijeti lozinku.")]
        public string Password { get; set; }
    }
}
