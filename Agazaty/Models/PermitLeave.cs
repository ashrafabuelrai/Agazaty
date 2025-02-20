using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Agazaty.Models
{
    public class PermitLeave
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string EmployeeNationalNumber { get; set; }
        public double Hours { get; set; }
        [Required]
        public DateOnly Date { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public List<PermitLeaveImage> PermitLeaveImages { get; set; }
    }
}
