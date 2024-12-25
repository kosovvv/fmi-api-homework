using Cars.Data.Models.Models;
using Cars.Data.Services.Exceptions;
using Cars.Data.Services.Interfaces;
using Cars.Web.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Cars.Data.Services.Implementations
{
    public class GarageService(CarsContext dbContext) : IGarageService
    {
        public async Task<ResponseGarageDTO> Get(long id)
        {
            Garage? result = await dbContext
               .Garages
               .Include(x => x.Cars)
               .FirstOrDefaultAsync(x => x.Id == id) 
               ?? throw new NotFoundException($"{nameof(Garage)} with {id} is not found");

            return new ResponseGarageDTO(result.Id, result.Name, result.Location, result.City, result.Capacity);
        }

        public async Task<ResponseGarageDTO> Update(long id, UpdateGarageDTO dto)
        {
            Garage? result = await dbContext
                .Garages
                .Include(x => x.Cars)
                .FirstOrDefaultAsync(x => x.Id == id) 
                ?? throw new NotFoundException($"{nameof(Garage)} with {id} is not found");

            result.Name = dto.Name ?? result.Name;
            result.Location = dto.Location ?? result.Location;
            result.Capacity = dto.Capacity;
            result.City = dto.City ?? result.Location;

            await dbContext.SaveChangesAsync();

            return await Get(id);
        }

        public async Task Delete(long id)
        {
            Garage? garageToDelete = await dbContext
                .Garages
                .FirstOrDefaultAsync(x => x.Id == id) 
                ?? throw new NotFoundException($"{nameof(Garage)} with {id} is not found");

            dbContext.Garages.Remove(garageToDelete);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ResponseGarageDTO>> Search(string? city)
        {
            IEnumerable<Garage> result = await dbContext
               .Garages
               .Where(x => string.IsNullOrWhiteSpace(city) || x.City.Contains(city))
               .ToListAsync()
               ?? throw new NotFoundException($"{nameof(Garage)}'s not found");

            return result.Select(x => new ResponseGarageDTO(x.Id, x.Name, x.Location, x.City, x.Capacity));
        }

        public async Task<ResponseGarageDTO> Create(CreateGarageDTO dto)
        {
            Garage garageToCreate = new()
            {
                Name = dto.Name,
                City = dto.City,
                Location = dto.Location,
                Capacity = dto.Capacity
            };

            Garage createdEntity = (await dbContext.Garages.AddAsync(garageToCreate)).Entity;
            await dbContext.SaveChangesAsync();

            return new ResponseGarageDTO(createdEntity.Id,
                createdEntity.Name,
                createdEntity.Location,
                createdEntity.City,
                createdEntity.Capacity);
        }

        public async Task<IEnumerable<GarageDailyAvailabilityReportDTO>> GetReport(long garageId, DateTime? startDate, DateTime? endDate)
        {
            IEnumerable<Maintenance> maintenanceRequests = await dbContext
                .Maintenances
                .Where(request => request.GarageId == garageId &&
                       request.ScheduledDate >= startDate &&
                       request.ScheduledDate <= endDate)
                .ToListAsync();

            IEnumerable<DayChunk> daysChunk = ChunkToDays(startDate, endDate);

            List<GarageDailyAvailabilityReportDTO> results = [];

            ResponseGarageDTO garage = await Get(garageId);

            foreach (DayChunk chunk in daysChunk)
            {
                long chunkStartDateTimestamp = chunk.Start.Ticks;
                long chunkEndDateTimestamp = chunk.End.Ticks;

                IEnumerable<Maintenance> requests = maintenanceRequests.Where(request =>
                {
                    long requestDateTimestamp = request.ScheduledDate.Ticks;
                    return requestDateTimestamp >= chunkStartDateTimestamp && requestDateTimestamp <= chunkEndDateTimestamp;
                });

                results.Add(new GarageDailyAvailabilityReportDTO
                {
                    Date = chunk.Start.ToString("yyyy-MM-dd"),
                    Requests = requests.Count(),
                    AvailableCapacity = garage.Capacity - requests.Count()
                });
            }

            return results;
        }

        private static IEnumerable<DayChunk> ChunkToDays(DateTime? startDate, DateTime? endDate)
        {
            DateTime current = startDate ?? DateTime.Now;

            while (current <= endDate)
            {
                yield return new DayChunk
                {
                    Start = current.Date,
                    End = current.Date.AddDays(1).AddMilliseconds(-1)
                };

                current = current.AddDays(1);
            }
        }

        private readonly CarsContext dbContext = dbContext;
        private class DayChunk
        {
            public DateTime Start { get; set; }
            public DateTime End { get; set; }
        }
    }
}
