using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GuessGameWebApp.Models
{
    public class FirstScreenInput
    {
        [Required(ErrorMessage = "Required")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Must be at least 3 characters and maximum 20")]
        [RegularExpression("^[a-zA-Z0-9]{3,20}$", ErrorMessage = "Must letters and digits only")]
        public string Name { get; set; }
    }
}
