using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace GuessGameWebApp.Models
{
    public interface IPlayerRepository
    {
        public IQueryable<Player> Player { get; }

        //void Add(Player player);
        //void Update(Player player);
        //void SaveChanges();

        void Add<EntityType>(EntityType player);
        void Update<EntityType>(EntityType player);
        void SaveChanges();        
    }
}
