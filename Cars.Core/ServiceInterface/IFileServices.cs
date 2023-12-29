using Cars.Core.Domain;
using Cars.Core.Dto;

namespace Cars.Core.ServiceInterface
{
    public interface IFileServices
    {
        void UploadFilesToDatabase(CarDto dto, Car domain);
        Task<FileToDatabase> RemoveImageFromDatabase(FileToDatabaseDto dto);
        Task RemoveImagesByCarId(Guid? CarId);
    }
}
