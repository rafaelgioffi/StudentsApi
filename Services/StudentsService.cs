using StudentsApi.Context;
using StudentsApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsApi.Services
{
    public class StudentsService : IStudentService
    {
        private readonly AppDbContext _context; //instancia do contexto
        public StudentsService(AppDbContext context)  //construtor usando o contexto
        {
            _context = context;
        }

        public async Task<IEnumerable<Student>> GetStudents()
        {
            try
            {
                return await _context.Students.ToListAsync();
            }
            catch
            {
                throw;
            }
        }
        public async Task<Student> GetStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            return student;
        }
        public async Task<IEnumerable<Student>> GetStudentByName(string nome)
        {
            IEnumerable<Student> students;
            if (!string.IsNullOrWhiteSpace(nome))
            {
                students = await _context.Students.Where(n => n.Name.Contains(nome)).ToListAsync();
            }
            else
            {
                students = await GetStudents();
            }
            return students;
        }
        public async Task CreateStudent(Student aluno)
        {
            _context.Students.Add(aluno);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateStudent(Student aluno)
        {
            _context.Entry(aluno).State= EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task DeleteStudent(Student aluno)
        {
            _context.Students.Remove(aluno);
            await _context.SaveChangesAsync();
        }
    }
}
