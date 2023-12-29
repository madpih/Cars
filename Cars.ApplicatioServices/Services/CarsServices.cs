using Cars.Core.Domain;
using Cars.Core.Dto;
using Cars.Core.ServiceInterface;
using Cars.Data;
using Microsoft.EntityFrameworkCore;


namespace Cars.ApplicatioServices.Services
{
    public class CarsServices : ICarsServices

    {
        private readonly CarsContext _context;
        private readonly IFileServices _fileServices;

        public CarsServices
            (
            CarsContext context,
            IFileServices fileServices
            )
        {
            _context = context;
            _fileServices = fileServices;
        }

        public async Task<Car> Create(CarDto dto)
        {
            Car car = new Car();
            car.Id = Guid.NewGuid();
            car.Make = dto.Make;
            car.Engine = dto.Engine;
            car.InitialReg = dto.InitialReg;
            car.Mileage = dto.Mileage;
            car.Color = dto.Color;
            car.Fuel = dto.Fuel;
            car.Transmission = dto.Transmission;
            car.Price = dto.Price;
            car.CreatedAt = dto.CreatedAt;
            car.UpdatedAt = dto.UpdatedAt;

            if (dto.Files != null)
            {
                _fileServices.UploadFilesToDatabase(dto, car);
            }

            await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();

            return car;
        }

        public async Task<Car> Update(CarDto dto)
        {
            Car car = new Car();

            car.Id = dto.Id;
            car.Make = dto.Make;
            car.Engine = dto.Engine;
            car.InitialReg = dto.InitialReg;
            car.Mileage = dto.Mileage;
            car.Color = dto.Color;
            car.Fuel = dto.Fuel;
            car.Transmission = dto.Transmission;
            car.Price = dto.Price;
            car.CreatedAt = dto.CreatedAt;
            car.UpdatedAt = dto.UpdatedAt;

            if (dto.Files != null)
            {
                _fileServices.UploadFilesToDatabase(dto, car);
            }

            _context.Cars.Update(car);
            await _context.SaveChangesAsync();

            return car;
        }
        public async Task<Car> DetailsAsync(Guid id)
        {
            var result = await _context.Cars
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public async Task<Car> Delete(Guid id)
        {
            var car = await _context.Cars
                .FirstOrDefaultAsync(x => x.Id == id);

            if (car != null)
            {
                // Remove associated images from the database
                await _fileServices.RemoveImagesByCarId(car.Id);

                _context.Cars.Remove(car);
                await _context.SaveChangesAsync();
            }

            return car;
        }
    }
}
