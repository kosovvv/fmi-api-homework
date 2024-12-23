using CarsAPI.Dtos;

namespace CarsAPI.Services
{
    public interface IMaintenanceService
    {
        Task<ResponseMaintenanceDTO?> GetMaintenanceById(long id);
        Task<ResponseMaintenanceDTO?> UpdateMaintenanceById(long id, UpdateMaintenanceDTO dto);
        Task DeleteMaintenanceById(long id);
        Task<ResponseMaintenanceDTO?> CreateMaintenance(CreateMaintenanceDTO dto);
        Task<IEnumerable<ResponseMaintenanceDTO>> GetMaintenanceByQueryParams(string? startDate, string? endDate, long? carId, long? garageId);
        Task<List<MonthlyRequestsReportDTO>> GetMonthlyRequestsReportAsync(long garageId, DateTime startDate, DateTime endDate);
    }
}
