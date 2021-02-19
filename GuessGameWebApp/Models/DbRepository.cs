﻿using GuessGameWebApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuessGameWebApp.Models
{
    public class DbRepository : IPlayerRepository
    {
        private readonly ApplicationDbContext _db;

        public DbRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        
        public IQueryable<Player> Player => _db.Player;

        public void Update(Player player) => _db.Update(player);
        public void Add(Player player) => _db.Add(player);
        public void SaveChanges() => _db.SaveChanges();
    }
}
