using System.ComponentModel.DataAnnotations;

namespace HealthCareProject.Models
{
    public class PatientDetails
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string BloodGroup { get; set; }

        public double Height { get; set; }

        public double Weight { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public UserModelClass Users { get; set; }
        public int UsersId { get; set; }


    }
}
