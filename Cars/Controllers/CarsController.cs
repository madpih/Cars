using Cars.Core.Dto;
using Cars.Core.ServiceInterface;
using Cars.Data;
using Cars.Models.Cars;
using Microsoft.AspNetCore.Mvc;

namespace Cars.Controllers
{
    public class CarsController : Controller
    {
        private readonly CarsContext _context;
        private readonly ICarsServices _carsServices;
        //private readonly IFileServices _fileServices;


        public CarsController
            (
                CarsContext context,
                ICarsServices cars
                //IFileServices fileServices
            )
        {
            _context = context;
            _carsServices = cars;
            //_fileServices = fileServices;
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
                    Color = x.Color,
                    Transmission = x.Transmission,
                    Mileage = x.Mileage,
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

           
            var vm = new CarDetailsViewModel();

            vm.Id = car.Id;
            vm.Make = car.Make;
            vm.Transmission = car.Transmission;
            vm.Color = car.Color;
            vm.Mileage = car.Mileage;
            vm.Price = car.Price;
            vm.Engine = car.Engine;
            vm.Fuel = car.Fuel;
            vm.InitialReg = car.InitialReg;            
            vm.CreatedAt = car.CreatedAt;
            vm.UpdatedAt = car.UpdatedAt;
            //vm.Image.AddRange(photos);

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
                Transmission = vm.Transmission,
                Color = vm.Color,
                Mileage = vm.Mileage,
                Price = vm.Price,
                Engine = vm.Engine,
                Fuel = vm.Fuel,
                InitialReg = vm.InitialReg,
                CreatedAt = vm.CreatedAt,
                UpdatedAt = vm.UpdatedAt,
             
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


            var vm = new CarCreateUpdateViewModel();

            vm.Id = car.Id;
            vm.Make = car.Make;
            vm.Transmission = car.Transmission;
            vm.Color = car.Color;
            vm.Mileage = car.Mileage;
            vm.Price = car.Price;
            vm.Engine = car.Engine;
            vm.Fuel = car.Fuel;
            vm.InitialReg = car.InitialReg;
            vm.CreatedAt = car.CreatedAt;
            vm.UpdatedAt = car.UpdatedAt;


            return View("CreateUpdate", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CarCreateUpdateViewModel vm)
        {

            var dto = new CarDto()
            {
                Id = vm.Id,
                Make = vm.Make,
                Transmission = vm.Transmission,
                Color = vm.Color,
                Mileage = vm.Mileage,
                Price = vm.Price,
                Engine = vm.Engine,
                Fuel = vm.Fuel,
                InitialReg = vm.InitialReg,
                CreatedAt = vm.CreatedAt,
                UpdatedAt = vm.UpdatedAt,

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

            var vm = new CarDeleteViewModel();

            vm.Id = car.Id;
            vm.Make = car.Make;
            vm.Transmission = car.Transmission;
            vm.Color = car.Color;
            vm.Mileage = car.Mileage;
            vm.Price = car.Price;
            vm.Engine = car.Engine;
            vm.Fuel = car.Fuel;
            vm.InitialReg = car.InitialReg;
            vm.CreatedAt = car.CreatedAt;
            vm.UpdatedAt = car.UpdatedAt;

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

                
    }

}
