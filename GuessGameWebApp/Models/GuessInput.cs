using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GuessGameWebApp.Models
{
    public class GuessInput
    {
        [Required(ErrorMessage = "Required")]
        [StringLength(1, ErrorMessage = "Must be single value")]
        [RegularExpression("[1-9]", ErrorMessage = "First must be non 0 digit")]
        public string GuessDigit1 { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(1, ErrorMessage = "Must be single value")]
        [RegularExpression("[0-9]", ErrorMessage = "Must be digit")]
        public string GuessDigit2 { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(1, ErrorMessage = "Must be single value")]
        [RegularExpression("[0-9]", ErrorMessage = "Must be digit")]
        public string GuessDigit3 { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(1, ErrorMessage = "Must be single value")]
        [RegularExpression("[0-9]", ErrorMessage = "Must be digit")]
        public string GuessDigit4 { get; set; }

    }
}
