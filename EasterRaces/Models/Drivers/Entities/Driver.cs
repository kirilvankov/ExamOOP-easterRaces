using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Models.Drivers.Contracts;
using EasterRaces.Utilities.Messages;
using System;

namespace EasterRaces.Models.Cars.Entities
{
    public class Driver : IDriver
    {
        private const int MIN_LENGTH_NAME = 5;
        private string name;
        //private bool canParticipate;
        public Driver(string name)
        {
            this.Name = name;
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


        public ICar Car { get; private set; }


        public int NumberOfWins { get; private set; }

        public bool CanParticipate => this.Car != null;


        public void AddCar(ICar car)
        {
            if (car == null)
            {
                throw new ArgumentNullException(nameof(ICar), ExceptionMessages.CarInvalid);
            }
            this.Car = car;
        }

        public void WinRace()
        {
            this.NumberOfWins++;
        }
    }
}
