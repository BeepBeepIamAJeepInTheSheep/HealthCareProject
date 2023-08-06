using HealthCareProject.Models;
using HealthCareProject.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthCareProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        IDoctorGetRepository<AppointmentsModelClass> _getAppointment;
        IDoctorRepository<PatientReport> _appointmentRep;
        IGetDoctorDetailsUsingUserId<DoctorModelCLass> _getDoctorByUserId;
        IGetRepositories<DoctorModelCLass> _getRepositories;

        public DoctorController(IDoctorGetRepository<AppointmentsModelClass> getAppointment, IDoctorRepository<PatientReport> appointmentRep, IGetDoctorDetailsUsingUserId<DoctorModelCLass> getDoctorByUserId, IGetRepositories<DoctorModelCLass> getRepositories)
        {
            _getAppointment = getAppointment;
            _appointmentRep = appointmentRep;
            _getDoctorByUserId = getDoctorByUserId;
            _getRepositories = getRepositories;
        }

        //To get all the appointments of the doctor
        [HttpGet("GetAllAppointments/{id}")]
        public async Task<IEnumerable<AppointmentsModelClass>> GetAllAppointments(int id)
        {
            return await _getAppointment.GetAppointmentByDoctorId(id);
            
        }

        //To create a report
        [HttpPost("CreateReport")]
        public async Task<IActionResult> CreateReport([FromBody] PatientReport patientReport)
        {
            if (ModelState.IsValid)
            {
                await _appointmentRep.CreateReport(patientReport);
                return Ok();
            }
            return BadRequest();
        }

        //To update report
        [HttpPut("UpdateReport/{id}")]
        public async Task<IActionResult> UpdateReport(int id, [FromBody] PatientReport patientReport)
        {
            if (ModelState.IsValid)
            {
                await _appointmentRep.UpdateReport(id, patientReport);
                return Ok();
            }
            return BadRequest();
        }

        //To get doctor details using userid
        [HttpGet("GetDoctorDetailsByUserId/{id}")]
        public async Task<IActionResult> GetDoctorDetailsByUserId(int id)
        {
            var res = await _getDoctorByUserId.GetDoctorDetailsUsingUserId(id);
            if (res != null)
            {
                return Ok(res);
            }
            else { return BadRequest(); }
        }

        //To get doctor details using id
        [HttpGet("GetDoctorDetailsById/{id}")]
        public async Task<IActionResult> GetDoctorDetailsById(int id)
        {
            var res = await _getRepositories.GetById(id);
            if (res != null)
            {
                return Ok(res);
            }
            else { return BadRequest(); }
        }
    }
}
