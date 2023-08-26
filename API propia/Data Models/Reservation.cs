using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

namespace API_propia.Data_Models
{
    public class Reservation : BaseEntity
    {
        [Required]
        public int NumberofGuests { get; set; }

        [Required]
        public int TotalNights { get; set; }

        [Required]
        public bool Breakfast { get; set; }
        
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public int PhoneNumber { get; set; }
        
        [Required, StringLength(40)]
        public string Name { get; set; } = string.Empty;
        
        [Required, StringLength(60)]
        public string Surname { get; set; } = string.Empty;

        public ICollection<Rooms> Rooms = new List<Rooms>(); 
    }
}
