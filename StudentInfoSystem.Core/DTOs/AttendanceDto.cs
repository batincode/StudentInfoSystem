using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfoSystem.Core.DTOs
{
    public class AttendanceDto
    {
        public int StudentId { get; set; }  
        public int CourseId { get; set; }  
        public bool IsPresent { get; set; } 
    }
}
