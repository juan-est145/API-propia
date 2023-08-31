using System.ComponentModel.DataAnnotations;

namespace API_propia.Data_Models
{
    public class Users : BaseEntity
    {
        [Required, StringLength(50)]
        public string UserName { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string EmailAddress { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
