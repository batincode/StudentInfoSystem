using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfoSystem.Core.DTOs.Enrollment
{
    public class EnrollmentDto
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public bool IsApproved { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string StudentName { get; set; }
        public string CourseName { get; set; }
    }
}
