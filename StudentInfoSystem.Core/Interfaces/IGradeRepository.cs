using StudentInfoSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfoSystem.Core.Interfaces
{
    public interface IGradeRepository
    {
        Task<Grade> GetByIdAsync(int id);
        Task<IEnumerable<Grade>> GetAllByStudentIdAsync(int studentId);
        Task<IEnumerable<Grade>> GetAllAsync();  
        Task AddAsync(Grade grade);
        Task UpdateAsync(Grade grade);
        Task DeleteAsync(int id);
    }

}
