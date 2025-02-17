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
    public class EnrollmentRepository : Repository<Enrollment>, IEnrollmentRepository
    {
        public EnrollmentRepository(ApplicationDbContext context) : base(context)
        {
        }

        
        public async Task<IEnumerable<Enrollment>> GetAllAsync()
        {
            return await _context.Set<Enrollment>().ToListAsync();
        }

       
        public async Task<IEnumerable<Enrollment>> GetByStudentIdAsync(int studentId)
        {
            return await _context.Set<Enrollment>()
                .Where(enrollment => enrollment.StudentId == studentId)
                .ToListAsync();
        }

       
        public async Task<Enrollment> GetByIdAsync(int id)
        {
            return await _context.Set<Enrollment>().FindAsync(id);
        }

        
        public async Task AddAsync(Enrollment enrollment)
        {
            await _context.Set<Enrollment>().AddAsync(enrollment);
            await _context.SaveChangesAsync();
        }

        
        public async Task UpdateAsync(Enrollment enrollment)
        {
            _context.Set<Enrollment>().Update(enrollment);
        }

        
        public async Task DeleteAsync(int id)
        {
            var enrollment = await _context.Set<Enrollment>().FindAsync(id);
            if (enrollment != null)
            {
                _context.Set<Enrollment>().Remove(enrollment);
            }
        }
        public async Task<IEnumerable<Enrollment>> GetAllByStudentIdAsync(int studentId)
        {
            return await _context.Set<Enrollment>()
                .Where(enrollment => enrollment.StudentId == studentId)
                .ToListAsync();
        }
        public async Task<IEnumerable<Enrollment>> GetEnrollmentsByCourseIdAsync(int courseId)
        {
            return await _context.Set<Enrollment>()
                .Where(enrollment => enrollment.CourseId == courseId)
                .ToListAsync();
        }
    }
}
