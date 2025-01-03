﻿using Cars.Data.Models.Models;
using Cars.Data.Services.Exceptions;
using Cars.Data.Services.Interfaces;
using Cars.Web.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Cars.Data.Services.Implementations
{
    public class MaintenanceService(CarsContext dbContext) : IMaintenanceService
    {
        public async Task<ResponseMaintenanceDTO> Get(long id)
        {
            Maintenance result = await dbContext
                .Maintenances
                .Include(x => x.Car)
                .Include(x => x.Garage)
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new NotFoundException($"{nameof(Maintenance)} with {id} is not found");

            return new ResponseMaintenanceDTO(result.Id,
                result.Car.Id,
                result.Car?.Make,
                result.ServiceType,
                result.ScheduledDate.ToString(),
                result.GarageId,
                result.Garage?.Name);
        }

        public async Task<ResponseMaintenanceDTO> Update(long id, UpdateMaintenanceDTO dto)
        {
            Maintenance? result = await dbContext
                .Maintenances
                .Include(x => x.Car)
                .Include(x => x.Garage)
                .FirstOrDefaultAsync(x => x.Id == id) 
                ?? throw new NotFoundException($"{nameof(Maintenance)} with {id} is not found");

            result.CarId = dto.CarId;
            result.ServiceType = dto.ServiceType ?? result.ServiceType;
            result.ScheduledDate = dto.ScheduledDate != null ? DateTime.Parse(dto.ScheduledDate) : result.ScheduledDate;
            result.GarageId = dto.GarageId;

            await dbContext.SaveChangesAsync();

            return await Get(id);
        }


        public async Task Delete(long id)
        {
            Maintenance? maintenanceToDelete = await dbContext.Maintenances.FirstOrDefaultAsync(x => x.Id == id);

            if (maintenanceToDelete == null)
            {
                return;
                // TODO: Add exception
            }

            dbContext.Maintenances.Remove(maintenanceToDelete);
            await dbContext.SaveChangesAsync();
        }

        public async Task<ResponseMaintenanceDTO> Create(CreateMaintenanceDTO dto)
        {
            Maintenance maintenanceToCreate = new()
            {
                CarId = dto.CarId,
                ScheduledDate = DateTime.Parse(dto.ScheduledDate),
                GarageId = dto.GarageId,
                ServiceType = dto.ServiceType,
            };

            Maintenance? createdEntity = (await dbContext.Maintenances.AddAsync(maintenanceToCreate)).Entity;
            await dbContext.SaveChangesAsync();

            return await Get(createdEntity.Id);
        }

        public async Task<IEnumerable<ResponseMaintenanceDTO>> Search(string? startDate, string? endDate, long? carId, long? garageId)
        {
           IEnumerable<Maintenance> result = await dbContext
                .Maintenances
                .Include(x => x.Garage)
                .Include(x => x.Car)
                .Where(x => 
                    (string.IsNullOrEmpty(startDate) || x.ScheduledDate >= DateTime.Parse(startDate)) &&
                    (string.IsNullOrEmpty(endDate) || x.ScheduledDate <= DateTime.Parse(endDate)) &&
                    (!carId.HasValue || x.CarId == carId) &&
                    (!garageId.HasValue || x.GarageId == garageId)
                )
                .ToListAsync();

            if (result == null)
            {
                return [];
            }

            return result.Select(x => new ResponseMaintenanceDTO(
                x.Id,
                x.CarId,
                x.Car?.Model,
                x.ServiceType,
                x.ScheduledDate.ToString(),
                x.GarageId,
                x.Garage?.Name));
        }

        public async Task<List<MonthlyRequestsReportDTO>> GetReport(long garageId, DateTime? startDate, DateTime? endDate)
        {
            IEnumerable<Maintenance> maintenanceRequests = await dbContext.Maintenances
                .Where(request => request.GarageId == garageId &&
                       request.ScheduledDate >= startDate &&
                       request.ScheduledDate <= endDate)
                .ToListAsync();

            IEnumerable<MonthChunk> monthChunks = ChunkToMonths(startDate, endDate);

            List<MonthlyRequestsReportDTO> results = [];

            foreach (MonthChunk chunk in monthChunks)
            {
                long chunkStartDateTimestamp = chunk.Start.Ticks;
                long chunkEndDateTimestamp = chunk.End.Ticks;

                IEnumerable<Maintenance> requests = maintenanceRequests
                    .Where(request => request.ScheduledDate.Ticks >= chunkStartDateTimestamp && request.ScheduledDate.Ticks <= chunkEndDateTimestamp)
                    .ToList();

                results.Add(new MonthlyRequestsReportDTO
                {
                    YearMonth = $"{chunk.Year}-{chunk.Month}",
                    Requests = requests.Count()
                });
            }

            return results;
        }

        private static IEnumerable<MonthChunk> ChunkToMonths(DateTime? startDate, DateTime? endDate)
        {
            DateTime current = startDate != null ? new(startDate.Value.Year, startDate.Value.Month, 1) : DateTime.Now;

            while (current <= endDate)
            {
                yield return new MonthChunk
                {
                    Start = new DateTime(current.Year, current.Month, 1),
                    End = new DateTime(current.Year, current.Month, 1).AddMonths(1).AddDays(-1),
                    Month = current.ToString("MMMM").ToUpper(),
                    Year = current.Year,
                    MonthValue = current.Month - 1,
                    LeapYear = DateTime.IsLeapYear(current.Year)
                };

                current = current.AddMonths(1);
            }
        }

        private readonly CarsContext dbContext = dbContext;
        private class MonthChunk
        {
            public DateTime Start { get; set; }
            public DateTime End { get; set; }
            public string? Month { get; set; }
            public int Year { get; set; }
            public int MonthValue { get; set; }

            public bool LeapYear { get; set; }
        }
    }
}
