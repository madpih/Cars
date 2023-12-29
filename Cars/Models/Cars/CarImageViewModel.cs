﻿namespace Cars.Models.Cars
{
    public class CarImageViewModel
    {
        public Guid ImageId { get; set; }
        public string ImageTitle { get; set; }
        public byte[] ImageData { get; set; }
        public string Image { get; set; }
        public Guid? CarId { get; set; }
    }
}
