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
        public async Task<ActionResult<CourseResponseDto>> AddCourse([FromBody] CourseCreateDto courseCreateDto)
        {
            if (courseCreateDto == null)
            {
                return BadRequest("Course data is null.");
            }

            var course = new Course
            {
                Name = courseCreateDto.Name,
                Code = courseCreateDto.Code,
                Credits = courseCreateDto.Credits,
                StartTime = courseCreateDto.StartTime,
                EndTime = courseCreateDto.EndTime,
                TeacherId = courseCreateDto.TeacherId,
                Day = courseCreateDto.Day
            };

            await _courseRepository.AddAsync(course);

            var courseResponse = new CourseResponseDto
            {
                Id = course.Id,
                Name = course.Name,
                Code = course.Code,
                Credits = course.Credits,
                Day = course.Day,
                StartTime = course.StartTime,
                EndTime = course.EndTime,
                TeacherId = course.TeacherId
            };

            return CreatedAtAction(nameof(GetCourse), new { id = course.Id }, courseResponse);
        }





        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCourse(int id, [FromBody] CourseCreateDto courseCreateDto)
        {
            // Eğer gelen veri null ise veya ID eşleşmiyorsa, hatalı istek döndür
            if (courseCreateDto == null || id != courseCreateDto.Id)
            {
                return BadRequest("Course data is null or ID mismatch.");
            }

            // Veritabanından mevcut kursu al
            var existingCourse = await _courseRepository.GetByIdAsync(id);
            if (existingCourse == null)
            {
                return NotFound(); // Eğer kurs bulunamazsa, NotFound döndür
            }

            // Mevcut kursu güncelle
            existingCourse.Name = courseCreateDto.Name;
            existingCourse.Code = courseCreateDto.Code;
            existingCourse.Credits = courseCreateDto.Credits;
            existingCourse.StartTime = courseCreateDto.StartTime;
            existingCourse.EndTime = courseCreateDto.EndTime;
            existingCourse.Day = courseCreateDto.Day; // Gün bilgisini de burada güncelle
            existingCourse.TeacherId = courseCreateDto.TeacherId;

            // Kursu güncelle
            await _courseRepository.UpdateAsync(existingCourse);

            // Güncellenmiş kursu döndür
            return NoContent(); // Genellikle güncelleme işlemi sonrası 204 No Content döndürülür
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
