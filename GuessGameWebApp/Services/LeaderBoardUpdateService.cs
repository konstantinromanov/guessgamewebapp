using GuessGameWebApp.Data;
using GuessGameWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuessGameWebApp.Services
{
    public class LeaderBoardUpdateService
    {
        // For Db usage;
        //public static void UpdateLeaderBoard(ApplicationDbContext context, Game sessionUser)
        // For Static List usage.
        //public static void UpdateLeaderBoard(List<Player> context, Game sessionUser)
        public static void UpdateLeaderBoard(IPlayerRepository context, Game sessionUser)
        {

            var player = new Player();
            player.Name = sessionUser.UserName;

            // For Db usage.
            // (context.Player.Where(u => u.Name == player.Name).Any());  

            if (context.Player.Where(u => u.Name == player.Name).Any())
            {
                // For Db usage.
                //player = context.Player.First(m => m.Name == player.Name).
                // For Static List usage.
                //player = context.First(m => m.Name == player.Name);
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

                // For Db usage;
                //context.Update(player);
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
            // For Db usage;
            //context.SaveChanges();
            context.SaveChanges();
        }
    }
}
