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
        //static List<Game> GamesHistory = new List<Game>();
        Game random = new Game() { RandomNum = GenerateRandomNum() };

        public IActionResult Index()
        {


            //GamesHistory.Add(new Game { RandomNum = GenerateRandomNum() });
            return View();
        }

        public IActionResult GameScreen(Guess guess)
        {
            ViewBag.AskForGuessNumber = "Please enter 4 digits number";

            if (guess.GuessNumber == null)
            {

                return View();
            }
            int numberRandom = random.RandomNum;
            //int numberRandom = GamesHistory.ElementAt(0).RandomNum;
            int cleanNum = int.Parse(guess.GuessNumber);
            int[] result = Guessing(numberRandom, cleanNum);



            return View();
        }

        public IActionResult HandleButtonClick(Guess guess)
        {

            //GuessCheckService securityService = new GuessCheckService();

            //if (securityService.IsValid(guess))
            //{
            //    ViewBag.Message = "right";
            //}
            //else
            //{
            //    ViewBag.Message = "wrong";
            //}
            //ViewBag.Greeting = UserModel.username;

            //var numInput1 = guess;
            //int cleanNum = 0;

            //while (!int.TryParse(numInput1, out cleanNum) || numInput1.Length != numberRandom.ToString().Length)
            //{
            //    if (!int.TryParse(numInput1, out cleanNum))
            //    {
            //        Console.Write("This is not valid input. Please enter number: ");
            //    }
            //    else
            //    {
            //        Console.Write("Please enter {0} digits integer value: ", numberRandom.ToString().Length);
            //    }
            //    numInput1 = Console.ReadLine();
            //}


            //if (cleanNum == numberRandom)
            //{
            //    ViewBag.Message = "right";
            //}
            //else
            //{
            //    ViewBag.Message = "wrong";
            //}

            return View("GameScreen");
        }

        public static int GenerateRandomNum()
        {
            // Number of digits for random number to generate
            int randomDigits = 4;

            int _max = (int)Math.Pow(10, randomDigits);
            Random _rdm = new Random();
            int _out = _rdm.Next(0, _max);

            while (randomDigits != _out.ToString().ToArray().Distinct().Count())
            {
                _out = _rdm.Next(0, _max);
            }
            return _out;
        }

        public static int[] Guessing(int number, int input)
        {
            int p = 0;
            int m = 0;
            int[] numberArray = number.ToString().Select(c => Convert.ToInt32(c.ToString())).ToArray();
            int[] inputArray = input.ToString().Select(c => Convert.ToInt32(c.ToString())).ToArray();

            for (int i = 0; i < numberArray.Length; i++)
            {

                if (numberArray.Contains(inputArray[i]) && inputArray[i] != numberArray[i])
                {
                    m++;
                }
                if (inputArray[i] == numberArray[i])
                {
                    p++;
                }
            }
            int[] resultArray = new int[] { input, m, p };
            return resultArray;
        }
    }
}
