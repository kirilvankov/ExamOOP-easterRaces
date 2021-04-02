using EasterRaces.Core.Contracts;
using EasterRaces.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using EasterRaces.Models.Cars.Entities;
using EasterRaces.Models.Drivers.Contracts;
using EasterRaces.Utilities.Messages;
using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Models.Races.Contracts;

namespace EasterRaces.Core.Entities
{
    public class ChampionshipController : IChampionshipController
    {
        private CarRepository cars;
        private DriverRepository drivers;
        private RaceRepository races;
        public ChampionshipController()
        {
            this.cars = new CarRepository();
            this.drivers = new DriverRepository();
            this.races = new RaceRepository();
        }
        public string AddCarToDriver(string driverName, string carModel)
        {
            if (!drivers.GetAll().Any(d => d.Name == driverName))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.DriverNotFound, driverName));
            }
            if (!cars.GetAll().Any(c => c.Model == carModel))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.CarNotFound, carModel));
            }
            IDriver driver = drivers.GetAll().First(d => d.Name == driverName);
            ICar car = cars.GetAll().First(c => c.Model == carModel);
            driver.AddCar(car);
            this.cars.Remove(car);                                              // ---- should I remove
            return $"Driver {driver.Name} received car {car.Model}.";
        }

        public string AddDriverToRace(string raceName, string driverName)
        {
            if (!this.races.GetAll().Any(r => r.Name == raceName))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceNotFound, raceName));
            }
            if (!this.drivers.GetAll().Any(d => d.Name == driverName))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.DriverNotFound, driverName));
            }
            IDriver driver = this.drivers.GetAll().First(d => d.Name == driverName);
            IRace race = this.races.GetAll().First(r => r.Name == raceName);
            race.AddDriver(driver);                                     //-----------------------------should I
            return $"Driver {driver.Name} added in {race.Name} race.";
        }

        public string CreateCar(string type, string model, int horsePower)
        {
            if (this.cars.GetAll().Any(c => c.Model == model))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CarExists, model));
            }
            Car car = null;
            if (type == "Muscle")
            {
                car = new MuscleCar(model, horsePower);
            }
            else if (type == "Sports")
            {
                car = new SportsCar(model, horsePower);
            }
            this.cars.Add(car);
            return $"{car.GetType().Name} {car.Model} is created.";
        }

        public string CreateDriver(string driverName)
        {
            IDriver driver; 
            if (drivers.GetAll().Any(d => d.Name == driverName))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.DriversExists, driverName));
            }
            driver = new Driver(driverName);
            this.drivers.Add(driver);
            return $"Driver {driverName} is created.";
        }

        public string CreateRace(string name, int laps)
        {
            if (races.GetAll().Any(r => r.Name == name))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceExists, name));
            }
            IRace race = new Race(name, laps);
            this.races.Add(race);
            return $"Race {race.Name} is created.";
        }

        public string StartRace(string raceName)
        {
            if (!this.races.GetAll().Any(r => r.Name == raceName))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceNotFound, raceName));
            }
            IRace race = this.races.GetAll().First(r => r.Name == raceName);
            if (race.Drivers.Count < 3)
            {
                throw new InvalidOperationException($"Race {race.Name} cannot start with less than 3 participants.");
            }
            
            List<IDriver> result = race.Drivers.OrderByDescending(d => d.Car.CalculateRacePoints(race.Laps)).ToList();
            result[0].WinRace();
            this.races.Remove(race);
            StringBuilder sb = new StringBuilder();
            sb
                .AppendLine($"Driver {result[0].Name} wins {race.Name} race.")
                .AppendLine($"Driver {result[1].Name} is second in {race.Name} race.")
                .AppendLine($"Driver {result[2].Name} is third in {race.Name} race.");
            return sb.ToString().TrimEnd();
        }
    }
}
