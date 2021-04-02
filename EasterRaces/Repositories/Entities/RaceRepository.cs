using System.Collections.Generic;
using System.Linq;
using EasterRaces.Models.Cars.Entities;
using EasterRaces.Models.Races.Contracts;
using EasterRaces.Repositories.Contracts;

namespace EasterRaces.Repositories.Entities
{
    public class RaceRepository : IRepository<IRace>
    {
        private readonly ICollection<IRace> races;
        public RaceRepository()
        {
            this.races = new List<IRace>();
        }

        public void Add(IRace model)
        {
            this.races.Add(model);
        }

        public IReadOnlyCollection<IRace> GetAll()
        {
            return this.races.ToList();
        }

        public IRace GetByName(string name)
        {
            return races.FirstOrDefault(r => r.Name == name);
        }

        public bool Remove(IRace model)
        {
            return this.races.Remove(model);
        }
    }
}
