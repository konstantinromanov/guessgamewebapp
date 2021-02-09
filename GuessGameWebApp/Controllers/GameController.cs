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
            GamesHistory.Add(new Game() { RandomNum = RandomNumberService.GenerateRandomNum(), Tries = 4, TriesLeft = 4 });
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

            string logOut = "<div>";
            
            for (int j = GamesHistory.ElementAt(gameNumber).Tries - 1; j >= triesLeft - 1; j--)
            {
                logOut = logOut + (GamesHistory.ElementAt(gameNumber).Tries - j) + ": " + "Number " + GamesHistory.ElementAt(gameNumber).logResults[j][0] + ", P: " + GamesHistory.ElementAt(gameNumber).logResults[j][1] + ", M: " + GamesHistory.ElementAt(gameNumber).logResults[j][2] + ". " + "<br />";
            }

            logOut = ViewBag.LogOut + "<div />";
            

            triesLeft--;

            ViewBag.TriesLeft = triesLeft;
            ViewBag.PreviousGuess = "Your previous guess was: " + cleanNum.ToString();
            ViewBag.Result = "P: " + result[1].ToString() + ", M: " + result[2].ToString();
            ViewBag.Logout = logOut;

           


            GamesHistory.ElementAt(gameNumber).TriesLeft = triesLeft;
            GamesHistory.ElementAt(gameNumber).logResults[triesLeft] = result;

            

            if (triesLeft == 0)
            {
                ViewBag.SecretNumber = numberRandom;
                return View("GameOver");
            }

            return View("GameScreen", guess);
        }

        public IActionResult HandleButtonClick(Game guess)
        {


            return View("GameScreen");
        }




    }
}
