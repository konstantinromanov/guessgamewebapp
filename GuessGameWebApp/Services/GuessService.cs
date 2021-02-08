using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuessGameWebApp.Services
{
    public class GuessService
    {


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
