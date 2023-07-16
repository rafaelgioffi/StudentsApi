using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentsApi.Models;
using StudentsApi.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private IStudentService _studentService;
        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[Produces("application/json")]    //limita a api a retornar o resultado apenas em JSON
        public async Task<ActionResult<IAsyncEnumerable<Student>>> GetStudents()
        {
            try
            {
                var students = await _studentService.GetStudents();
                return Ok(students);
            }
            catch
            {
                //return BadRequest("Erro ao obter os alunos");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao obter alunos, código: {StatusCodes.Status500InternalServerError}");
                return StatusCode(StatusCodes.Status400BadRequest, $"Erro ao obter alunos, código: {StatusCodes.Status400BadRequest}");
                return StatusCode(StatusCodes.Status200OK, $"Erro ao obter alunos, código: {StatusCodes.Status200OK}");
            }
        }

        [HttpGet("StudentsByName")]
        public async Task<ActionResult<IAsyncEnumerable<Student>>> GetStudentByName([FromQuery] string name)
        {
            try
            {
                var students = await _studentService.GetStudentByName(name);
                if (students.Count() == 0)
                {
                    return NotFound($"Nenhum(a) aluno(a) chamado(a) \"{name}\" encontrado(a).");
                }
                return Ok(students);
            }
            catch
            {
                return NotFound($"Erro ao buscar o(a) aluno(a) {name}.");
            }
        }

        [HttpGet("{id:int}", Name = "GetStudent")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            try
            {
                var student = await _studentService.GetStudent(id);
                if (student == null)
                {
                    return NotFound($"Nenhum(a) aluno(a) com matrícula \"{id}\" cadastrado(a).");
                }
                return Ok(student);
            }
            catch
            {
                return NotFound($"Erro ao buscar a matrícula {id}.");
            }
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateStudent(Student student)
        {
            try
            {
                await _studentService.CreateStudent(student);
                return CreatedAtRoute(nameof(GetStudent), new { id = student.Id }, student);
            }
            catch
            {
                //return BadRequest("Erro ao cadastrar aluno(a)");
                return StatusCode(StatusCodes.Status400BadRequest, $"Erro: A matricula {student.Id} já está cadastrada!");
            }
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateStudent(int id, [FromBody] Student student)
        {
            try
            {
                if (student.Id == id)
                {
                    await _studentService.UpdateStudent(student);
                    return Ok($"Aluno(a) \"{student.Name}\" atualizado(a) na base de dados!");
                }
                else
                {
                    return BadRequest($"Dados inválidos do(a) aluno(a) \"{student.Name}\". Tente novamente.");
                }
            }
            catch
            {
                //return BadRequest("Erro ao cadastrar aluno(a)");
                return StatusCode(StatusCodes.Status204NoContent, $"Erro: Dados incompletos para atualizar o(a) aluno(a) {student.Name}...");
            }
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteStudent(int id)
        {
            try
            {
                var student = await _studentService.GetStudent(id);
                if (student != null)
                {
                    await _studentService.DeleteStudent(student);
                    return Ok($"O(a) aluno(a) com a matrícula {id} foi excluido(a) do sistema.");

                }
                else
                {
                    return NotFound($"Aluno(a) da matricula {id} não encontrado(a). Tente novamente.");
                }
            }
            catch
            {
                //return BadRequest("Erro ao cadastrar aluno(a)");
                return StatusCode(StatusCodes.Status204NoContent, $"Erro ao excluir o(a) aluno(a) com a matricula {id}...");
            }
        }

    }
}
