using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfoSystem.Core.DTOs.CourseDto
{
    public class CourseCreateDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int Credits { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int TeacherId { get; set; }
    }

}
