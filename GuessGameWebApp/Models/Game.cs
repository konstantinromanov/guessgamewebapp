using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuessGameWebApp.Models
{
    public class Game
    {
        public string GuessNumber { get; set; }
        public string UserName { get; set; }
        public int Tries { get; set; }
        public int RandomNum { get; set; }

        public int[][] logResults = new int[4][];



    }
}
