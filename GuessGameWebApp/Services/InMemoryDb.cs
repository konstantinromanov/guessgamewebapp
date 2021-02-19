using GuessGameWebApp.Data;
using GuessGameWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace GuessGameWebApp.Services
{
    public static class InMemoryDb
    {

        public static IPlayerRepository GetInMemoryRepository()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                              .UseInMemoryDatabase(databaseName: "Leaderboard")
                              .Options;

            var context = new ApplicationDbContext(options);

            var repository = new DbRepository(context);

            return repository;
        }

    }
}
