using Microsoft.AspNetCore.Mvc;
using StudentInfoSystem.Core.Entities;
using StudentInfoSystem.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentInfoSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherRepository _teacherRepository;

        
        public TeacherController(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Teacher>>> GetAllTeachers()
        {
            var teachers = await _teacherRepository.GetAllAsync();
            return Ok(teachers);
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<Teacher>> GetTeacherById(int id)
        {
            var teacher = await _teacherRepository.GetByIdAsync(id);
            if (teacher == null)
            {
                return NotFound($"Teacher with ID {id} not found.");
            }
            return Ok(teacher);
        }

       
        [HttpPost]
        public async Task<ActionResult<Teacher>> AddTeacher(Teacher teacher)
        {
            if (teacher == null)
            {
                return BadRequest("Teacher data is invalid.");
            }

            await _teacherRepository.AddAsync(teacher);
            return CreatedAtAction(nameof(GetTeacherById), new { id = teacher.Id }, teacher);
        }

        
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTeacher(int id, Teacher teacher)
        {
            if (id != teacher.Id)
            {
                return BadRequest("ID in URL does not match the ID of the teacher record.");
            }

            var existingTeacher = await _teacherRepository.GetByIdAsync(id);
            if (existingTeacher == null)
            {
                return NotFound($"Teacher with ID {id} not found.");
            }

            await _teacherRepository.UpdateAsync(teacher);
            return NoContent(); 
        }

        
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTeacher(int id)
        {
            var teacher = await _teacherRepository.GetByIdAsync(id);
            if (teacher == null)
            {
                return NotFound($"Teacher with ID {id} not found.");
            }

            await _teacherRepository.DeleteAsync(id);
            return NoContent(); 
        }

        
        [HttpGet("{teacherId}/courses")]
        public async Task<ActionResult<IEnumerable<Course>>> GetCoursesByTeacherId(int teacherId)
        {
            var courses = await _teacherRepository.GetCoursesByTeacherIdAsync(teacherId);
            if (courses == null || !courses.Any())
            {
                return NotFound($"No courses found for teacher with ID {teacherId}.");
            }
            return Ok(courses);
        }
    }
}
