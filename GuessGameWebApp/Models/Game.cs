using GuessGameWebApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuessGameWebApp.Models
{
    public class Game
    {
        public static int Tries = 8;       

        public string UserName { get; set; }

        public int RandomNum { get; set; } // = RandomNumberService.GenerateRandomNum();


        public int TriesLeft = Tries;

        public int[][] logResults = new int[Tries][];

        public string logOut { get; set; }

        public string GameStatus = "Lost";


    }
}
