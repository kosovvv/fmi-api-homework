namespace Cars.Web.ViewModels
{
    public class ResponseCarDTO(long id,
        string? make,
        string? model,
        int productionYear,
        string? licensePlate,
        IEnumerable<ResponseGarageDTO> garages)
    {
        public long Id { get; set; } = id;

        public string? Make { get; set; } = make;

        public string? Model { get; set; } = model;

        public int ProductionYear { get; set; } = productionYear;

        public string? LicensePlate { get; set; } = licensePlate;

        public IEnumerable<ResponseGarageDTO> Garages { get; set; } = garages;
    }
}
