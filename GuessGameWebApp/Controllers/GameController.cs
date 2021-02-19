using GuessGameWebApp.Data;
using GuessGameWebApp.Models;
using GuessGameWebApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace GuessGameWebApp.Controllers
{
    public class GameController : Controller
    {
        // For Static List Db usage;
        //private static List<Player> _context = new List<Player>();

        //For InMemoryDb;
        public static IPlayerRepository _context = InMemoryDb.GetInMemoryRepository();

        // For Db usage;
        //private readonly IPlayerRepository _context;

        // For Db usage;
        //public GameController(IPlayerRepository context)
        //{
        //    _context = context;
        //}

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
        [ValidateAntiForgeryToken]
        public IActionResult FirstScreen(FirstScreenInput user)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            game = new Game();
            game.UserName = user.Name;

            ViewBag.TriesLeft = game.TriesLeft;
            ViewBag.Greeting = user.Name;
            HttpContext.Session.SetString("SessionUser", JsonConvert.SerializeObject(game));

            return View("GameScreen");

        }

        [HttpPost]
        public IActionResult HandleGuess(GuessInput guess)
        {
            Game sessionUser = JsonConvert.DeserializeObject<Game>(HttpContext.Session.GetString("SessionUser"));

            if (HttpContext.Session.GetString("SessionUser") == null)
            {
                return View("FirstScreen");
            }


            int triesLeft = sessionUser.TriesLeft;

            if (!ModelState.IsValid)
            {
                ViewBag.Greeting = sessionUser.UserName;
                ViewBag.TriesLeft = triesLeft;
                ViewBag.ScreenMessage = "Please enter 4 digits. 4 digits number can't start with 0.";
                ViewBag.Logout = sessionUser.LogPrintOut;

                return View("GameScreen");
            }

            int numberRandom = sessionUser.RandomNum;


            string enteredNum = guess.GuessDigit1 + guess.GuessDigit2 + guess.GuessDigit3 + guess.GuessDigit4;
            int cleanNum = 0;

            if (!int.TryParse(enteredNum, out cleanNum) || cleanNum.ToString().Length != numberRandom.ToString().Length || enteredNum.Distinct().Count() != enteredNum.Count())
            {
                ViewBag.ScreenMessage = "Digits must be distinct.";
                ViewBag.Logout = sessionUser.LogPrintOut;

                return View("GameScreen");
            }


            int[] result = GuessService.Guessing(numberRandom, cleanNum);


            sessionUser.LogResults[triesLeft - 1] = result;

            if (cleanNum == numberRandom)
            {
                ViewBag.WinStatus = "You won!";
                ViewBag.SecretNumber = numberRandom;

                sessionUser.GameStatus = "Won";

                LeaderBoardUpdateService.UpdateLeaderBoard(_context, sessionUser);

                return View("GameOver");
            }

            triesLeft--;

            if (triesLeft == 0)
            {
                ViewBag.WinStatus = "You lost!";
                ViewBag.SecretNumber = numberRandom;

                sessionUser.GameStatus = "Lost";

                LeaderBoardUpdateService.UpdateLeaderBoard(_context, sessionUser);

                return View("GameOver");
            }

            ViewBag.TriesLeft = triesLeft;
            ViewBag.ScreenMessage = "Your previous guess was: " + cleanNum.ToString();
            ViewBag.Result = "P: " + result[1].ToString() + ", M: " + result[2].ToString();
            ViewBag.Logout = Game.logBuilder(sessionUser);

            sessionUser.TriesLeft = triesLeft;

            sessionUser.LogResults[triesLeft] = result;

            HttpContext.Session.SetString("SessionUser", JsonConvert.SerializeObject(sessionUser));

            return View("GameScreen");
        }

        
        public IActionResult LeaderBoard(string nStr)
        {
            int n = 1;

            Regex nRegex = new Regex("^[0-9]{1,2}$");

            if (nStr != null && nRegex.IsMatch(nStr))
            {
                n = int.Parse(nStr);
            }

            ViewBag.NgamesPlayed = n;

            var sortedRank = from r in _context.Player // For Static List usage _context // For Db usage _context.Player
                             where r.GamesPlayed >= n
                             select r;

            sortedRank = sortedRank.OrderByDescending(r => r.Rank).ThenBy(g => g.GamesPlayed);

            return View(sortedRank);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
