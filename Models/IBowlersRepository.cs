using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission13.Models
{
    public interface IBowlersRepository 
    {
        IQueryable<Bowler> Bowlers { get; }

        public void Save(Bowler b)
        {

        }
        public void Edit(Bowler b)
        {

        }
        public void Delete(Bowler b)
        {

        }
        public List<Bowler> GetAll()
        {
            var x = new List<Bowler>();
            return x;
        }
    }
}
