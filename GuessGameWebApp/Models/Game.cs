using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuessGameWebApp.Models
{
    public class Game
    {

        public int RandomNum { get; set; }


        public static int guessTimes = 4;
        int[][] logResults = new int[guessTimes][];



    }
}
