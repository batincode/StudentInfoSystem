using Microsoft.EntityFrameworkCore;
using StudentInfoSystem.Core.Entities;
using StudentInfoSystem.Core.Interfaces;
using StudentInfoSystem.Infrastructure.Data;

public class CourseRepository : ICourseRepository
{
    private readonly ApplicationDbContext _context;

    
    public CourseRepository(ApplicationDbContext context)
    {
        _context = context;
    }

   
    public async Task<IEnumerable<Course>> GetAllAsync()
    {
        return await _context.Courses.ToListAsync();
    }

   
    public async Task<IEnumerable<Course>> GetCoursesByTeacherIdAsync(int teacherId)
    {
        return await _context.Courses
            .Where(course => course.TeacherId == teacherId)
            .ToListAsync();
    }

    
    public async Task<Course> GetByIdAsync(int id)
    {
        return await _context.Courses.FindAsync(id);
    }

   
    public async Task AddAsync(Course course)
    {
        await _context.Courses.AddAsync(course);
        await _context.SaveChangesAsync();
    }

    
    public async Task UpdateAsync(Course course)
    {
        _context.Courses.Update(course);
        await _context.SaveChangesAsync();
    }

    
    public async Task DeleteAsync(int id)
    {
        var course = await _context.Courses.FindAsync(id);
        if (course != null)
        {
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
        }
    }
   

}
