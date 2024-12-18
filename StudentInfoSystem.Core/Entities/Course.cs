using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfoSystem.Core.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }  
        public string Code { get; set; }  
        public int Credits { get; set; } 
        public DateTime StartTime { get; set; }  
        public DateTime EndTime { get; set; }  
        public int TeacherId { get; set; }  
        public ICollection<Student> Students { get; set; }
        public Teacher Teacher { get; set; }  
        public ICollection<Enrollment> Enrollments { get; set; }  
    }
}
