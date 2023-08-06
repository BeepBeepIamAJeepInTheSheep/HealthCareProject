using HealthCareProject.Models;
using HealthCareProject.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthCareProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        IRepositories<PatientDetails> _repository;
        IRepositories<AppointmentsModelClass> _appointmentRep;
        IPatientGetRepository<PatientReport> _patientReportGetRepository;
        IGetUserdetailsUsingId<UserModelClass> _userDetailsUsingId;
        IGetRepositories<PatientDetails> _getPatientDetails;
        IGetPatientsAllReports<PatientReport> _getPatientsAllReports;
        IGetMyAppointments<AppointmentsModelClass> _getMyAppointments;

        public PatientController(IRepositories<PatientDetails> repository, IRepositories<AppointmentsModelClass> appointmentRep, IPatientGetRepository<PatientReport> patientReportGetRepository, IGetUserdetailsUsingId<UserModelClass> userDetailsUsingId, IGetRepositories<PatientDetails> getPatientDetails, IGetPatientsAllReports<PatientReport> getPatientsAllReports, IGetMyAppointments<AppointmentsModelClass> getMyAppointments)
        {
            _repository = repository;
            _appointmentRep = appointmentRep;
            _patientReportGetRepository = patientReportGetRepository;
            _userDetailsUsingId = userDetailsUsingId;
            _getPatientDetails = getPatientDetails;
            _getPatientsAllReports = getPatientsAllReports;
            _getMyAppointments = getMyAppointments;
        }

        //Adding patient details
        [HttpPost("CreatePatientDetails")]
        public async Task<IActionResult> CreatePatientDetails([FromBody] PatientDetails patientDetails)
        {
            if (ModelState.IsValid)
            {
                await _repository.Create(patientDetails);

                return Ok(patientDetails);

            }
            else
                return BadRequest();

        }

        //Modify Patient details
        [HttpPut("UpdatePatientDetails/{id}")]
        public async Task<IActionResult> UpdatePatientDetails(int id, [FromBody] PatientDetails patientDetails)
        {
            if (ModelState.IsValid)
            {
                await _repository.Update(id, patientDetails);
                return Ok(patientDetails);
            }
            return BadRequest();
        }

        //To Create an appointment
        [HttpPost("CreateAppointment")]
        public async Task<IActionResult> CreateAppointment([FromBody] AppointmentsModelClass appointment)
        {
            if (ModelState.IsValid)
            {
                await _appointmentRep.Create(appointment);

                return Ok();

            }
            else
                return BadRequest();
        }

        //To modify appointment
        [HttpPut("UpdateAppointment/{id}")]
        public async Task<IActionResult> UpdateAppointment(int id, [FromBody] AppointmentsModelClass appointment)
        {
            if (ModelState.IsValid)
            {
                await _appointmentRep.Update(id, appointment);
                return Ok(appointment);
            }
            return BadRequest();
        }

        //To cancel an appointment
        [HttpDelete("DeleteAppointment/{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            await _appointmentRep.Delete(id);
            return Ok();
        }

        //To get patient report by patient id
        [HttpGet("GetPatientReportById/{id}")]
        public async Task<IActionResult> GetPatientReportById(int id)
        {
            var res = await _patientReportGetRepository.GetReportById(id);
            if (res != null)
            {
                return Ok(res);
            }
            else
            {
                return NotFound();
            }
        }

        //To get userdetails using user id
        [HttpGet("GetUserDetailsUsingId/{id}")]
        public async Task<IActionResult> GetUserDetailsUsingId(int id)
        {
            var res = await _userDetailsUsingId.GetUserDetailsUsingId(id);
            if (res != null)
            {
                return Ok(res);
            }
            return NotFound();
        }

        //To get patient details using patient id
        [HttpGet("GetPatientDetailsUsingId/{id}")]
        public async Task<IActionResult> GetPatientDetailsUsingId(int id)
        {
            var res = await _getPatientDetails.GetById(id);
            if (res != null)
            {
                return Ok(res);
            }
            return NotFound();
        }

        //To get all the patient details
        [HttpGet("GetAllPatientDetails")]
        public async Task<IEnumerable<PatientDetails>> GetAllPatientDetails()
        {
            return await _getPatientDetails.GetAll();
        }

        //To get all patient reports
        [HttpGet("GetAllPatientReports/{id}")]
        public async Task<IEnumerable<PatientReport>> GetAllPatientReports(int id)
        {
            return await _getPatientsAllReports.GetAllPatientsReports(id);
        }

        //To get my appointment details
        [HttpGet("GetMyAppointments/{id}")]
        public async Task<IEnumerable<AppointmentsModelClass>> GetMyAppointments(int id)
        {
            return await _getMyAppointments.GetMyAppointments(id);
            
        }
    }
}



