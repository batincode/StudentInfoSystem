using StudentInfoSystem.Core.DTOs.Teacher;
using StudentInfoSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using AutoMapper;

namespace StudentInfoSystem.Application.Mapping
{
    public class TeacherProfile : Profile
    {
        public TeacherProfile()
        {
            CreateMap<TeacherDto, Teacher>().ReverseMap();
        }
    }
}
