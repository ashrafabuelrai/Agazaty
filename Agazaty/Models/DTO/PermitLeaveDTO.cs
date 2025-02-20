﻿using System.ComponentModel.DataAnnotations;

namespace Agazaty.Models.DTO
{
    public class PermitLeaveDTO
    {
        [Required]
        public string EmployeeNationalNumber { get; set; }
        public double Hours { get; set; }
        [Required]
        public DateOnly Date { get; set; }
        [Required]
        public string UserId { get; set; }

    }
}
