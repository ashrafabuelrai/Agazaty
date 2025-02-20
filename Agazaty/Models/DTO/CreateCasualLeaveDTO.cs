using System.ComponentModel.DataAnnotations;

namespace Agazaty.Models.DTO
{
    public class CreateCasualLeaveDTO
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateOnly Year { get; set; }
        [Required]
        public string UserId { get; set; }

    }
}
