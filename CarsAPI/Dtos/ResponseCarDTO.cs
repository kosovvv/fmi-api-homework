namespace CarsAPI.Dtos
{
    public class ResponseCarDTO
    {
        public ResponseCarDTO()
        {
            this.Garages = [];
        }

        public long Id { get; set; }

        public string? Make { get; set; }

        public string? Model { get; set; }

        public int ProductionYear { get; set; }

        public string? LicensePlate { get; set; }

        public ICollection<ResponseGarageDTO> Garages { get; set; }
    }
}
