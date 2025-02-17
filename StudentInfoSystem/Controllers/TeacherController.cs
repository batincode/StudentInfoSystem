using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentInfoSystem.Core.DTOs.Teacher;
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
        private readonly IMapper _mapper;

        public TeacherController(ITeacherRepository teacherRepository, IMapper mapper)
        {
            _teacherRepository = teacherRepository;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeacherDto>>> GetAllTeachers()
        {
            var teachers = await _teacherRepository.GetAllAsync();

            var teacherDtos = _mapper.Map<IEnumerable<TeacherDto>>(teachers);

            return Ok(teacherDtos); 
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<Teacher>> GetTeacherById(int id)
        {
            var teachers = await _teacherRepository.GetByIdAsync(id);
            if (teachers == null)
            {
                return NotFound($"Teacher with ID {id} not found.");
            }
            return Ok(teachers);
        }


        [HttpPost]
        public async Task<ActionResult<TeacherDto>> AddTeacher(TeacherDto teacherDto)
        {
            if (teacherDto == null)
            {
                return BadRequest("Teacher data is invalid.");
            }

            var teacher = _mapper.Map<Teacher>(teacherDto);
            await _teacherRepository.AddAsync(teacher);
            var resultDto = _mapper.Map<TeacherDto>(teacher);
            

            return CreatedAtAction(nameof(GetTeacherById), new { id = resultDto.Id }, resultDto);
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
