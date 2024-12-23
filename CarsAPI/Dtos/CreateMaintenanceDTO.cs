using System.ComponentModel.DataAnnotations;

namespace CarsAPI.Dtos
{
    public class CreateMaintenanceDTO
    {
        public required long GarageId { get; set; }

        public required long CarId { get; set; }

        public required string ServiceType { get; set; }

        [DataType(DataType.Date)]
        public required string ScheduledDate { get; set; }
    }
}
