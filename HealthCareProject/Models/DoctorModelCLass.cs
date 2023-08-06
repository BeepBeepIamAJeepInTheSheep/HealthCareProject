using System.ComponentModel.DataAnnotations;

namespace HealthCareProject.Models
{
    public class DoctorModelCLass
    {
        public int Id { get; set; }
        [Required]
        public string Specialization { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string Education { get; set; }
        [Required]
        public string Experience { get; set; }
        
        public int Fees { get; set; }
        [Required]
        public string Gender { get; set; }
        public UserModelClass User { get; set; }
        public int UserId { get; set; }


    }
}
