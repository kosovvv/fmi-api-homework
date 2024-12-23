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

        public async Task<IEnumerable<ResponseGarageDTO>> GetGaragesByCity(string city)
        {
            IEnumerable<Garage> result = await this.dbContext
               .Garages
               .Include(x => x.Cars)
               .Where(x => x.City == city)
               .ToListAsync();

            if (result == null)
            {
                //to do
                return null;
            }

            return result.Select(x => new ResponseGarageDTO(x.Id, x.Name, x.Location, x.City, x.Capacity));
        }

        public async Task<ResponseGarageDTO?> CreateGarage(CreateGarageDTO dto)
        {
            Garage garageToCreate = new()
            {
                Name = dto.Name,
                City = dto.City,
                Location = dto.Location,
                Capacity = dto.Capacity // can be set to 0
            };

            Garage? createdEntity = (await dbContext.Garages.AddAsync(garageToCreate)).Entity;
            await dbContext.SaveChangesAsync();

            return new ResponseGarageDTO(createdEntity.Id,
                createdEntity.Name,
                createdEntity.Location,
                createdEntity.City, 
                createdEntity.Capacity);
        }

        public async Task<IEnumerable<GarageDailyAvailabilityReportDTO>> GetDailyAvailabilityReport(Garage garage, DateTime startDate, DateTime endDate)
        {
            IEnumerable<Maintenance> maintenanceRequests = await dbContext.Maintenances
                .Where(request => request.GarageId == garage.Id && 
                       request.ScheduledDate >= startDate && 
                       request.ScheduledDate <= endDate)
                .ToListAsync();

            IEnumerable<DayChunk> daysChunk = DateUtils.ChunkToDays(startDate, endDate);

            List<GarageDailyAvailabilityReportDTO> results = [];

            foreach (DayChunk chunk in daysChunk)
            {
                long chunkStartDateTimestamp = chunk.Start.Ticks;
                long chunkEndDateTimestamp = chunk.End.Ticks;

                IEnumerable<Maintenance> requests = maintenanceRequests.Where(request =>
                {
                    var requestDateTimestamp = request.ScheduledDate.Ticks;
                    return requestDateTimestamp >= chunkStartDateTimestamp && requestDateTimestamp <= chunkEndDateTimestamp;
                });

                results.Add(new GarageDailyAvailabilityReportDTO
                {
                    Date = chunk.Start.ToString("yyyy-MM-dd"),
                    Requests = requests.Count(),
                    AvaliableCapacity = garage.Capacity - requests.Count()
                });
            }

            return results;
        }

        private readonly CarsContext dbContext = dbContext;
    }
}
