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

            return View();
        }

        public IActionResult FirstScreen()
        {
            GamesHistory.Add(new Game() { RandomNum = RandomNumberService.GenerateRandomNum() });
            return View();
        }

        public IActionResult GameScreen(Game guess)
        {


            int gameNumber = GamesHistory.Count() - 1;

            ViewBag.Greeting = GamesHistory.ElementAt(gameNumber).UserName;

            int triesLeft = GamesHistory.ElementAt(gameNumber).TriesLeft;


            if (guess.GuessNumber1 == null)
            {
                ViewBag.TriesLeft = triesLeft;
                string player = guess.UserName;
                ViewBag.Greeting = player;
                GamesHistory.ElementAt(gameNumber).UserName = player;
                return View();
            }



            int numberRandom = GamesHistory.ElementAt(gameNumber).RandomNum;

            int cleanNum = int.Parse(guess.GuessNumber1 + guess.GuessNumber2 + guess.GuessNumber3 + guess.GuessNumber4);




            int[] result = GuessService.Guessing(numberRandom, cleanNum);

            GamesHistory.ElementAt(gameNumber).logResults[triesLeft - 1] = result;

            if (cleanNum == numberRandom)
            {
                ViewBag.WinStatus = "You won!";
                ViewBag.SecretNumber = numberRandom;
                GamesHistory.ElementAt(gameNumber).GameStatus = "Won";
                return View("GameOver");
            }
            


            string logOut = "<div>";

            for (int j = Game.Tries - 1; j >= triesLeft - 1; j--)
            {
                logOut = logOut + (Game.Tries - j) + ": " + "Number " + GamesHistory.ElementAt(gameNumber).logResults[j][0] + ", P: " + GamesHistory.ElementAt(gameNumber).logResults[j][1] + ", M: " + GamesHistory.ElementAt(gameNumber).logResults[j][2] + ". " + "<br />";
            }

            logOut = logOut + "<div />";


            triesLeft--;

            ViewBag.TriesLeft = triesLeft;
            ViewBag.PreviousGuess = "Your previous guess was: " + cleanNum.ToString();
            ViewBag.Result = "P: " + result[1].ToString() + ", M: " + result[2].ToString();
            ViewBag.Logout = logOut;




            GamesHistory.ElementAt(gameNumber).TriesLeft = triesLeft;
            GamesHistory.ElementAt(gameNumber).logResults[triesLeft] = result;



            if (triesLeft == 0)
            {
                ViewBag.WinStatus = "You lost!";
                ViewBag.SecretNumber = numberRandom;
                GamesHistory.ElementAt(gameNumber).GameStatus = "Lost";
                return View("GameOver");
            }

            return View();
        }

        public IActionResult LeaderBoard()
        {
            return View(GamesHistory);
        }
    }
}
