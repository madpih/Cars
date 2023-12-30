using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars.Core.Domain
{
    public class Car
    {
        [Key]
        public Guid? Id { get; set; }
        public string Make { get; set; }
        public string CarModel { get; set; }

        public DateTime InitialReg { get; set; }

        public string Engine { get; set; }
        public string Fuel { get; set; }
        public int Mileage { get; set; }
        public string Transmission { get; set; }

        public string Color { get; set; }
        public float Price { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }



    }

}

