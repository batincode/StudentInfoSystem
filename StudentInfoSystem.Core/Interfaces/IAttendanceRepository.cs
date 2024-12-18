using StudentInfoSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace StudentInfoSystem.Core.Interfaces
{
    public interface IAttendanceRepository
    {
        Task<Attendance> GetByIdAsync(int id);
        Task<IEnumerable<Attendance>> GetAllByStudentIdAsync(int studentId);
        Task<IEnumerable<Attendance>> GetAllAsync();
        Task AddAsync(Attendance attendance);
        Task UpdateAsync(Attendance attendance);
        Task DeleteAsync(int id);

       
        Task<IEnumerable<Attendance>> GetByStudentIdAsync(int studentId);  
    }



}

