using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentInfoSystem.Core.DTOs.Attendance;
using StudentInfoSystem.Core.Entities;
using StudentInfoSystem.Core.Interfaces;
using StudentInfoSystem.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentInfoSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IAttendanceService _attendanceService; 

      
        public AttendanceController(IAttendanceRepository attendanceRepository, IAttendanceService attendanceService)
        {
            _attendanceRepository = attendanceRepository;
            _attendanceService = attendanceService; 
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<AttendanceDto>>> GetAll()
        {
            var attendances = await _attendanceRepository.GetAllAsync();
            
            

            // Attendance modelinden AttendanceDto'ya dönüşüm
            var attendanceDtos = attendances.Select(a => new AttendanceDto
            {
                StudentId = a.StudentId,
                CourseId = a.CourseId,
                IsPresent = a.IsPresent,
                StudentName= a.Student.Name


            }).ToList();

            return Ok(attendanceDtos);
        }



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

       
        [HttpPost]
        public async Task<IActionResult> AddAttendance([FromBody] AttendanceDto attendanceDto)
        {
            if (attendanceDto == null)
            {
                return BadRequest("Attendance data is null");
            }

            
            var attendance = new Attendance
            {
                StudentId = attendanceDto.StudentId,
                CourseId = attendanceDto.CourseId,
                IsPresent = attendanceDto.IsPresent
            };

         
            await _attendanceService.AddAttendanceRecordAsync(attendance);

           
            return CreatedAtAction(nameof(GetById), new { id = attendance.Id }, attendance);
        }

       
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
            return NoContent(); 
        }

        
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAttendance(int id)
        {
            var attendance = await _attendanceRepository.GetByIdAsync(id);
            if (attendance == null)
            {
                return NotFound($"Attendance record with ID {id} not found.");
            }

            await _attendanceRepository.DeleteAsync(id);
            return NoContent(); 
        }
    }
}
