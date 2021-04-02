using EasterRaces.Models.Drivers.Contracts;
using EasterRaces.Models.Races.Contracts;
using EasterRaces.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EasterRaces.Models.Cars.Entities
{
    public class Race : IRace
    {
        private const int MIN_LENGTH_NAME = 5;
        private const int LAPS_MIN_VALUE = 1;
        private string name;
        private int laps;
        private readonly List<IDriver> drivers;
        private Race()
        {
            this.drivers = new List<IDriver>();
        }
        public Race(string name, int laps) 
            : this()
        {
            this.Name = name;
            this.Laps = laps;
        }

        public string Name
        {
            get 
            { 
                return this.name; 
            }
            private set 
            {
                if (string.IsNullOrEmpty(value) || value.Length < MIN_LENGTH_NAME)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidName, value, MIN_LENGTH_NAME));
                }
                this.name = value; 
            }
        }

        public int Laps 
        {
            get
            {
                return this.laps;
            }
            private set
            {
                if (value < LAPS_MIN_VALUE)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidNumberOfLaps, LAPS_MIN_VALUE));
                }
                this.laps = value;
            }
        }

        public IReadOnlyCollection<IDriver> Drivers => this.drivers;

        public void AddDriver(IDriver driver)
        {
            if (driver == null)
            {
                throw new ArgumentNullException(nameof(IDriver), ExceptionMessages.DriverInvalid);
            }
            
            if (!driver.CanParticipate)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.DriverNotParticipate, driver.Name));
            }

            if (this.Drivers.Any(d => d.Name == driver.Name))
            {
                throw new ArgumentNullException(string.Format(ExceptionMessages.DriverAlreadyAdded, driver.Name, this.Name));
            }
            this.drivers.Add(driver);
        }
    }
}
