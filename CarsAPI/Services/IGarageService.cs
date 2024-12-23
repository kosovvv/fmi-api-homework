using CarsAPI.Dtos;
using CarsAPI.Models;

namespace CarsAPI.Services
{
    public interface IGarageService
    {
        Task<ResponseGarageDTO?> GetGarageById(long id);
        Task<ResponseGarageDTO?> UpdateGarageById(long id, UpdateGarageDTO dto);
        Task DeleteGarageById(long id);
        Task<IEnumerable<ResponseGarageDTO>> GetGaragesByCity(string? city);
        Task<ResponseGarageDTO?> CreateGarage(CreateGarageDTO dto);
        Task<IEnumerable<GarageDailyAvailabilityReportDTO>> GetDailyAvailabilityReport(long garageId, DateTime startDate, DateTime endDate);
    }
}
