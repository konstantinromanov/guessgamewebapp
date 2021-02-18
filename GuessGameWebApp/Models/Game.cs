using GuessGameWebApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using GuessGameWebApp.Data;

namespace GuessGameWebApp.Models
{
    public class Game
    {
        public static int Tries = 8;

        public string UserName { get; set; }

        public int RandomNum { get; set; } = RandomNumberService.GenerateRandomNum();


        public int TriesLeft = Tries;

        public int[][] logResults = new int[Tries][];

        public string logPrintOut { get; set; }

        public string GameStatus = "Lost";



        public static void UpdateLeaderBoard(ApplicationDbContext context, Game sessionUser)
        {
            
            var player = new Player();
            player.Name = sessionUser.UserName;


            if (context.Player.Where(u => u.Name == player.Name).Any())
            {

                player = context.Player.First(m => m.Name == player.Name);
                if (sessionUser.GameStatus == "Won")
                {
                    player.Wins += 1;
                }
                else
                {
                    player.Loses += 1;
                }

                player.GamesPlayed += 1;
                player.Rank = (decimal)player.Wins / (decimal)player.GamesPlayed;

                context.Update(player);
            }
            else
            {
                if (sessionUser.GameStatus == "Won")
                {
                    player.Wins = 1;
                    player.Loses = 0;
                }
                else
                {
                    player.Wins = 0;
                    player.Loses = 1;
                }

                player.GamesPlayed = 1;
                player.Rank = (decimal)player.Wins / (decimal)player.GamesPlayed;

                context.Add(player);
            }

            context.SaveChanges();
        }
    }
}
