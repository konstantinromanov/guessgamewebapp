using Castle.MicroKernel.SubSystems.Conversion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GuessGameWebApp.Models
{
    public class Player
    {
        public int Id { get; set; }

        [Display(Name = "Player")]
        public string Name { get; set; }

        [Display(Name = "Games played")]
        public int GamesPlayed { get; set; } = 0;

        [Display(Name = "Loses")]
        public int Loses { get; set; } = 0;

        [Display(Name = "Wins")]
        public int Wins { get; set; } = 0;

        [Display(Name = "Rank")]
        [DisplayFormat(DataFormatString = "{0:0.00#}")]
        public decimal Rank { get; set; } = 0;

    }
}
