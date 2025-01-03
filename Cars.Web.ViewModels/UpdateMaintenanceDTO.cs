﻿using System.ComponentModel.DataAnnotations;

namespace Cars.Web.ViewModels
{
    public class UpdateMaintenanceDTO
    {
        public long CarId { get; set; }

        public string ServiceType { get; set; }

        [DataType(DataType.Date)]
        public string ScheduledDate { get; set; }

        [Required]
        public long GarageId { get; set; }
    }
}
