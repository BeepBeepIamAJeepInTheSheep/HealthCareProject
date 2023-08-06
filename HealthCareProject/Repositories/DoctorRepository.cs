using HealthCareProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCareProject.Repositories
{
    public class DoctorRepository : IRepositories<DoctorModelCLass>, IGetRepositories<DoctorModelCLass>, IDoctorRepository<PatientReport>, IDoctorGetRepository<AppointmentsModelClass>, IGetDoctorUsingRole<UserModelClass>, IGetUserdetailsUsingId<UserModelClass>, IGetDoctorDetailsUsingUserId<DoctorModelCLass>
    {
        private readonly ApplicationDbContext _dbContext;
        public DoctorRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Add new doctor details
        public async Task Create(DoctorModelCLass obj)
        {
            if (obj != null)
            {
                _dbContext.Doctors.Add(obj);
                await _dbContext.SaveChangesAsync();

            }
        }

        //Creating a new report for the patient
        public async Task<PatientReport> CreateReport(PatientReport obj)
        {
            if (obj != null)
            {
                _dbContext.patientReports.Add(obj);
                await _dbContext.SaveChangesAsync();
                return obj;
            }
            else { return null; }
        }



        //Remove doctor details 
        public async Task<DoctorModelCLass> Delete(int id)
        {
            var doctorInDb = await _dbContext.Doctors.FindAsync(id);
            if (doctorInDb != null)
            {
                _dbContext.Doctors.Remove(doctorInDb);
                await _dbContext.SaveChangesAsync();

                return doctorInDb;
            }
            return null;
        }

        //To get all the doctors
        public async Task<IEnumerable<DoctorModelCLass>> GetAll()
        {
            var doctors = await _dbContext.Doctors.ToListAsync();
            return doctors;
        }

        //To get all the doctors using role
        public async Task<IEnumerable<UserModelClass>> GetAllDoctorsUsingRole()
        {
            var doctors = await _dbContext.Users.Where(x => x.Role == "Doctor").ToListAsync();
            return doctors;
        }



        //Get all the appointments of the doctor 
        public async Task<IEnumerable<AppointmentsModelClass>> GetAppointmentByDoctorId(int id)
        {
            var appointments = await _dbContext.Appointment.Where(x => x.DoctorsId == id && x.Status == "APPROVED").ToListAsync();
            if (appointments != null)
            {
                return appointments;
            }
            else { return null; }
        }



        //get doctors by their id
        public async Task<DoctorModelCLass> GetById(int id)
        {
            var doctor = await _dbContext.Doctors.FirstOrDefaultAsync(x => x.Id == id);
            if (doctor != null)
            {
                return doctor;
            }
            return null;
        }

        //To get doctor details using userid
        public async Task<DoctorModelCLass> GetDoctorDetailsUsingUserId(int id)
        {
            return await _dbContext.Doctors.FirstOrDefaultAsync(x => x.UserId == id);
            
        }

        public async Task<UserModelClass> GetUserDetailsUsingId(int id)
        {
            var userDetails = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            return userDetails;
        }

        //Update doctor details
        public async Task<DoctorModelCLass> Update(int id, DoctorModelCLass obj)
        {
            var doctorInDb = await _dbContext.Doctors.FindAsync(id);
            if (doctorInDb != null)
            {
                doctorInDb.Specialization = obj.Specialization;
                doctorInDb.Age = obj.Age;
                doctorInDb.Gender = obj.Gender;
                doctorInDb.Education = obj.Education;
                doctorInDb.Fees = obj.Fees;
                doctorInDb.Experience = obj.Experience;

                _dbContext.Doctors.Update(doctorInDb);
                await _dbContext.SaveChangesAsync();
                return doctorInDb;
            }
            return null;

        }

        //Update report details
        public async Task<PatientReport> UpdateReport(int id, PatientReport obj)
        {
            var report = await _dbContext.patientReports.FindAsync(id);
            if (report != null)
            {
                report.DoctorName = obj.DoctorName;
                report.Medicines = obj.Medicines;
                report.UserId = obj.UserId;
                report.Diognosis = obj.Diognosis;
                report.Symptoms = obj.Symptoms;

                _dbContext.patientReports.Update(report);
                await _dbContext.SaveChangesAsync();
                return report;
            }
            return null;
        }


    }
}
