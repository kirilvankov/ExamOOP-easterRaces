namespace EasterRaces.Models.Cars.Entities
{
    public class SportsCar : Car
    {
        public SportsCar(string model, int horsePower) 
            : base(model, horsePower, CarSpecification.sportCarCubicCentimeters, 
                  CarSpecification.sportCarMinHorsePower, CarSpecification.sportCarMaxHorsePower)
        {
        }
    }
}
