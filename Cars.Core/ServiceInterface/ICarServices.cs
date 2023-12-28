using Cars.Core.Domain;
using Cars.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Cars.Core.ServiceInterface
{
    public interface ICarServices
    {
        Task<Car> Create(CarDto dto);
        Task<Car> DetailsAsync(Guid id);
        Task<Car> Delete(Guid id);
        Task<Car> Update(CarDto dto);
    }
}
