using Microsoft.AspNetCore.Mvc;
using StudentInfoSystem.Core.DTOs;
using StudentInfoSystem.Core.Entities;
using StudentInfoSystem.Core.Interfaces;
using StudentInfoSystem.Core.Services; // AttendanceService'yi kullanmak için ekleyin
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentInfoSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IAttendanceService _attendanceService; // IAttendanceService'i ekleyin

        // Constructor: Dependency injection
        public AttendanceController(IAttendanceRepository attendanceRepository, IAttendanceService attendanceService)
        {
            _attendanceRepository = attendanceRepository;
            _attendanceService = attendanceService; // AttendanceService'i enjekte edin
        }

        // GET: api/attendance
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Attendance>>> GetAll()
        {
            var attendances = await _attendanceRepository.GetAllAsync();
            return Ok(attendances);
        }

        // GET: api/attendance/student/{studentId}
        [HttpGet("student/{studentId}")]
        public async Task<ActionResult<IEnumerable<Attendance>>> GetByStudentId(int studentId)
        {
            var attendances = await _attendanceRepository.GetByStudentIdAsync(studentId);
            if (attendances == null || !attendances.Any())
            {
                return NotFound($"Attendance records for student with ID {studentId} not found.");
            }
            return Ok(attendances);
        }

        // GET: api/attendance/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Attendance>> GetById(int id)
        {
            var attendance = await _attendanceRepository.GetByIdAsync(id);
            if (attendance == null)
            {
                return NotFound($"Attendance record with ID {id} not found.");
            }
            return Ok(attendance);
        }

        // POST: api/attendance
        [HttpPost]
        public async Task<IActionResult> AddAttendance([FromBody] AttendanceDto attendanceDto)
        {
            if (attendanceDto == null)
            {
                return BadRequest("Attendance data is null");
            }

            // DTO'yu Attendance nesnesine dönüştür
            var attendance = new Attendance
            {
                StudentId = attendanceDto.StudentId,
                CourseId = attendanceDto.CourseId,
                IsPresent = attendanceDto.IsPresent
            };

            // Attendance kaydını ekleyin
            await _attendanceService.AddAttendanceRecordAsync(attendance);

            // Yeni oluşturulan kaydın ID'si ile 201 Created yanıtı döndür
            return CreatedAtAction(nameof(GetById), new { id = attendance.Id }, attendance);
        }

        // PUT: api/attendance/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAttendance(int id, Attendance attendance)
        {
            if (id != attendance.Id)
            {
                return BadRequest("ID in URL does not match the ID of the attendance record.");
            }

            var existingAttendance = await _attendanceRepository.GetByIdAsync(id);
            if (existingAttendance == null)
            {
                return NotFound($"Attendance record with ID {id} not found.");
            }

            await _attendanceRepository.UpdateAsync(attendance);
            return NoContent(); // No content is returned for successful update
        }

        // DELETE: api/attendance/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAttendance(int id)
        {
            var attendance = await _attendanceRepository.GetByIdAsync(id);
            if (attendance == null)
            {
                return NotFound($"Attendance record with ID {id} not found.");
            }

            await _attendanceRepository.DeleteAsync(id);
            return NoContent(); // No content is returned for successful delete
        }
    }
}
