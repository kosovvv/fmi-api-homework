using CarsAPI.Dtos;
using CarsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CarsAPI.Services
{
    public class GarageService(CarsContext dbContext)
    {
        public async Task<ResponseGarageDTO> GetGarageById(long id)
        {
            Garage? result = await this.dbContext
               .Garages
               .Include(x => x.Cars)
               .FirstOrDefaultAsync(x => x.Id == id);

            if (result == null)
            {
                //to do
                return null;
            }

            return new ResponseGarageDTO(result.Id, result.Name, result.Location, result.City, result.Capacity);
        }

        public async Task<ResponseGarageDTO?> UpdateGarageById(long id, UpdateGarageDTO dto)
        {
            Garage? result = await this.dbContext
                .Garages
                .Include(x => x.Cars)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (result == null)
            {
                //throw
                return null;
            }

            result.Name = dto.Name ?? result.Name;
            result.Location = dto.Location ?? result.Location;
            result.Capacity = dto.Capacity;
            result.City = dto.City ?? result.Location;

            await dbContext.SaveChangesAsync();

            return new ResponseGarageDTO(result.Id, result.Name, result.Location, result.City, result.Capacity);
        }

        public async Task DeleteGarageById(long id)
        {
            Garage? garageToDelete = await dbContext.Garages.FirstOrDefaultAsync(x => x.Id == id);

            if (garageToDelete == null)
            {
                return;
                // TODO: Add exception
            }

            dbContext.Garages.Remove(garageToDelete);
            await dbContext.SaveChangesAsync();
        }

        private readonly CarsContext dbContext = dbContext;
    }
}
