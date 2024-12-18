using Microsoft.EntityFrameworkCore;
using StudentInfoSystem.Core.Entities;
using StudentInfoSystem.Core.Interfaces;
using StudentInfoSystem.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentInfoSystem.Infrastructure.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly ApplicationDbContext _context;

       
        public AttendanceRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        
        public async Task<IEnumerable<Attendance>> GetAllAsync()
        {
            return await _context.Attendances
                .Include(a => a.Student) 
                .Include(a => a.Course)  
                .Select(a => new Attendance
                {
                    Id = a.Id,
                    StudentId = a.StudentId,
                    CourseId = a.CourseId,
                    Date = a.Date,
                    IsPresent = a.IsPresent,
                   
                    Student = new Student
                    {
                        Name = a.Student.Name
                    },
                   
                    Course = new Course
                    {
                        Name = a.Course.Name
                    }
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<Attendance>> GetAllByStudentIdAsync(int studentId)
        {
            return await _context.Attendances
                .Where(a => a.StudentId == studentId)
                .ToListAsync();
        }

       
        public async Task<Attendance> GetByIdAsync(int id)
        {
            return await _context.Attendances
                .Where(a => a.Id == id)
                .Select(a => new Attendance
                {
                    Id = a.Id,
                    StudentId = a.StudentId,
                    CourseId = a.CourseId,
                    Date = a.Date,
                    IsPresent = a.IsPresent,
                    Student = new Student
                    {
                        Id = a.Student.Id,
                        Name = a.Student.Name 
                    },
                    Course = new Course
                    {
                        Id = a.Course.Id,
                        Name = a.Course.Name 
                    }
                })
                .FirstOrDefaultAsync();
        }



        
        public async Task AddAsync(Attendance attendance)
        {
            await _context.Attendances.AddAsync(attendance);
            await _context.SaveChangesAsync(); 
        }

       
        public async Task UpdateAsync(Attendance attendance)
        {
            _context.Attendances.Update(attendance);
            await _context.SaveChangesAsync(); 
        }

       
        public async Task DeleteAsync(int id)
        {
            var attendance = await _context.Attendances.FindAsync(id);
            if (attendance != null)
            {
                _context.Attendances.Remove(attendance);
                await _context.SaveChangesAsync(); 
            }
        }

        
        public async Task<IEnumerable<Attendance>> GetByStudentIdAsync(int studentId)
        {
            return await _context.Attendances
                .Where(a => a.StudentId == studentId)
                .ToListAsync();
        }
    }
}
