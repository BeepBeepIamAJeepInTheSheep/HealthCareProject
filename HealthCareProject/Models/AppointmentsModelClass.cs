using System;
using System.ComponentModel.DataAnnotations;

namespace HealthCareProject.Models
{
    public class AppointmentsModelClass
    {
        [Required]
        public int Id { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public DateTime AppointmentDate { get; set; }
        public DoctorModelCLass Doctors { get; set; }
        public int DoctorsId { get; set; }
        public UserModelClass User { get; set; }
        public int UserId { get; set; }
    }
}
