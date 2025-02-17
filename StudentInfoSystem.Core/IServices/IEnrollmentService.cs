using StudentInfoSystem.Core.Entities;

namespace StudentInfoSystem.Core.Services
{
    public interface IEnrollmentService
    {
        
        Task<IEnumerable<Enrollment>> GetEnrollmentsByStudentIdAsync(int studentId);
        Task<IEnumerable<Enrollment>> GetEnrollmentsByCourseIdAsync(int courseId);
        Task AddEnrollmentAsync(Enrollment enrollment);
        Task DeleteEnrollmentAsync(int id);
        Task<IEnumerable<Enrollment>> GetAllEnrollmentsAsync();
        Task<Enrollment> GetEnrollmentByIdAsync(int id);
        Task UpdateEnrollmentAsync(Enrollment enrollment);
        

     
       
    }
}
