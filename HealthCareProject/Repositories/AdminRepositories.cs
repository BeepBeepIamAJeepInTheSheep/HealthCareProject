using HealthCareProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCareProject.Repositories
{
    public class AdminRepositories : IAdminRepository<AppointmentsModelClass>, IGetRepositories<AppointmentsModelClass>, IGetRepositories<UserModelClass>
    {
        private readonly ApplicationDbContext _dbContext;
        public AdminRepositories(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AppointmentsModelClass> Approve(int id)
        {
            var approveAppointment = await _dbContext.Appointment.FindAsync(id);
            if (approveAppointment != null)
            {
                approveAppointment.Status = "APPROVED";
                _dbContext.Appointment.Update(approveAppointment);
                await _dbContext.SaveChangesAsync();
                return approveAppointment;
            }
            return null;
        }

        public async Task<IEnumerable<AppointmentsModelClass>> GetAll()
        {
            var appointments = await _dbContext.Appointment.ToListAsync();
            return appointments;
        }

        public async Task<IEnumerable<AppointmentsModelClass>> GetAllApproved()
        {
            var approved = await _dbContext.Appointment.Where(h => h.Status == "APPROVED").ToListAsync();
            return approved;
        }

        public async Task<IEnumerable<AppointmentsModelClass>> GetAllPendings()
        {
            var pendings = await _dbContext.Appointment.Where(h => h.Status == null || h.Status == "PENDING").ToListAsync();
            return pendings;
        }

        public async Task<IEnumerable<AppointmentsModelClass>> GetAllRejected()
        {
            var rejected = await _dbContext.Appointment.Where(h => h.Status == "REJECTED").ToListAsync();
            return rejected;
        }

        //Get appointment by id
        public async Task<AppointmentsModelClass> GetById(int id)
        {
            var Appointment = await _dbContext.Appointment.FindAsync(id);
            if (Appointment != null)
            {
                return Appointment;
            }
            return null;
        }

        public async Task<AppointmentsModelClass> Reject(int id)
        {

            var rejectAppointment = await _dbContext.Appointment.FindAsync(id);
            if (rejectAppointment != null)
            {
                rejectAppointment.Status = "REJECTED";
                _dbContext.Appointment.Update(rejectAppointment);
                await _dbContext.SaveChangesAsync();

                return rejectAppointment;
            }
            return null;
        }

        async Task<IEnumerable<UserModelClass>> IGetRepositories<UserModelClass>.GetAll()
        {
            var users = await _dbContext.Users.ToListAsync();
            return users;
        }

        Task<UserModelClass> IGetRepositories<UserModelClass>.GetById(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
