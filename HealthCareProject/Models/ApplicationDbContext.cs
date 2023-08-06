using Microsoft.EntityFrameworkCore;
namespace HealthCareProject.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<UserModelClass> Users { get; set; }
        public DbSet<PatientDetails> PatientDetails { get; set; }
        public DbSet<DoctorModelCLass> Doctors { get; set; }
        public DbSet<AppointmentsModelClass> Appointment { get; set; }
        public DbSet<PatientReport> patientReports { get; set; }
    }
}
