﻿using System.ComponentModel.DataAnnotations;

namespace Cars.Web.ViewModels
{
    public class GarageDailyAvailabilityReportDTO
    {
        public GarageDailyAvailabilityReportDTO()
        {

        }
        public GarageDailyAvailabilityReportDTO(string date, int requests, int avaliableCapacity)
        {
            Date = date;
            Requests = requests;
            AvailableCapacity = avaliableCapacity;
        }

        [DataType(DataType.Date)]
        public string Date { get; set; }

        public int Requests { get; set; }

        public int AvailableCapacity { get; set; }
    }
}
