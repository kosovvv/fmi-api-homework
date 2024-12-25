using Cars.Web.ViewModels;

namespace Cars.Data.Services.Interfaces
{
    public interface IGarageService
    {
        Task<ResponseGarageDTO> Get(long id);
        Task<ResponseGarageDTO> Update(long id, UpdateGarageDTO dto);
        Task Delete(long id);
        Task<IEnumerable<ResponseGarageDTO>> Search(string? city);
        Task<ResponseGarageDTO> Create(CreateGarageDTO dto);
        Task<IEnumerable<GarageDailyAvailabilityReportDTO>> GetReport(long garageId, DateTime? startDate, DateTime? endDate);
    }
}
