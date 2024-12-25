using Cars.Data.Models.Models;
using Cars.Data.Services.Exceptions;
using Cars.Data.Services.Interfaces;
using Cars.Web.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Cars.Data.Services.Implementations
{
    public class CarService(CarsContext dbContext) : ICarService
    {
        public async Task<ResponseCarDTO> Get(long id)
        {
            Car? result = await dbContext
               .Cars
               .Include(x => x.Garages)
               .FirstOrDefaultAsync(x => x.Id == id)
               ?? throw new NotFoundException($"{nameof(Car)} with {id} is not found");

            IEnumerable<ResponseGarageDTO> garages =
                result.Garages.Select(x => new ResponseGarageDTO(x.Id, x.Name, x.Location, x.City, x.Capacity));

            return new ResponseCarDTO(result.Id, result.Make, result.Model, result.ProductionYear, result.LicensePlate, garages);
        }

        public async Task Delete(long id)
        {
            Car? carToDelete = await dbContext.Cars.FirstOrDefaultAsync(x => x.Id == id) ?? throw new NotFoundException($"{nameof(Car)} with {id} is not found");
            dbContext.Cars.Remove(carToDelete);
            await dbContext.SaveChangesAsync();
        }

        public async Task<ResponseCarDTO> Update(long id, UpdateCarDTO dto)
        {
            Car? result = await dbContext
                .Cars
                .Include(x => x.Garages)
                .FirstOrDefaultAsync(x => x.Id == id);

            ICollection<Garage> garages = await dbContext.Garages
                .Where(x => dto.GarageIds.Contains(x.Id))
                .ToListAsync();

            if (result == null)
            {
                throw new NotFoundException($"{nameof(Car)} with {id} is not found");
            }

            result.Make = dto.Make ?? result.Make;
            result.Model = dto.Model ?? result.Model;
            result.ProductionYear = dto.ProductionYear;
            result.LicensePlate = dto.LicensePlate ?? result.LicensePlate;
            result.Garages = garages;

            await dbContext.SaveChangesAsync();

            IEnumerable<ResponseGarageDTO> garagesDtos =
                result.Garages.Select(x => new ResponseGarageDTO(x.Id, x.Name, x.Location, x.City, x.Capacity));

            return new ResponseCarDTO(result.Id,
                result.Make,
                result.Model,
                result.ProductionYear,
                result.LicensePlate,
                garagesDtos);
        }

        public async Task<ResponseCarDTO> Create(CreateCarDTO dto)
        {
            Car carToCreate = new()
            {
                Make = dto.Make,
                Model = dto.Model,
                ProductionYear = dto.ProductionYear,
                LicensePlate = dto.LicensePlate
            };

            Car createdEntity = (await dbContext.Cars.AddAsync(carToCreate)).Entity;
            await dbContext.SaveChangesAsync();

            IEnumerable<ResponseGarageDTO> garagesDtos =
                createdEntity.Garages.Select(x => new ResponseGarageDTO(x.Id, x.Name, x.Location, x.City, x.Capacity));

            return new ResponseCarDTO(createdEntity.Id, createdEntity.Make, createdEntity.Model, createdEntity.ProductionYear, createdEntity.LicensePlate, garagesDtos);
        }

        public async Task<IEnumerable<ResponseCarDTO>> Search(string? carMake, long? garageId, int? fromYear, int? toYear)
        {
            IEnumerable<ResponseCarDTO> cars = await dbContext.Cars
                .Include(x => x.Garages)
                .Where(x => (string.IsNullOrWhiteSpace(carMake) || x.Make.Contains(carMake)) &&
                    (!garageId.HasValue || x.Garages.Any(g => g.Id == garageId.Value)) &&
                    (!fromYear.HasValue || x.ProductionYear >= fromYear.Value) &&
                    (!toYear.HasValue || x.ProductionYear <= toYear.Value))
                .Select(x => new ResponseCarDTO(x.Id, x.Make, x.Model, x.ProductionYear, x.LicensePlate,
                    x.Garages.Select(y => new ResponseGarageDTO(y.Id, y.Name, y.Location, y.City, y.Capacity))))
               .ToListAsync();

            return cars;
        }

        private readonly CarsContext dbContext = dbContext;
    }
}
