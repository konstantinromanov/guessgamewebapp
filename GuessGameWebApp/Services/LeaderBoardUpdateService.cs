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
        
        public static void UpdateLeaderBoard(IPlayerRepository context, Game sessionUser)
        {

            var player = new Player();
            player.Name = sessionUser.UserName;
            
            if (context.Player.Where(u => u.Name == player.Name).Any())
            {
                
                player = context.Player.First(m => m.Name == player.Name);
                if (sessionUser.GameStatus == "won")
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
                if (sessionUser.GameStatus == "won")
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
