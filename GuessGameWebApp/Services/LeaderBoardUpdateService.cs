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
                    player.Losses += 1;
                }

                player.GamesPlayed += 1;

                player.Rank = CalculateRank(player);                

                context.Update(player);
            }
            else
            {
                if (sessionUser.GameStatus == "won")
                {
                    player.Wins = 1;
                    player.Losses = 0;
                }
                else
                {
                    player.Wins = 0;
                    player.Losses = 1;
                }

                player.GamesPlayed = 1;

                player.Rank = CalculateRank(player);                

                context.Add(player);
            }
            context.SaveChanges();
        }

        //Creates default Leader-board.
        public static void DefaultLeaderboard(IPlayerRepository context, Game currentGame)
        {

            if (context.Player.Count() == 0)
            {
                Dictionary<string, int[]> userName = new Dictionary<string, int[]>()
                {                    
                    { "Tatjana", new[] {1, 1}},
                    { "Bill", new[] { 4, 1 }},
                    { "Konstantin", new[] { 5, 1 }},
                    { "Edvards", new[] { 1, 0 }},
                    { "Marie", new[] { 1, 0 }},
                    { "Gatis", new[] { 1, 0 }},
                    { "sadsd", new[] { 1, 0 }},
                    { "Max", new[] { 3, 0 }},
                    { "Jenny", new[] { 3, 0 }},

                };

                foreach (KeyValuePair<string, int[]> player in userName)
                {
                    for (int i = 0; i < player.Value[0]; i++)
                    {
                        currentGame = new Game();
                        currentGame.UserName = player.Key;
                        currentGame.GameStatus = i < player.Value[1] ? "won" : "lost";
                        UpdateLeaderBoard(context, currentGame);
                    }
                }
            }
        }

        private static float CalculateRank(Player player)
        {
            return player.Rank = (float)((((float)player.Wins + 1.9208) / (player.Wins + player.Losses) - 1.96 * Math.Sqrt(player.Wins * player.Losses / (player.Wins + player.Losses) + 0.9604) / (player.Wins + player.Losses)) / (1 + 3.8416 / (player.Wins + player.Losses)));
        }
    }
}
