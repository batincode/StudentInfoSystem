using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfoSystem.Core.Entities
{
    public class Student
    {
        public int Id { get; set; }
        [Column("FullName")]
        public string Name{ get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? AdvisorId { get; set; }

        public Teacher Advisor { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }

    }
}