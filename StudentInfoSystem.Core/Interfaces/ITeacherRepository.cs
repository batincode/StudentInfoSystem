using StudentInfoSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfoSystem.Core.Interfaces
{
    public interface ITeacherRepository
    {
        Task<Teacher> GetByIdAsync(int id);
        Task<IEnumerable<Teacher>> GetAllAsync();
        Task<IEnumerable<Course>> GetCoursesByTeacherIdAsync(int teacherId);  
        Task AddAsync(Teacher teacher);
        Task UpdateAsync(Teacher teacher);
        Task DeleteAsync(int id);
    }

}
