using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuessGameWebApp.Models
{
    public class Game
    {
        public string GuessNumber1 { get; set; }
        public string GuessNumber2 { get; set; }
        public string GuessNumber3 { get; set; }
        public string GuessNumber4 { get; set; }
        
        public string UserName { get; set; }
        public int TriesLeft { get; set; }
        public int RandomNum { get; set; }
        public int Tries { get; set; }

        public int[][] logResults = new int[4][];



    }
}
