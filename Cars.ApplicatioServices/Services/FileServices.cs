using Cars.Core.Domain;
using Cars.Core.Dto;
using Cars.Core.ServiceInterface;
using Cars.Data;
using Microsoft.EntityFrameworkCore;

namespace Cars.ApplicatioServices.Services
{
          public class FileServices : IFileServices

        {
            private readonly CarsContext _context;

            public FileServices
                (
                   CarsContext context
                )
            {
                _context = context;
            }

            public void UploadFilesToDatabase(CarDto dto, Car domain)
            {
                if (dto.Files != null && dto.Files.Count > 0)
                {
                    foreach (var image in dto.Files)
                    {
                        using (var target = new MemoryStream())
                        {
                            FileToDatabase files = new FileToDatabase()
                            {
                                Id = Guid.NewGuid(),
                                ImageTitle = image.FileName,
                                CarId = domain.Id
                            };

                            image.CopyTo(target);
                            files.ImageData = target.ToArray();

                            _context.FileToDatabases.Add(files);
                            
                        }
                    //_context.SaveChangesAsync();
                }
                }
            }
           
            public async Task<FileToDatabase> RemoveImageFromDatabase(FileToDatabaseDto dto)
            {
                var imageId = await _context.FileToDatabases

                    .FirstOrDefaultAsync(x => x.Id == dto.Id);

                _context.FileToDatabases.Remove(imageId);
                await _context.SaveChangesAsync();

                return null;
            }

            public async Task RemoveImagesByCarId(Guid? carId)
            {
                var images = await _context.FileToDatabases
                    .Where(x => x.CarId == carId)
                    .ToListAsync();

                foreach (var image in images)
                {
                    _context.FileToDatabases.Remove(image);
                }

                await _context.SaveChangesAsync();
            }

          }
}
