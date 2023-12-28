using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars.Core.Dto
{
    public class CarDto
    {
        public Guid? Id { get; set; }
        public string Make { get; set; }
        public DateTime InitialReg { get; set; }
        public int Engine { get; set; }
        public string Fuel { get; set; }
        public int Mileage { get; set; }
        public string Transmission { get; set; }

        public string Color { get; set; }
        public float Price { get; set; }

        //public List<IFormFile> Files { get; set; }
        //public IEnumerable<FileToDatabaseDto> Image { get; set; }
        //    = new List<FileToDatabaseDto>();

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
