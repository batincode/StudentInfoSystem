using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfoSystem.Core.DTOs.CourseDto
{
    public class CourseCreateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int Credits { get; set; }

        // Dersin başlangıç ve bitiş tarih-saatleri
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        // Haftanın günü (Pazartesi, Salı, vb.)
        public string? Day { get; set; }

        // Dersi açacak öğretmenin ID'si
        public int TeacherId { get; set; }
    }

}
