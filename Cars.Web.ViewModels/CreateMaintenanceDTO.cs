using System.ComponentModel.DataAnnotations;

namespace Cars.Web.ViewModels
{
    public class CreateMaintenanceDTO
    {
        [Required]
        public long GarageId { get; set; }

        [Required]
        public long CarId { get; set; }

        [Required]
        public string ServiceType { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public string ScheduledDate { get; set; }
    }
}
