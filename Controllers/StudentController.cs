using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplicationTraining3.DB;
using WebApplicationTraining3.Entities;

namespace WebApplicationTraining3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly SqlDB _sqlDb;

        public StudentController(SqlDB sqlDb)
        {
            _sqlDb = sqlDb;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            List<Student> students = await _sqlDb.GetStudents();
            return Ok(students);
        }

        [HttpPost]
        public async Task<IActionResult> PostStudent([FromBody] Student student)
        {

            if (student == null)
            {
                return BadRequest("Student is Empty");
            }

            bool success = await _sqlDb.SaveStudent(student);

            if (success)
            {
                return Ok($"{student.FirstName} Saved Sucessfully");
            }
            else
            {
                return BadRequest("Student Not saved");
            }
        }
    }
}
