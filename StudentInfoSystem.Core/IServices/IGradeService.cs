using StudentInfoSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfoSystem.Core.Services
{
    public interface IGradeService
    {
        Task<IEnumerable<Grade>> GetGradesByStudentIdAsync(int studentId);
        Task AddGradeAsync(Grade grade);
        Task UpdateGradeAsync(Grade grade);
        Task DeleteGradeAsync(int id);
    }
}
