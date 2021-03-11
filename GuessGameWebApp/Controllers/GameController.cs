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

        private readonly IPlayerRepository _context;

        public GameController(IPlayerRepository context)
        {
            _context = context;

            //For InMemoryDb only. For Db usage run <update-database> in package manager, this line should be removed.
            _context = InMemoryDb.GetInMemoryRepository();
        }

        public Game CurrentGame;


        public IActionResult Index()
        {
            //Creates default Leader-board.
            if (_context.Player.Count() == 0)
            {
                Dictionary<string, int[]> userName = new Dictionary<string, int[]>()
                {
                    { "Bill", new[]{4, 1}},
                    { "Max", new[] { 3, 0 }},
                    { "Marie", new[] { 1, 0 }}
                };

                foreach (KeyValuePair<string, int[]> player in userName)
                {
                    for (int i = 0; i < player.Value[0]; i++)
                    {
                        CurrentGame = new Game();
                        CurrentGame.UserName = player.Key;
                        CurrentGame.GameStatus = i < player.Value[1] ? "won" : "lost";
                        LeaderBoardUpdateService.UpdateLeaderBoard(_context, CurrentGame);
                    }
                }
            }
            return View("Index");
        }

        public IActionResult FirstScreen()
        {
            return View("FirstScreen");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult FirstScreen(FirstScreenInput user)
        {
            if (!ModelState.IsValid)
            {
                return View("FirstScreen");
            }
            CurrentGame = new Game();

            CurrentGame.UserName = user.Name;

            ViewBag.TriesLeft = CurrentGame.TriesLeft;
            ViewBag.Greeting = user.Name;
            HttpContext.Session.SetString("SessionUser", JsonConvert.SerializeObject(CurrentGame));



            CurrentGame.UserName = user.Name;



            return View("GameScreen");
        }

        [HttpPost]
        public IActionResult HandleGuess(GuessInput guess)
        {

            if (HttpContext.Session.GetString("SessionUser") == null)
            {

                return View("FirstScreen");
            }

            Game sessionUser = JsonConvert.DeserializeObject<Game>(HttpContext.Session.GetString("SessionUser"));

            int triesLeft = sessionUser.TriesLeft;

            ViewBag.Greeting = sessionUser.UserName;
            ViewBag.TriesLeft = triesLeft;
            ViewBag.LogMessage = sessionUser.LogPrintOut;

            if (!ModelState.IsValid)
            {
                ViewBag.ScreenMessage = "Please enter 4 digits. 4 digits number can't start with 0.";
            }
            else
            {
                int numberRandom = sessionUser.RandomNum;

                string enteredNum = guess.GuessDigit1 + guess.GuessDigit2 + guess.GuessDigit3 + guess.GuessDigit4;

                int cleanNum = 0;

                if (!int.TryParse(enteredNum, out cleanNum) || enteredNum.Distinct().Count() != enteredNum.Length)
                {
                    ViewBag.ScreenMessage = "Digits must be distinct.";
                }
                else
                {
                    int[] result = GuessService.Guessing(numberRandom, cleanNum);

                    sessionUser.LogResults[triesLeft - 1] = result;

                    triesLeft--;

                    if (cleanNum == numberRandom || triesLeft == 0)
                    {
                        sessionUser.GameStatus = cleanNum == numberRandom ? "won" : "lost";

                        ViewBag.WinStatus = sessionUser.GameStatus;
                        ViewBag.SecretNumber = numberRandom;

                        LeaderBoardUpdateService.UpdateLeaderBoard(_context, sessionUser);

                        return View("GameOver");
                    }

                    ViewBag.TriesLeft = triesLeft;
                    ViewBag.ScreenMessage = "Your previous guess was: " + cleanNum.ToString();
                    ViewBag.Result = "P: " + result[1].ToString() + ", M: " + result[2].ToString();
                    ViewBag.LogMessage = Game.logBuilder(sessionUser);

                    sessionUser.TriesLeft = triesLeft;

                    sessionUser.LogResults[triesLeft] = result;

                    HttpContext.Session.SetString("SessionUser", JsonConvert.SerializeObject(sessionUser));
                }
            }
            return View("GameScreen");
        }


        public IActionResult LeaderBoard(string nStr)
        {
            int n = 1;

            int gamesTotal = _context.Player.AsEnumerable().Aggregate(0, (total, next) => total + next.GamesPlayed);

            Regex nRegex = new Regex("^[0-9]{1,2}$");

            if (nStr != null && nRegex.IsMatch(nStr))
            {
                n = int.Parse(nStr);
            }

            ViewBag.NgamesPlayed = n;

            var sortedRank = from r in _context.Player
                             where r.GamesPlayed >= n
                             select r;

            sortedRank = sortedRank.OrderByDescending(r => _context.Player.Count() != 0 ? (decimal)r.Wins / (decimal)gamesTotal : 0).ThenBy(g => g.GamesPlayed);

            ViewBag.gamesTotal = gamesTotal;

            return View(sortedRank);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
