using HealthCareProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCareProject.Repositories
{
    public class PatientRepositories : IRepositories<PatientDetails>, IRepositories<AppointmentsModelClass>, IPatientGetRepository<PatientReport>, IGetUserDEtailsUsingEmail<UserModelClass>, IGetRepositories<PatientDetails>, IGetPatientsAllReports<PatientReport>, IGetMyAppointments<AppointmentsModelClass>

    {
        private readonly ApplicationDbContext _dbContext;
        public PatientRepositories(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Adding new patient details
        public async Task Create(PatientDetails obj)
        {
            if (obj != null)
            {
                _dbContext.PatientDetails.Add(obj);
                await _dbContext.SaveChangesAsync();
            }
        }

        //Creating new appointment 
        public async Task Create(AppointmentsModelClass obj)
        {
            if (obj != null)
            {
                _dbContext.Appointment.Add(obj);
                await _dbContext.SaveChangesAsync();
            }
        }

        //To delete patient details
        public Task<PatientDetails> Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        //To get all patient details
        public async Task<IEnumerable<PatientDetails>> GetAll()
        {
            return await _dbContext.PatientDetails.ToListAsync();
        }

        //To get all the reports of the patient
        public async Task<IEnumerable<PatientReport>> GetAllPatientsReports(int id)
        {
            var res = await _dbContext.patientReports.Where(x => x.UserId == id).ToListAsync();
            return res;

        }

        //To get patient details using patient id
        public async Task<PatientDetails> GetById(int id)
        {
            var res = await _dbContext.PatientDetails.FirstOrDefaultAsync(x => x.UsersId == id);
            return res;
        }

        public async Task<IEnumerable<AppointmentsModelClass>> GetMyAppointments(int id)
        {
            var res = await _dbContext.Appointment.Where(x => x.UserId == id).ToListAsync();
            return res;
        }


        //To get Patient report
        public async Task<PatientReport> GetReportById(int id)
        {
            var report = await _dbContext.patientReports.FirstOrDefaultAsync(x => x.UserId == id);
            if (report != null)
            {
                return report;
            }
            return null;

        }

        //To get user details using email
        public async Task<UserModelClass> GetUserDetailsUsingEmail(string email)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (user != null)
            {
                return user;
            }
            return null;
        }

        //Updating patient details
        public async Task<PatientDetails> Update(int id, PatientDetails obj)
        {
            var patientInDb = await _dbContext.PatientDetails.FirstOrDefaultAsync(x => x.UsersId == id);
            if (patientInDb != null)
            {
                patientInDb.Age = obj.Age;
                patientInDb.UsersId = obj.UsersId;
                patientInDb.BloodGroup = obj.BloodGroup;
                patientInDb.Address = obj.Address;
                patientInDb.Gender = obj.Gender;
                patientInDb.Height = obj.Height;
                patientInDb.Weight = obj.Weight;

                _dbContext.PatientDetails.Update(patientInDb);
                await _dbContext.SaveChangesAsync();
                return patientInDb;
            }
            return null;
        }

        //Updating Appointment details
        public async Task<AppointmentsModelClass> Update(int id, AppointmentsModelClass obj)
        {
            var appointment = await _dbContext.Appointment.FirstOrDefaultAsync(x => x.Id == id);
            if (appointment != null)
            {
                appointment.Description = obj.Description;
                appointment.Status = obj.Status;
                appointment.DoctorsId = obj.DoctorsId;

                _dbContext.Update(appointment);
                await _dbContext.SaveChangesAsync();
                return appointment;
            }
            return null;
        }

        //Deleting Appointment
        async Task<AppointmentsModelClass> IRepositories<AppointmentsModelClass>.Delete(int id)
        {
            var appointment = await _dbContext.Appointment.FirstOrDefaultAsync(x => x.Id == id);
            if (appointment != null)
            {
                _dbContext.Appointment.Remove(appointment);
                await _dbContext.SaveChangesAsync();
                return appointment;
            }
            return null;
        }


    }
}
