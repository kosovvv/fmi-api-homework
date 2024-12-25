using Cars.Web.ViewModels;

namespace Cars.Data.Services.Interfaces
{
    public interface ICarService
    {
        Task<ResponseCarDTO> Get(long id);
        Task Delete(long id);
        Task<ResponseCarDTO> Update(long id, UpdateCarDTO dto);
        Task<ResponseCarDTO> Create(CreateCarDTO dto);
        Task<IEnumerable<ResponseCarDTO>> Search(string? carMake, long? garageId, int? fromYear, int? toYear);
    }
}
