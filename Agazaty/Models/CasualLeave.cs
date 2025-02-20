using System.ComponentModel.DataAnnotations;

namespace Agazaty.Models
{
    public class CasualLeave
    {
        [Key]
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateOnly Year { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}
