using StudentInfoSystem.Core.Entities;
using StudentInfoSystem.Core.Interfaces;
using StudentInfoSystem.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfoSystem.Application.Services
{
    public class GradeService : IGradeService
    {
        private readonly IGradeRepository _gradeRepository;

        public GradeService(IGradeRepository gradeRepository)
        {
            _gradeRepository = gradeRepository;
        }

        public async Task<IEnumerable<Grade>> GetAllGradesAsync()
        {
            return await _gradeRepository.GetAllAsync();
        }

        public async Task<Grade> GetGradeByIdAsync(int id)
        {
            return await _gradeRepository.GetByIdAsync(id);
        }

        public async Task AddGradeAsync(Grade grade)
        {
            await _gradeRepository.AddAsync(grade);
        }

        public async Task UpdateGradeAsync(Grade grade)
        {
            await _gradeRepository.UpdateAsync(grade);
        }

        public async Task DeleteGradeAsync(int id)
        {
            await _gradeRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Grade>> GetGradesByStudentIdAsync(int studentId)
        {
            return await _gradeRepository.GetAllByStudentIdAsync(studentId);
        }
    }
}
