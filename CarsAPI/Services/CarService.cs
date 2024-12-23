using CarsAPI.Dtos;
using CarsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CarsAPI.Services
{
    public class CarService(CarsContext dbContext)
    {
        public async Task<ResponseCarDTO> GetCarById(long id)
        {
            Car? result = await this.dbContext
               .Cars
               .Include(x => x.Garages)
               .FirstOrDefaultAsync(x => x.Id == id);

            if (result == null)
            {
                //to do
                return null;
            }

            IEnumerable<ResponseGarageDTO> garages = 
                result.Garages.Select(x => new ResponseGarageDTO(x.Id, x.Name, x.Location, x.City, x.Capacity));

            return new ResponseCarDTO(result.Id, result.Make, result.Model, result.ProductionYear, result.LicensePlate, garages);
        }

        public async Task DeleteCarById(long id)
        {
            Car? carToDelete = await dbContext.Cars.FirstOrDefaultAsync(x => x.Id == id);

            if (carToDelete == null)
            {
                return;
                // TODO: Add exception2
            }

            dbContext.Cars.Remove(carToDelete);
            await dbContext.SaveChangesAsync();
        }

        public async Task<ResponseCarDTO?> UpdateCarById(long id, UpdateCarDTO dto)
        {
            Car? result = await this.dbContext
                .Cars
                .Include(x => x.Garages)
                .FirstOrDefaultAsync(x => x.Id == id);

            ICollection<Garage> garages = await this.dbContext.Garages.Where(x => dto.GarageIds.Contains(x.Id)).ToListAsync();

            if (result == null)
            {
                return null;
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

        public async Task<ResponseCarDTO?> CreateCar(CreateCarDTO dto)
        {
            Car carToCreate = new()
            {
                Make = dto.Make,
                Model = dto.Model,
                ProductionYear = dto.ProductionYear,
                LicensePlate = dto.LicensePlate
            };

            Car? createdEntity = (await dbContext.Cars.AddAsync(carToCreate)).Entity;
            await dbContext.SaveChangesAsync();

            IEnumerable<ResponseGarageDTO> garagesDtos =
                createdEntity.Garages.Select(x => new ResponseGarageDTO(x.Id, x.Name, x.Location, x.City, x.Capacity));

            return new ResponseCarDTO(createdEntity.Id, createdEntity.Make, createdEntity.Model, createdEntity.ProductionYear,createdEntity.LicensePlate, garagesDtos);
        }

        public async Task<IEnumerable<ResponseCarDTO>> SearchCarsAsync(string carMake, int garageId, int fromYear, int toYear)
        {
            IEnumerable<ResponseCarDTO> cars = await dbContext.Cars
                .Include(x => x.Garages)
                .Where(x => x.Garages.Any(x => x.Id == garageId) &&
                            x.Make == carMake &&
                            x.ProductionYear >= fromYear &&
                            x.ProductionYear <= toYear)
                .Select(x => new ResponseCarDTO(x.Id, x.Make, x.Model, x.ProductionYear, x.LicensePlate, 
                    x.Garages.Select(y => new ResponseGarageDTO(y.Id, y.Name, y.Location, y.City, y.Capacity))))
                .ToListAsync();
     

            return cars;
        }

        private readonly CarsContext dbContext = dbContext;
    }
}
