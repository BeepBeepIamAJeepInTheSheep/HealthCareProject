using HealthCareProject.Models;
using HealthCareProject.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthCareProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        IRepositories<DoctorModelCLass> _doctorsRepositories;
        IGetRepositories<DoctorModelCLass> _getRepositories;
        IGetRepositories<AppointmentsModelClass> _getAppointment;
        IAdminRepository<AppointmentsModelClass> _adminRepository;
        IGetDoctorUsingRole<UserModelClass> _getDoctorUsingRole;
        IGetRepositories<UserModelClass> _getRepositories1;
        IGetDoctorDetailsUsingUserId<DoctorModelCLass> _getDoctorUsingUserId;

        public AdminController(IRepositories<DoctorModelCLass> doctorsRepositories, IGetRepositories<DoctorModelCLass> getRepositories, IGetRepositories<AppointmentsModelClass> getAppointment, IAdminRepository<AppointmentsModelClass> adminRepository, IGetDoctorUsingRole<UserModelClass> getDoctorUsingRole,IGetRepositories<UserModelClass> getRepositories1, IGetDoctorDetailsUsingUserId<DoctorModelCLass> getDoctorUsingUserId)
        {
            _doctorsRepositories = doctorsRepositories;
            _getRepositories = getRepositories;
            _getAppointment = getAppointment;
            _adminRepository = adminRepository;
            _getDoctorUsingRole = getDoctorUsingRole;
            _getRepositories1 = getRepositories1;
            _getDoctorUsingUserId = getDoctorUsingUserId;
        }

        //Get all the doctors information 
        [HttpGet("GetAllDoctors")]
        public async Task<IEnumerable<DoctorModelCLass>> GetAllDoctors()
        {
            return await _getRepositories.GetAll();
        }

        //Get The doctors details using doctor id
        [HttpGet]
        [Route("GetDoctorById/{id}", Name = "GetDoctorById")]
        public async Task<IActionResult> GetDoctorById(int id)
        {
            var doctor = await _getDoctorUsingUserId.GetDoctorDetailsUsingUserId(id);
            if (doctor != null)
            {
                return Ok(doctor);
            }
            return NotFound();
        }

        
        //Add new Doctor details to the database
        [HttpPost("AddDoctor")]
        public async Task<IActionResult> AddDoctor([FromBody] DoctorModelCLass doctor)
        {
            if (ModelState.IsValid)
            {
                await _doctorsRepositories.Create(doctor);
                return CreatedAtAction("GetDoctorById", new { id = doctor.Id }, doctor);
            }
            return BadRequest();

        }

        //delete Doctor details using doctorId
        [HttpDelete("DeleteDoctor/{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            var res = await _doctorsRepositories.Delete(id);
            if (res != null)
            {
                return Ok(res);
            }
            return NotFound("Doctor with id " + id + " not available");
        } 


        //update Doctor details
        [HttpPut("UpdateDoctor/{id}")]
        public async Task<IActionResult> UpdateDoctor(int id, [FromBody] DoctorModelCLass doctor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _doctorsRepositories.Update(id, doctor);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }


        //To get all the appointments
        [HttpGet("GetAllAppointments/{status}")]
        public async Task<IEnumerable<AppointmentsModelClass>> GetAllAppointments(string status)
        {
            if(status == "PENDING")
            {
                return await _adminRepository.GetAllPendings();
            }
            else if (status == "APPROVED")
            {
                return await _adminRepository.GetAllApproved();
            }
            else if (status == "REJECTED")
            {
                return await _adminRepository.GetAllRejected();
            }
            else
            {
                return await _getAppointment.GetAll();
            }
        }

        //To approve appointments
        [HttpPut("ApproveAppointment/{id}")]
        public async Task<IActionResult> ApproveAppointment(int id)
        {
            var approve = await _adminRepository.Approve(id);
            if (approve != null)
            {
                return Ok(approve);
            }
            else { return NotFound("Appointment with id " + id + " not available"); }
        }

        //To Reject appointments
        [HttpPut("RejectAppointment/{id}")]
        public async Task<IActionResult> RejectAppointment(int id)
        {
            var reject = await _adminRepository.Reject(id);
            if (reject != null)
            {
                return Ok(reject);
            }
            else { return NotFound("Appointment with id " + id + " not available"); }

        }

        //To get all the doctors using role
        [HttpGet("GetAllDoctorsUsingRole")]
        public async Task<IEnumerable<UserModelClass>> GetAllDoctorsUsingRole()
        {
            return await _getDoctorUsingRole.GetAllDoctorsUsingRole();
            
        }

        //To get all the user details
        [HttpGet("GetAllTheUserDetails")]
        public async Task<IEnumerable<UserModelClass>> GetAllTheUserDetails()
        {
            return await _getRepositories1.GetAll();
        }

        //To get appointment details
        [HttpGet("GetAppointmentdetails/{id}")]
        public async Task<AppointmentsModelClass> GetAppointmentsetails(int id)
        {
            return await _getAppointment.GetById(id);
        }
    }
}
