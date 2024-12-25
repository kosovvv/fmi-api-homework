using System.ComponentModel.DataAnnotations;

namespace Cars.Web.ViewModels
{
    public class ResponseMaintenanceDTO(long id,
        long carId,
        string carName,
        string serviceType,
        string scheduledDate,
        long garageId,
        string garageName)
    {
        public long Id { get; set; } = id;

        public long CarId { get; set; } = carId;

        public string CarName { get; set; } = carName;

        public string ServiceType { get; set; } = serviceType;

        [DataType(DataType.Date)]
        public string ScheduledDate { get; set; } = scheduledDate;

        public long GarageId { get; set; } = garageId;

        public string GarageName { get; set; } = garageName;
    }
}
