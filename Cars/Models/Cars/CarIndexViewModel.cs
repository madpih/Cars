namespace Cars.Models.Cars
{
    public class CarIndexViewModel
    {
        public Guid? Id { get; set; }
        public string Make { get; set; } 
        
        public string Color { get; set; }
        public string Transmission { get; set; }
        public int Mileage { get; set; }
        public float Price { get; set; }

              
    }
}
