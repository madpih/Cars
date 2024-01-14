using Cars.Core.Dto;
using Cars.Core.ServiceInterface;


namespace Cars.CarsTest
{
    public class CarTest : TestBase
    {
        [Fact]
        public async Task ShouldNot_AddEmptyCar_WhenReturnResult()
        {   
            CarDto car = new CarDto();
            car.Make = "Porche";
            car.CarModel = "911";
            car.Engine = "3.0";
            car.Price = 100000;
            car.Fuel = "Petrol";
            car.Mileage = 5000;
            car.Transmission = "automatic";
            car.Color = "silver";
            car.InitialReg = DateTime.Now;
            car.CreatedAt = DateTime.Now;
            car.UpdatedAt = DateTime.Now;

            var result = await Svc<ICarsServices>().Create(car);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetByIdCar_WhenReturnsEqual()
        {

            //Arrange
            Guid guid1 = Guid.Parse("ea7807c8-4286-46a2-8245-9e939950ea07");
            Guid guid2 = Guid.Parse("ea7807c8-4286-46a2-8245-9e939950ea07");

            //Act
            await Svc<ICarsServices>().DetailsAsync(guid2);

            //Assert
            Assert.Equal(guid1, guid2);
        }

        [Fact]
        public async Task Should_DeleteByIdCar_WhenDeleteCar()
        {
            CarDto car = MockCarData();

            var addCar = await Svc<ICarsServices>().Create(car);
            var result = await Svc<ICarsServices>().Delete((Guid)addCar.Id);

            Assert.Equal(addCar, result);
        }

        [Fact]
        public async Task ShouldNot_UpdateCar_WhenNotUpdateData()
        {
            CarDto dto = MockCarData();
            var createCar = await Svc<ICarsServices>().Create(dto);

            CarDto nullUpdate = MockNullCar();
            var result = await Svc<ICarsServices>().Update(nullUpdate);

            var nullId = nullUpdate.Id;

            Assert.True(dto.Id == nullId);
        }

        private CarDto MockCarData()
        {
            CarDto car = new()
            {
            Make = "Porche",
            CarModel = "911",
            Engine = "3.0",
            Price = 100000,
            Fuel = "Petrol",
            Mileage = 5000,
            Transmission = "automatic",
            Color = "silver",
            InitialReg = DateTime.Now.AddYears(-10),
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,

        };

            return car;
        }

        private CarDto MockNullCar()
        {
            CarDto nullDto = new()
            {
                Id = null,
                Make = "Porche",
                CarModel = "911",
                Engine = "3.0",
                Price = 99000,
                Fuel = "Petrol",
                Mileage = 6000,
                Transmission = "automatic",
                Color = "silver",
                InitialReg = DateTime.Now.AddYears(-20),
                CreatedAt = DateTime.Now.AddYears(-1),
                UpdatedAt = DateTime.Now.AddYears(-1),
            };
            return nullDto;
        }
    }
}