using Microsoft.AspNetCore.Mvc;
using StudentInfoSystem.Core.DTOs.Enrollment;
using StudentInfoSystem.Core.Entities;
using StudentInfoSystem.Core.Interfaces;
using StudentInfoSystem.Core.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentInfoSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IEnrollmentService _enrollmentService;

        public EnrollmentController(IEnrollmentRepository enrollmentRepository, IEnrollmentService enrollmentService)
        {
            _enrollmentRepository = enrollmentRepository;
            _enrollmentService = enrollmentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnrollmentDto>>> GetAllEnrollments()
        {
            var enrollments = await _enrollmentRepository.GetAllAsync();
            var enrollmentDtos = enrollments.Select(e => new EnrollmentDto
            {
                StudentId = e.StudentId,
                CourseId = e.CourseId,
                IsApproved = e.IsApproved,
                EnrollmentDate = e.EnrollmentDate,
                StudentName = e.Student.Name,
                CourseName = e.Course.Name
            }).ToList();

            return Ok(enrollmentDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EnrollmentDto>> GetEnrollmentById(int id)
        {
            var enrollment = await _enrollmentRepository.GetByIdAsync(id);
            if (enrollment == null)
            {
                return NotFound($"Enrollment record with ID {id} not found.");
            }

            var enrollmentDto = new EnrollmentDto
            {
                StudentId = enrollment.StudentId,
                CourseId = enrollment.CourseId,
                IsApproved = enrollment.IsApproved,
                EnrollmentDate = enrollment.EnrollmentDate,
                StudentName = enrollment.Student.Name,
                CourseName = enrollment.Course.Name
            };

            return Ok(enrollmentDto);
        }

        [HttpGet("student/{studentId}")]
        public async Task<ActionResult<IEnumerable<EnrollmentDto>>> GetEnrollmentsByStudentId(int studentId)
        {
            var enrollments = await _enrollmentRepository.GetByStudentIdAsync(studentId);
            if (enrollments == null || !enrollments.Any())
            {
                return NotFound($"No enrollments found for student with ID {studentId}.");
            }

            var enrollmentDtos = enrollments.Select(e => new EnrollmentDto
            {
                StudentId = e.StudentId,
                CourseId = e.CourseId,
                IsApproved = e.IsApproved,
                EnrollmentDate = e.EnrollmentDate,
                StudentName = e.Student.Name,
                CourseName = e.Course.Name
            }).ToList();

            return Ok(enrollmentDtos);
        }

        [HttpPost]
        public async Task<IActionResult> AddEnrollment([FromBody] EnrollmentDto enrollmentDto)
        {
            if (enrollmentDto == null)
            {
                return BadRequest("Enrollment data is null.");
            }

            var enrollment = new Enrollment
            {
                StudentId = enrollmentDto.StudentId,
                CourseId = enrollmentDto.CourseId,
                IsApproved = enrollmentDto.IsApproved,
                EnrollmentDate = enrollmentDto.EnrollmentDate
            };

            await _enrollmentService.AddEnrollmentAsync(enrollment);

            return CreatedAtAction(nameof(GetEnrollmentById), new { id = enrollment.Id }, enrollment);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateEnrollment(int id, [FromBody] EnrollmentDto enrollmentDto)
        {
            if (id <= 0 || enrollmentDto == null)
            {
                return BadRequest("Invalid request.");
            }

            var existingEnrollment = await _enrollmentRepository.GetByIdAsync(id);
            if (existingEnrollment == null)
            {
                return NotFound($"Enrollment record with ID {id} not found.");
            }

            existingEnrollment.IsApproved = enrollmentDto.IsApproved;
            existingEnrollment.EnrollmentDate = enrollmentDto.EnrollmentDate;

            await _enrollmentService.UpdateEnrollmentAsync(existingEnrollment);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEnrollment(int id)
        {
            var enrollment = await _enrollmentRepository.GetByIdAsync(id);
            if (enrollment == null)
            {
                return NotFound($"Enrollment record with ID {id} not found.");
            }

            await _enrollmentService.DeleteEnrollmentAsync(id);
            return NoContent();
        }
    }
}
