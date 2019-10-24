using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NorthwindMVC.Models
{
    public class Logins
    {
        public int LoginID { get; set; }
        [Required(ErrorMessage = "Anna käyttäjätunnus")]
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Anna salasana.")]
        public string PassWord { get; set; }
        public string LoginErrorMessage { get; set; }
    }
}