using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using HealthCareProject.Models;

namespace HealthCareProject.Repositories
{

    //Manipulation operations
    public interface IRepositories<T> where T : class
    {
        Task Create(T obj);
        Task<T> Update(int id, T obj);
        Task<T> Delete(int id);
    }

    //Retrieving data 
    public interface IGetRepositories<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
    }

    //Admin operations
    public interface IAdminRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllPendings();
        Task<IEnumerable<T>> GetAllApproved();
        Task<IEnumerable<T>> GetAllRejected();
        Task<T> Reject(int id);
        Task<T> Approve(int id);
    }

    //Doctor operations
    public interface IDoctorGetRepository<AppointmentModelClass>
    {
        Task<IEnumerable<AppointmentModelClass>> GetAppointmentByDoctorId(int id);
    }

    public interface IDoctorRepository<PatientReport>
    {
        Task<PatientReport> CreateReport(PatientReport obj);
        Task<PatientReport> UpdateReport(int id, PatientReport obj);

    }

    //To get patient report
    public interface IPatientGetRepository<PatientReport>
    {
        Task<PatientReport> GetReportById(int id);
    }

    //To get all the doctors using role
    public interface IGetDoctorUsingRole<UserModelClass>
    {
        Task<IEnumerable<UserModelClass>> GetAllDoctorsUsingRole();
    }

    //To get user details using email
    public interface IGetUserDEtailsUsingEmail<UserModelClass>
    {
        Task<UserModelClass> GetUserDetailsUsingEmail(string email);
    }

    //To get user registered details using id
    public interface IGetUserdetailsUsingId<UserModelClass>
    {
        Task<UserModelClass> GetUserDetailsUsingId(int id);
    }

    //To get doctor details using user id
    public interface IGetDoctorDetailsUsingUserId<DoctorModelCLass>
    {
        Task<DoctorModelCLass> GetDoctorDetailsUsingUserId(int id);
    }

    //To get all the patients report
    public interface IGetPatientsAllReports<PatientReport>
    {
        Task<IEnumerable<PatientReport>> GetAllPatientsReports(int id);
    }

    //To get all my appointments
    public interface IGetMyAppointments<AppointmentsModelClass>
    {
        Task<IEnumerable<AppointmentsModelClass>> GetMyAppointments(int id);
    }

}
