using System.ComponentModel.DataAnnotations;

namespace API_propia.Data_Models
{
    public class Rooms : BaseEntity
    {   
        [Required, StringLength(50)]
        public string Category { get; set; } = string.Empty;
        
        [Required]
        public int CostPerNight { get; set; }
        
        [Required]
        public bool Available { get; set; }
    }
}
