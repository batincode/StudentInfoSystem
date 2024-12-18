using Microsoft.AspNetCore.Mvc;
using StudentInfoSystem.Core.Entities;
using StudentInfoSystem.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using StudentInfoSystem.Core.DTOs.CourseDto;

namespace StudentInfoSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;

        public CoursesController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
        {
            var courses = await _courseRepository.GetAllAsync();
            return Ok(courses);
        }

        
        [HttpGet("teacher/{teacherId}")]
        public async Task<ActionResult<IEnumerable<Course>>> GetCoursesByTeacherId(int teacherId)
        {
            var courses = await _courseRepository.GetCoursesByTeacherIdAsync(teacherId);
            if (courses == null)
            {
                return NotFound();
            }
            return Ok(courses);
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return Ok(course);
        }

        
        [HttpPost]
        public async Task<ActionResult<Course>> AddCourse([FromBody] CourseCreateDto courseDto)
        {
            if (courseDto == null)
            {
                return BadRequest("Course data is null.");
            }

           
            var course = new Course
            {
                Name = courseDto.Name,
                Code = courseDto.Code,
                Credits = courseDto.Credits,
                StartTime = courseDto.StartTime,
                EndTime = courseDto.EndTime,
                TeacherId = courseDto.TeacherId
            };

            await _courseRepository.AddAsync(course);

           
            return CreatedAtAction(nameof(GetCourse), new { id = course.Id }, course);
        }


       
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCourse(int id, [FromBody] Course course)
        {
            if (course == null || id != course.Id)
            {
                return BadRequest();
            }

            var existingCourse = await _courseRepository.GetByIdAsync(id);
            if (existingCourse == null)
            {
                return NotFound();
            }

            await _courseRepository.UpdateAsync(course);
            return NoContent();
        }

       
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCourse(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            await _courseRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
