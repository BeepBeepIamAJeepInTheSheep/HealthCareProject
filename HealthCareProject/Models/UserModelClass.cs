using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthCareProject.Models
{
    public class UserModelClass : UserLogin
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public string Role  { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set;}

    }

    public class UserLogin
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [NotMapped]
        public string Password { get; set; }
    }
}
