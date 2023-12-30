using Cars.Core.Dto;
using Cars.Core.ServiceInterface;
using Cars.Data;
using Cars.Models.Cars;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cars.Controllers
{
    public class CarsController : Controller
    {
        private readonly CarsContext _context;
        private readonly ICarsServices _carsServices;
        private readonly IFileServices _fileServices;


        public CarsController
            (
                CarsContext context,
                ICarsServices cars,
                IFileServices fileServices
            )
        {
            _context = context;
            _carsServices = cars;
            _fileServices = fileServices;
        }



        [HttpGet]
        public IActionResult Index()
        {
            var result = _context.Cars
                .OrderByDescending(y => y.CreatedAt)
                .Select(x => new CarIndexViewModel
                {
                    Id = x.Id,
                    Make = x.Make,
                    CarModel = x.CarModel,
                    Color = x.Color,
                    Price = x.Price,
                   
                });

            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var car = await _carsServices.DetailsAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            var photos = await _context.FileToDatabases
                .Where(x => x.CarId == id)
                .Select(y => new CarImageViewModel
                {
                    CarId = y.Id,
                    ImageId = y.Id,
                    ImageData = y.ImageData,
                    ImageTitle = y.ImageTitle,
                    Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
                }).ToArrayAsync();


            var vm = new CarDetailsViewModel();

            vm.Id = car.Id;
            vm.Make = car.Make;
            vm.CarModel = car.CarModel;
            vm.Transmission = car.Transmission;
            vm.Color = car.Color;
            vm.Mileage = car.Mileage;
            vm.Price = car.Price;
            vm.Engine = car.Engine;
            vm.Fuel = car.Fuel;
            vm.InitialReg = car.InitialReg;            
            vm.CreatedAt = car.CreatedAt;
            vm.UpdatedAt = car.UpdatedAt;
            vm.Image.AddRange(photos);

            return View(vm);
        }

        [HttpGet]
        public IActionResult Create()
        {
            CarCreateUpdateViewModel vm = new();

            return View("CreateUpdate", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CarCreateUpdateViewModel vm)
        {
            var dto = new CarDto()

            {
                Id = vm.Id,
                Make = vm.Make,
                CarModel = vm.CarModel,
                Transmission = vm.Transmission,
                Color = vm.Color,
                Mileage = vm.Mileage,
                Price = vm.Price,
                Engine = vm.Engine,
                Fuel = vm.Fuel,
                InitialReg = vm.InitialReg,
                CreatedAt = vm.CreatedAt,
                UpdatedAt = vm.UpdatedAt,
                Files = vm.Files,
                Image = vm.Image
                    .Select(x => new FileToDatabaseDto
                    {
                        Id = x.ImageId,
                        ImageData = x.ImageData,
                        ImageTitle = x.ImageTitle,
                        CarId = x.CarId
                    }).ToArray()

            };

            
            var result = await _carsServices.Create(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index), vm);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var car = await _carsServices.DetailsAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            var photos = await _context.FileToDatabases
                .Where(x => x.CarId == id)
                .Select(y => new CarImageViewModel
                {
                    CarId = y.Id,
                    ImageId = y.Id,
                    ImageData = y.ImageData,
                    ImageTitle = y.ImageTitle,
                    Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
                }).ToArrayAsync();

            var vm = new CarCreateUpdateViewModel();

            vm.Id = car.Id;
            vm.Make = car.Make;
            vm.CarModel=car.CarModel;
            vm.Transmission = car.Transmission;
            vm.Color = car.Color;
            vm.Mileage = car.Mileage;
            vm.Price = car.Price;
            vm.Engine = car.Engine;
            vm.Fuel = car.Fuel;
            vm.InitialReg = car.InitialReg;
            vm.CreatedAt = car.CreatedAt;
            vm.UpdatedAt = car.UpdatedAt;
            vm.Image.AddRange(photos);

            return View("CreateUpdate", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CarCreateUpdateViewModel vm)
        {

            var dto = new CarDto()
            {
                Id = vm.Id,
                Make = vm.Make,
                CarModel = vm.CarModel,
                Transmission = vm.Transmission,
                Color = vm.Color,
                Mileage = vm.Mileage,
                Price = vm.Price,
                Engine = vm.Engine,
                Fuel = vm.Fuel,
                InitialReg = vm.InitialReg,
                CreatedAt = vm.CreatedAt,
                UpdatedAt = vm.UpdatedAt,
                Files = vm.Files,
                Image = vm.Image
                    .Select(x => new FileToDatabaseDto
                    {
                        Id = x.ImageId,
                        ImageData = x.ImageData,
                        ImageTitle = x.ImageTitle,
                        CarId = x.CarId,
                    }).ToArray()

            };

            var result = await _carsServices.Update(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index), vm);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var car = await _carsServices.DetailsAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            var photos = await _context.FileToDatabases
                .Where(x => x.CarId == id)
                .Select(y => new CarImageViewModel
                {
                    CarId = y.Id,
                    ImageId = y.Id,
                    ImageData = y.ImageData,
                    ImageTitle = y.ImageTitle,
                    Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
                }).ToArrayAsync();

            var vm = new CarDeleteViewModel();

            vm.Id = car.Id;
            vm.Make = car.Make;
            vm.CarModel = car.CarModel;
            vm.Transmission = car.Transmission;
            vm.Color = car.Color;
            vm.Mileage = car.Mileage;
            vm.Price = car.Price;
            vm.Engine = car.Engine;
            vm.Fuel = car.Fuel;
            vm.InitialReg = car.InitialReg;
            vm.CreatedAt = car.CreatedAt;
            vm.UpdatedAt = car.UpdatedAt;
            vm.Image.AddRange(photos);

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var carId = await _carsServices.Delete(id);

            if (carId == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveImage(CarImageViewModel vm)
        {
            var dto = new FileToDatabaseDto()
            {
                Id = vm.ImageId
            };

            var image = await _fileServices.RemoveImageFromDatabase(dto);

            if (image == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }


    }

}
