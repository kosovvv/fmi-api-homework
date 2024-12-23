using CarsAPI.Dtos;
using CarsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CarsAPI.Services
{
    public class MaintenanceService(CarsContext dbContext)
    {
        public async Task<ResponseMaintenanceDTO?> GetMaintenanceById(long id)
        {
            Maintenance? result = await this.dbContext
                .Maintenances
                .Include(x => x.Car)
                .Include(x => x.Garage)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (result == null)
            {
                return null;
            }

            return new ResponseMaintenanceDTO(result.Id,
                result.Car.Id,
                result.Car.Make,
                result.ServiceType,
                result.ScheduledDate.ToString(),
                result.GarageId,
                result.Garage.Name);
        }

        public async Task<ResponseMaintenanceDTO?> UpdateMaintenanceById(long id, UpdateMaintenanceDTO dto)
        {
            Maintenance? result = await this.dbContext
                .Maintenances
                .Include(x => x.Car)
                .Include(x => x.Garage)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (result == null)
            {
                return null;
            }

            result.CarId = dto.CarId;
            result.ServiceType = dto.ServiceType ?? result.ServiceType;
            result.ScheduledDate = dto.ScheduledDate != null ? DateTime.Parse(dto.ScheduledDate) : result.ScheduledDate;
            result.GarageId = dto.GarageId;

            await dbContext.SaveChangesAsync();

            return new ResponseMaintenanceDTO(result.Id,
                result.Car.Id,
                result.Car.Make,
                result.ServiceType,
                result.ScheduledDate.ToString(),
                result.GarageId,
                result.Garage.Name);
        }


        public async Task DeleteMaintenanceById(long id)
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

        public async Task<ResponseMaintenanceDTO?> CreateMaintenanceById(CreateMaintenanceDTO dto)
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

            return new ResponseMaintenanceDTO(createdEntity.Id, 
                createdEntity.CarId, 
                createdEntity.Car.Model, 
                createdEntity.ServiceType, 
                createdEntity.ScheduledDate.ToString(), 
                createdEntity.GarageId, 
                createdEntity.Garage.Name);
        }

        public async Task<IEnumerable<ResponseMaintenanceDTO>> GetMaintenanceByQueryParams(QueryParams queryParams)
        {
            IEnumerable<Maintenance> result = await ApplyFilters(this.dbContext.Maintenances, queryParams).ToListAsync();

            if (result == null)
            {
                return [];
            }

            return result.Select(x => new ResponseMaintenanceDTO(
                x.Id,
                x.CarId,
                x.Car.Model,
                x.ServiceType,
                x.ScheduledDate.ToString(),
                x.GarageId,
                x.Garage.Name));
        }

        public async Task<MonthlyRequestsReportDTO> GetMonthlyReport(QueryParams queryParams)
        {
            IEnumerable<Maintenance> result = await ApplyFilters(this.dbContext.Maintenances, queryParams).ToListAsync();

            if (result == null)
            {
                return new MonthlyRequestsReportDTO()
                {
                    Requests = 0,
                    YearMonth = queryParams.StartDate,
                };
            }

            return new MonthlyRequestsReportDTO()
            {
                Requests = result.Count(),
                YearMonth = queryParams.StartDate,
            };
        }


        private static IQueryable<Maintenance> ApplyFilters(IQueryable<Maintenance> query, QueryParams queryParams)
        {
            return query
                .Where(x => string.IsNullOrEmpty(queryParams.StartDate) || x.ScheduledDate >= DateTime.Parse(queryParams.StartDate))
                .Where(x => string.IsNullOrEmpty(queryParams.EndDate) || x.ScheduledDate <= DateTime.Parse(queryParams.EndDate))
                .Where(x => !queryParams.CarId.HasValue || x.CarId == queryParams.CarId)
                .Where(x => !queryParams.GarageId.HasValue || x.GarageId == queryParams.GarageId);
        }

        private readonly CarsContext dbContext = dbContext;
    }
}
