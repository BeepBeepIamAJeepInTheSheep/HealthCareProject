namespace HealthCareProject.Models
{
    public class PatientReport
    {
        public int Id { get; set; }
        public string DoctorName { get; set; }
        public string Symptoms { get; set; }
        public string Diognosis { get; set; }
        public string Medicines { get; set; }
        public UserModelClass User { get; set; }
        public int UserId { get; set; }

    }
}
