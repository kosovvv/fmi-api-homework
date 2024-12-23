namespace CarsAPI.Dtos
{
    public class ResponseCarDTO
    {
        public ResponseCarDTO(long id, 
            string? make, 
            string? model, 
            int productionYear, 
            string? licensePlate, 
            IEnumerable<ResponseGarageDTO> garages)
        {
            Id = id;
            Make = make;
            Model = model;
            ProductionYear = productionYear;
            LicensePlate = licensePlate;
            Garages = garages;
        }

        public long Id { get; set; }

        public string? Make { get; set; }

        public string? Model { get; set; }

        public int ProductionYear { get; set; }

        public string? LicensePlate { get; set; }

        public IEnumerable<ResponseGarageDTO> Garages { get; set; }
    }
}
