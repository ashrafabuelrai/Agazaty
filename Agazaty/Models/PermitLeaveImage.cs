using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Agazaty.Models
{
    public class PermitLeaveImage
    {
        public int Id { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [ForeignKey("PermitLeave")]
        public int LeaveId { get; set; }
        public PermitLeave PermitLeave { get; set; }
    }
}
