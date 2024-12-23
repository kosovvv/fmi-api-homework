﻿using System.ComponentModel.DataAnnotations;

namespace CarsAPI.Dtos
{
    public class MonthlyRequestsReportDTO
    {
        public string? YearMonth { get; set; }

        public int Requests { get; set; }
    }
}
