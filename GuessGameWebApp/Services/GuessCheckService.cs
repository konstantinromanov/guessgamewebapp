using GuessGameWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuessGameWebApp.Services
{
    public class GuessCheckService
    {

        


        public bool IsValid(Guess number)
        {
            var cleanNumber = number.GuessNumber;
            //return true if found in the list
            return true;
        }

    }
}
