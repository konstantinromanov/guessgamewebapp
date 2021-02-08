using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuessGameWebApp.Services
{
    public class RandomNumberService
    {

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

    }
}
