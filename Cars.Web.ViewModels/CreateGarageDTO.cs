namespace Cars.Web.ViewModels
{
    public class CreateGarageDTO
    {
        public required string Name { get; set; }

        public required string Location { get; set; }

        public required string City { get; set; }

        public int Capacity { get; set; }
    }
}
