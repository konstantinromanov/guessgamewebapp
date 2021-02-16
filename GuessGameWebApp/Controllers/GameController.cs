using GuessGameWebApp.Models;
using GuessGameWebApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuessGameWebApp.Controllers
{
    public class GameController : Controller
    {

        public Game game;


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FirstScreen()
        {
            return View();
        }

        [HttpPost]
        public IActionResult FirstScreen(Game name)
        {
            game = new Game();
            game.UserName = name.UserName;
            game.RandomNum = RandomNumberService.GenerateRandomNum();
            ViewBag.TriesLeft = game.TriesLeft;
            ViewBag.Greeting = name.UserName;
            HttpContext.Session.SetString("SessionUser", JsonConvert.SerializeObject(game));
            return View("GameScreen", game);
        }

        public IActionResult GameScreen(Game game)
        {

            Game sessionUser = JsonConvert.DeserializeObject<Game>(HttpContext.Session.GetString("SessionUser"));

            ViewBag.Greeting = sessionUser.UserName;


            int triesLeft = sessionUser.TriesLeft;

            ViewBag.TriesLeft = triesLeft;
            ViewBag.Greeting = sessionUser.UserName;


            int numberRandom = sessionUser.RandomNum;
            int cleanNum = int.Parse(game.GuessNumber1 + game.GuessNumber2 + game.GuessNumber3 + game.GuessNumber4);


            int[] result = GuessService.Guessing(numberRandom, cleanNum);


            sessionUser.logResults[triesLeft - 1] = result;

            if (cleanNum == numberRandom)
            {
                ViewBag.WinStatus = "You won!";
                ViewBag.SecretNumber = numberRandom;
                sessionUser.GameStatus = "Won";
                return View("GameOver");
            }



            string logOut = "<div>";

            for (int j = Game.Tries - 1; j >= triesLeft - 1; j--)
            {                
                logOut = logOut + (Game.Tries - j) + ": " + "Number " + sessionUser.logResults[j][0] + ", P: " + sessionUser.logResults[j][1] + ", M: " + sessionUser.logResults[j][2] + ". " + "<br />";
            }

            logOut = logOut + "<div />";


            triesLeft--;

            ViewBag.TriesLeft = triesLeft;
            ViewBag.PreviousGuess = "Your previous guess was: " + cleanNum.ToString();
            ViewBag.Result = "P: " + result[1].ToString() + ", M: " + result[2].ToString();
            ViewBag.Logout = logOut;
            
            sessionUser.TriesLeft = triesLeft;
            
            sessionUser.logResults[triesLeft] = result;



            if (triesLeft == 0)
            {
                ViewBag.WinStatus = "You lost!";
                ViewBag.SecretNumber = numberRandom;               

                sessionUser.GameStatus = "Lost";
                return View("GameOver");
            }

            HttpContext.Session.SetString("SessionUser", JsonConvert.SerializeObject(sessionUser));

            return View();
        }

        public IActionResult LeaderBoard()
        {
            return View();
        }
    }
}
