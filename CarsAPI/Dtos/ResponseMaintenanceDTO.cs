using System.ComponentModel.DataAnnotations;

namespace CarsAPI.Dtos
{
    public class ResponseMaintenanceDTO
    {
        public long Id { get; set; }

        public long CarId { get; set; }

        public string? CarName { get; set; }

        public string? ServiceType { get; set; }

        [DataType(DataType.Date)]
        public string? ScheduledDate { get; set; }

        public long GarageId { get; set; }

        public string? GarageName { get; set; }
    }
}
