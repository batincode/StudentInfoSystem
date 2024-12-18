using Microsoft.EntityFrameworkCore;
using StudentInfoSystem.Core.Entities;
using StudentInfoSystem.Core.Interfaces;
using StudentInfoSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentInfoSystem.Infrastructure.Repositories
{
    public class TeacherRepository : Repository<Teacher>, ITeacherRepository
    {
        public TeacherRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Teacher>> GetAllAsync()
        {
            return await _context.Set<Teacher>().ToListAsync();
        }

       
        public async Task<Teacher> GetByIdAsync(int id)
        {
            return await _context.Set<Teacher>().FindAsync(id);
        }

     
        public async Task AddAsync(Teacher teacher)
        {
            await _context.Set<Teacher>().AddAsync(teacher);
        }

        
        public async Task UpdateAsync(Teacher teacher)
        {
            _context.Set<Teacher>().Update(teacher);
        }

       
        public async Task DeleteAsync(int id)
        {
            var teacher = await _context.Set<Teacher>().FindAsync(id);
            if (teacher != null)
            {
                _context.Set<Teacher>().Remove(teacher);
            }
        }
        public async Task<IEnumerable<Course>> GetCoursesByTeacherIdAsync(int teacherId)
        {
            return await _context.Set<Course>()
                .Where(course => course.TeacherId == teacherId) 
                .ToListAsync();
        }
    }
}
