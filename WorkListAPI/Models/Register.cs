using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WorkListAPI.Models
{
    public class Register
    {   
        [Required(ErrorMessage = "Potrebno je korisničko ime.")]
        public string UserName { get; set; }

        [RegularExpression(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
            + "@"
            + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$",
            ErrorMessage = "Unesite ispravnu email adresu.")]
        [Required(ErrorMessage = "Potrebna je email adresa.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Potrebna je lozinka.")]
        public string Password { get; set; }
    }
}
