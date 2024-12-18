using StudentInfoSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfoSystem.Core.Interfaces
{
    public interface IEnrollmentRepository
    {
        Task<Enrollment> GetByIdAsync(int id);
        Task<IEnumerable<Enrollment>> GetAllByStudentIdAsync(int studentId);
        Task<IEnumerable<Enrollment>> GetAllAsync();
       
        Task AddAsync(Enrollment enrollment);
        Task UpdateAsync(Enrollment enrollment);
        Task DeleteAsync(int id);
        Task<IEnumerable<Enrollment>> GetEnrollmentsByCourseIdAsync(int courseId);
    }


}
