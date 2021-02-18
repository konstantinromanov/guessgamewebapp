using GuessGameWebApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using GuessGameWebApp.Data;

namespace GuessGameWebApp.Models
{
    public class Game
    {
        public static int Tries = 8;

        public string UserName { get; set; }

        public int RandomNum { get; set; } = RandomNumberService.GenerateRandomNum();


        public int TriesLeft = Tries;

        public int[][] LogResults = new int[Tries][];

        public string LogPrintOut { get; set; }

        public string GameStatus = "Lost";


        public static string logBuilder(Game sessionUser)
        {
            string logPrintOut = "<div>";

            for (int j = Tries - 1; j >= sessionUser.TriesLeft - 1; j--)
            {
                logPrintOut = logPrintOut + (Tries - j) + ": " + "Number " + sessionUser.LogResults[j][0] + ", P: " + sessionUser.LogResults[j][1] + ", M: " + sessionUser.LogResults[j][2] + ". " + "<br />";
            }

            logPrintOut = logPrintOut + "<div />";

            return sessionUser.LogPrintOut = logPrintOut;
        }

    }
}
