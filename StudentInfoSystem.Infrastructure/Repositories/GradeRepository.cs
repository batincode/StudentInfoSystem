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
    public class GradeRepository : Repository<Grade>, IGradeRepository
    {
        public GradeRepository(ApplicationDbContext context) : base(context)
        {
        }

       
        public async Task<IEnumerable<Grade>> GetAllAsync()
        {
            return await _context.Set<Grade>().ToListAsync();
        }

    
        public async Task<IEnumerable<Grade>> GetByStudentIdAsync(int studentId)
        {
            return await _context.Set<Grade>()
                .Where(grade => grade.StudentId == studentId)
                .ToListAsync();
        }

      
        public async Task<Grade> GetByIdAsync(int id)
        {
            return await _context.Set<Grade>().FindAsync(id);
        }

        
        public async Task AddAsync(Grade grade)
        {
            await _context.Set<Grade>().AddAsync(grade);
        }

        
        public async Task UpdateAsync(Grade grade)
        {
            _context.Set<Grade>().Update(grade);
        }

        
        public async Task DeleteAsync(int id)
        {
            var grade = await _context.Set<Grade>().FindAsync(id);
            if (grade != null)
            {
                _context.Set<Grade>().Remove(grade);
            }
        }
        public async Task<IEnumerable<Grade>> GetAllByStudentIdAsync(int studentId)
        {
            return await _context.Set<Grade>()
                .Where(grade => grade.StudentId == studentId)
                .ToListAsync();
        }
    }
}
