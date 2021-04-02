
namespace EasterRaces.Models.Cars.Entities
{
    public class MuscleCar : Car
    {
        
        public MuscleCar(string model, int horsePower) 
            : base(model, horsePower, CarSpecification.muscleCarCubicCentimeters,
                  CarSpecification.muscleCarMinHorsePower, CarSpecification.muscleCarMaxHorsePower)
        {
        }

        
        
    }
}
