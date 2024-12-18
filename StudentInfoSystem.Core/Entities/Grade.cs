﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfoSystem.Core.Entities
{
    public class Grade
    {
        public int Id { get; set; }
        public int StudentId { get; set; }  
        public int CourseId { get; set; }   
        public decimal Score { get; set; }  
        public DateTime Date { get; set; }  

        public Student Student { get; set; }  
        public Course Course { get; set; }   
    }
}
