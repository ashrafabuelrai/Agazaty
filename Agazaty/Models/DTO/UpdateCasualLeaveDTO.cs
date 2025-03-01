﻿using System.ComponentModel.DataAnnotations;

namespace Agazaty.Models.DTO
{
    public class UpdateCasualLeaveDTO
    {
        [Required]
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}
