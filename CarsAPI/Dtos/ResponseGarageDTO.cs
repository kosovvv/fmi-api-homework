namespace CarsAPI.Dtos
{
    public class ResponseGarageDTO
    {
        public required long Id { get; set; }

        public required string Name { get; set; }

        public required string Location { get; set; }

        public required string City { get; set; }

        public required int Capacity { get; set; }
    }
}
