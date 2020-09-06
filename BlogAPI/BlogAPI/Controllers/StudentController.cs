using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using BlogAPI.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private List<Student> _student = new List<Student>
        {
            new Student
            {
                Id = 1,
                Address = "Kurunegala",
                Name = "Thisuri"
            },

            new Student
            {
                Id = 2,
                Address = "Kandy",
                Name = "Daphni"
            }
        };

        [Route("get-a")]

        [HttpGet]
        public IActionResult GetStudent()
        {
            
            return Ok(_student);
        }

        [HttpPost]
        public IActionResult CreateStudent([FromBody]Student newStudent)
        {
            newStudent.Id = _student.Select(s => s.Id).Max() + 1;
            _student.Add(newStudent);

            return Ok(newStudent);
        }

        [HttpPut]
        public IActionResult UpdateStudent(int Id,[FromBody] Student updatedStudent)
        {
            var student = _student.Where(s => s.Id == Id).FirstOrDefault();

            if (student == null)
                return NotFound();

            student.Name = updatedStudent.Name;
            student.Address = updatedStudent.Address;

            var index = _student.FindIndex(s => s.Id == Id);
            _student[index] = student;

            return Ok(student);
        }
    }
}
