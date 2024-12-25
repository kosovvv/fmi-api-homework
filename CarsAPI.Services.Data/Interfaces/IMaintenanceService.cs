using Cars.Web.ViewModels;

namespace Cars.Data.Services.Interfaces
{
    public interface IMaintenanceService
    {
        Task<ResponseMaintenanceDTO> Get(long id);
        Task<ResponseMaintenanceDTO> Update(long id, UpdateMaintenanceDTO dto);
        Task Delete(long id);
        Task<ResponseMaintenanceDTO> Create(CreateMaintenanceDTO dto);
        Task<IEnumerable<ResponseMaintenanceDTO>> Search(string? startDate, string? endDate, long? carId, long? garageId);
        Task<List<MonthlyRequestsReportDTO>> GetReport(long garageId, DateTime? startDate, DateTime? endDate);
    }
}
