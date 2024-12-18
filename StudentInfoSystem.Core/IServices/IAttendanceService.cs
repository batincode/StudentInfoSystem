using StudentInfoSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfoSystem.Core.Services
{
    public interface IAttendanceService
    {
        Task<IEnumerable<Attendance>> GetAllAttendanceRecordsAsync();
        Task<Attendance> GetAttendanceRecordByIdAsync(int id);
        Task AddAttendanceRecordAsync(Attendance attendance);
        Task UpdateAttendanceRecordAsync(Attendance attendance);
        Task DeleteAttendanceRecordAsync(int id);
        Task<IEnumerable<Attendance>> GetAttendanceByStudentIdAsync(int studentId); 
    }
}
