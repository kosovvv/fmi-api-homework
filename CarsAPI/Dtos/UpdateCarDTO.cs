namespace CarsAPI.Dtos
{
    public class UpdateCarDTO
    {
        public UpdateCarDTO()
        {
            this.GarageIds = [];
        }

        public string? Make { get; set; }

        public string? Model { get; set; }

        public int ProductionYear { get; set; }

        public string? LicensePlate { get; set; }

        public ICollection<long> GarageIds { get; set; }

    }
}
