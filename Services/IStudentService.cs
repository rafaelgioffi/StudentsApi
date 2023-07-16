using StudentsApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentsApi.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetStudents();
        Task<Student> GetStudent(int id);
        Task<IEnumerable<Student>> GetStudentByName(string nome);
        Task CreateStudent(Student aluno);
        Task UpdateStudent(Student aluno);
        Task DeleteStudent(Student aluno);
    }
}
