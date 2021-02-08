using GuessGameWebApp.Models;
using GuessGameWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuessGameWebApp.Controllers
{
    public class GameController : Controller
    {
        static List<Game> GamesHistory = new List<Game>();


        public IActionResult Index()
        {
            GamesHistory.Add(new Game() { RandomNum = RandomNumberService.GenerateRandomNum(), Tries = 4 });
            return View();
        }

        public IActionResult GameScreen(Game guess)
        {
            ViewBag.AskForGuessNumber = "Please enter 4 digits number";
            int gameNumber = GamesHistory.Count() - 1;

            if (guess.GuessNumber == null)
            {
                string player = guess.UserName;
                GamesHistory.ElementAt(gameNumber).UserName = player;
                return View();
            }
            int numberRandom = GamesHistory.ElementAt(gameNumber).RandomNum;

            int cleanNum = int.Parse(guess.GuessNumber);

            int[] result = GuessService.Guessing(numberRandom, cleanNum);
            int a = GamesHistory.ElementAt(gameNumber).Tries;

            GamesHistory.ElementAt(gameNumber).logResults[a - 1] = result;
            a--;
            GamesHistory.ElementAt(gameNumber).Tries = a;


            return View("GameScreen", guess);
        }

        public IActionResult HandleButtonClick(Game guess)
        {

           
            return View("GameScreen");
        }

        


    }
}
